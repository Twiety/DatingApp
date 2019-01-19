using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Anpassung der Zugriffsregeln,
            // denn Client- und Server-Applikation werden von verschiedenen Webdiensten
            // auf unterschiedlichen Ports zur Verfügung gestellt.
            // Somit kommt es zu einem Cross-Domain-Zugriff der per Standard nicht erlaubt ist.
            services.AddCors();

            // Arten der Service-Injezierung
            // services.AddSingleton --> Fügt eine einzige Instanz des Objektes zur Applikation hinzu. Dieses Objekt ist für alle Http-Requests identisch
            // services.AddTransient --> Fügt eine Instanz des Objektes bei jedem Seitenaufruf hinzu
            // services.AddScoped --> Ähnlich wie Singleton, aber ???
            // services.AddSession --> Fügt eine Instanz im Rahmen einer Session der Applikation hinzu

            // Registriert die konkrete AuthRepository-Klasse unter Angabe der zugehörigen Schnittstelle als Service
            services.AddScoped<IAuthRepository,AuthRepository>();

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // !!! In dieser Funktion ist auch die Reihenfolge von Bedeutung !!!

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();   --> Zwangsweise Umleitung zur Https-Seite

            // Anpassung der Zugriffsregeln, um einen Cross-Domain-Zugriff zu erlauben.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Prüfung einer evtl. notwendingen Authentifizierung (diese muss zuvor als Service definiert sein; siehe Funktion ConfigureServices)
            app.UseAuthentication();

            // Ausgabe der API-Controller
            app.UseMvc();
        }
    }
}
