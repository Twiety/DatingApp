using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        // Erweiterungsmethode um die Fehlerausgabe so zu konfigurieren,
        // dass der Header der Ausgabe die Angaben zum Fehler enthält.
        // Da die hiesige Applikation nur eine REST-Api beinhaltet, ist
        // die sonst übliche textuelle Fehlerausgabe nicht wirklich sinnvoll.
        // In VB würde die Methode mit dem Attribut <Extension()> versehen.
        // In C# erfolgt diese Kennzeichnung durch die Angabe von this
        // in der Funktionssignatur vor Definition des ersten Übergabeparameters.
        // Der erste Übergabeparameter bezieht sich somit immer auf this
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // Ausgabe des Fehlernachricht
            response.Headers.Add("Application-Error", message);
            // Da die Angular-Applikation den vorherigen Header nicht kennt
            // und es somit zu einem Cross-Origin-Zugriff käme, muss dieser Header zunächst
            // bekannt gemacht werden, außerdem bedarf es der Erlaubnis 
            // aller aufrufenden Domainen diese Header-Informationen zu verwenden
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today)
                age--;
                
            return age;
        }
    }
}