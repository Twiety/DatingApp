using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        // ID des Bildes in der Cloud
        public string PublicId  { get; set; }

        // Nachfolgende Angaben sind Navigations-Attribute und dienen zur Definition
        // der Relation zwischen User- und Photo-Klasse.
        // Mit Hilfe dieser Angaben wird ein Migratonsscript
        // erstellt, welches beim Löschen eines User ein kaskadierented Löschen
        // der Photos auslöst.
         public User User { get; set; }

         public int UserId { get; set; }


    }
}