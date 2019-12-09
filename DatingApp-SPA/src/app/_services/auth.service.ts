import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

// Konstruktor der Klasse nimmt ein Objekt vom Typ HttpClient
// entgegen und speichert dieses Objekt in einer als privat deklarierten
// Variabel mit dem Namen http
// Um mit einer Variabel von diesem Typ arbeiten zu können,
// muss der entsprechende Namespace mittels Import-Anweisung zuvor
// importiert werden.

constructor(private http: HttpClient) { }

// Login-Funktion
// Der Funktionsparameter beinhaltet das clientseitige Datenmodell,
// welches mittels Angular an das Formular und die Controls gebunden ist.
// Innerhalb dieser Funktion wird dann ein HttpClient-Zugriff per Post
// auf die definierte URL (erster Parameter) durchgeführt.
// Im zweiten Parameter wird das Datenmodell übergeben.
// Die Definition eines dritten Parameters ist in diesem Fall nicht notwendig.
// Dieser würde verwendet werden, wenn der aufgerufene Webservice per Standard
// kein JSON-Objekt erwarten würde (diese ist aber bei unser Server-App der Fall)
// oder der Zugriff auf die WebApi einen authentifizierten Zugriff erfordert.
// Auch das ist hier nicht der Fall,
// da die Login-Methode einen anonymen Zugriff gestattet.
// Der HttpClient-Aufruf gibt als Ergebnis ein Observable zurück,
// dieses wird dann mit der Pipe-Methode verarbeitet.
// Innerhalb dieser Methode erfolgt dann mit der Map-Funktion
// die Verarbeitung des Response-Streams.
// Für die Map-Methode ist es allerdings erforderlich,
// dass mittels Import-Anweisung der Namespace map importiert wird.
// Der Namespace befindet sich im Verzeichnis rxjs/operators
// Der Inhalt der Map-Anweisung gestaltet sich wie folgt:
// - Die Antwort des Servers wird mittels einer Array-Funktion analysiert.
//   Hierzu wird eine Variabel mit dem Namen response definiert,
//   die vom Typ any ist. Denn zunächst kann der Typ nicht näher bestimmt werden.
// - Dem Objekt user wird dann diese Variabel übergeben.
// - Wenn die Konstante erfolgreicht initialisiert wurde, (weil ihr ein Inhalt übergeben werden konnte)
//   dann wird das Attribut token des Objektes user mittels der JavaScript-Funktion
//  localStorage gespeichert.

login(model: any) {
  return this.http.post(this.baseUrl + 'login',model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          // Das vom Server empfangene Token wird mittels jwtHelper dekodiert
          // und als JSON-Objekt zurückgegeben.
          // Folgende Properties sind im Token der hiesigen Applikation enthalten:
          // nameid = numerische ID des Users
          // unique_name = Name des Users
          // nbf = Ausgabedatum / -Uhrzeit
          // exp = Ablaufdatum / -Uhrzeit
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          // console.log(this.decodedToken);
        }
      })
    );
}

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  // Nachfolgende Funktion benutzt JwtHelper-Service zum Prüfen,
  // ob das Token gültig ist.
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
