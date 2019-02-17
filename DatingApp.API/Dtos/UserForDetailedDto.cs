using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int  Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }

        // Verweis auf die spezielle PhotoDto-Klasse,
        // um z.B. die Nagivations-Properties zur User-Klasse
        // nicht in der Ausgabe zu berücksichtigen.
        public ICollection<PhotosForDetailedDto> Photos { get; set; }

        // Verweis auf die ursprüngliche Collection der Photos
        // Da die Photo-Klasse einen Verweis auf User enthält,
        // werden beim Abruf der Daten und der Konvertierung
        // zu einer JSON-Auflistung alle Properties der User-
        // Klasse (incl. Passwort-Feldern) aufgeführt.
        // public ICollection<Photo> Photos { get; set; }
    }
}