using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Nachfolgender Service dient zur Anpassung der Zugriffsregeln.
            // Denn Client- und Server-Applikation werden von verschiedenen Webdiensten
            // auf unterschiedlichen Ports zur Verfügung gestellt.
            // Somit kommt es zu einem Cross-Domain-Zugriff der per Standard nicht erlaubt ist.
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();

            // Anpassung der Zugriffsregeln, um einen Cross-Domain-Zugriff zu erlauben.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
