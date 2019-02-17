using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Liste der Photos, die einem User zugeordnet sind.
        // Es handelt sich hiermit um eine 1:n Relation.
        // Damit EF detailliertere Migrationsscripte erstellen kann,
        // die den Vorstellungen hinsichtlich der Relation zwischen der
        // Klasse User und Photo entsprechen, ist es notwendig
        // auch in der N-Klasse (also Photos) die Relation zu dieser Klasse
        // explizit anzugeben.
        // Diese Navigations-Property führt allerdings zu einem Fehler
        // beim Abruf der Daten im Rahmen eines API-Controllers.
        // Dies liegt daran, dass in der Klasse Photo ein Verweis
        // auf das User-Objekt enthalten ist, somit kommt es zu einem Loop
        // dieser beiden Klassen.
        // In der start.cs Datei kann im Rahmen der MVC-Konfiguration daher
        // ein Ignorieren dieses Fehlers konfiguriert werden.
        // services.AddMvc(). ...
        //  .AddJsonOptions(opt => {
        //     opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // });
        public ICollection<Photo> Photos { get; set; }


        
    }
}