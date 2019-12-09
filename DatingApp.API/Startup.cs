using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// Mittels NUGET folgendes Paket hinzufügen            
// Microsoft.EntityFrameworkCore.Sqlite
// Sqlite --> Version 2.1.0
// Version 2.2.x scheint buggy zu sein
// In appsettings.json ist der Connection-String mit dem Namen "DefaultConnection" hinterlegt.
// Für Entwicklungszwecke kann ein gleicher Eintrag in appsettings.Developement.json angelegt werden,
// dieser würde dann die Werte in der ersten Datei überschreiben.

// EF-Befehle
// Anlegen einer Migration mit dem Namen InitalCreate 
// Beim Anlegen einer ersten Migration wird ein Ordner "Migrations" angelegt.
// In diesem befinden sich dann die eigentlichen Migrationsscripte
// --> dotnet ef migrations add InitalCreate
// Anwenden der Migration
// Im Rahmen der ersten Migration wird außerdem die Datenbank angelegt
// dotnet ef database update

// Starten der Applikation mittels dotnet run
// oder dotnet watch run. Hierbei wird bei einer Änderung
// des Quellcodes die Applikation automatisch angehalten und neu gestartet.

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Nachfolgende Services müssen für die Applikation definiert werden.
            // Diese stehen dann zu einem späteren Zeitpunkt zur Verfügung.
            // Die Reihenfolge der Registrierung ist in dieser Funktion nicht von Bedeutung.

            // Zugriff auf den Datenbank-Context
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // MVC-Funktionalität
            // Mittels der JSON-Einstellung wird ein Loop, der sich durch ein gegenseitiges Referenzieren von Objekten
            // in Entity Framework ergibt, ignoriert.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(opt => {
                        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            // Anpassung der Zugriffsregeln,
            // denn Client- und Server-Applikation werden von verschiedenen Webdiensten
            // auf unterschiedlichen Ports zur Verfügung gestellt.
            // Somit kommt es zu einem Cross-Domain-Zugriff der per Standard nicht erlaubt ist.
            services.AddCors();

            // Aus der Konfiguration den Abschnitt mit dem Namen CloudinarySettings laden
            // und damit ein Objekt vom Typ CloudinarySettings initialisieren.
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            // Tool um ein automatisches Mapping der Properties zwischen zwei Klassen zu erreichen.
            // Das Abbilden der Eigenschaften zwischen den Modell-Klassen und den DTO-Klassen wird
            // somit automatisiert.
            services.AddAutoMapper();

            // Hinzufügen der Klasse Seed, um bei Bedarf Daten generieren zu lassen
            services.AddTransient<Seed>();

            // Arten der Service-Injezierung
            // services.AddSingleton --> Fügt eine einzige Instanz des Objektes zur Applikation hinzu. Dieses Objekt ist für alle Http-Requests identisch
            // services.AddTransient --> Fügt eine Instanz des Objektes bei jedem Seitenaufruf hinzu
            // services.AddScoped --> Ähnlich wie Singleton, aber ???
            // services.AddSession --> Fügt eine Instanz im Rahmen einer Session der Applikation hinzu

            // Registriert die konkreten Repository-Klassen unter Angabe der zugehörigen Schnittstelle als Service
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IDatingRepository, DatingRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                                GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                    });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder, IApplicationLifetime applicationLifetime)
        {
            // !!! In dieser Funktion ist auch die Reihenfolge von Bedeutung !!!
            applicationLifetime.ApplicationStopping.Register(OnShutDown);

            // Ausgabe von statischen Dateien
            app.UseStaticFiles();

            // Über die Umgebungsvariabel env.IsDevelopment wird bestimmt, in welchem
            // Kontext die Applikation ausgeführt wird.
            // Die Einstellung hierzu wird in der Datei Properties/launchSettings.json
            // und dort in der Angabe ASPNETCORE_ENVIRONMENT geändert.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Globales Abfangen von Fehlern, wenn die Applikation Produktionsmodus aufgerufen wird.
                // Der auslösende Fehler führt zum Ausführen eines Webrequests in einem neuen Task
                // Diesem Request werden die Angaben zum Fehler übergeben damit diese bei Bedarf
                // protokolliert werden können.
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        // Ursprünglichen Fehler ermitteln
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            // Verwendung der Standardmethode WriteAsync der Klasse HttpContext zur Ausgabe des Fehlers.
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();   --> Zwangsweise Umleitung zur Https-Seite

            // User-Daten generieren (wird nur bei leerer Datenbank benötigt), andernfalls auskommentiert!
            //seeder.SeedUsers();

            // Anpassung der Zugriffsregeln, um einen Cross-Domain-Zugriff zu erlauben.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Prüfung einer evtl. notwendingen Authentifizierung (diese muss zuvor als Service definiert sein; siehe Funktion ConfigureServices)
            app.UseAuthentication();

            // Ausgabe der API-Controller
            app.UseMvc();
        }

        private void OnShutDown()
        {
            System.Diagnostics.Debug.WriteLine("Debug Shutdown application");
            System.Diagnostics.Trace.WriteLine("Trace Shutdown application");
            // System.Diagnostics.Trace.Flush();
            // System.Threading.Thread.Sleep(1000);
        }
    }
}
