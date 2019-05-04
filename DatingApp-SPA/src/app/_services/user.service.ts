// Achtung: Der hier definierte Service muss (wie gewöhnlich) in app.module.ts im Bereich Providers angegeben werden!!!
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';

// Header-Objekt definieren, in dem das aktuelle Token gespeichert wird.
// Diese Vorgehensweise hat nur das Problem, dass sie bei jedem Request
// diesem manuell hinzugefügt werden muss.
// Stattdessen wird ein HttpInterceptor verwendet, der mittels JwtModule
// zur Verfügung gestellt wird.
// const httpOptions = {
//   headers: new HttpHeaders({
//     'Authorization': 'Bearer ' + localStorage.getItem('token')
//   })
// };

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // Verwendung der Umgebungsvariabel apiUrl (siehe environment/environment.ts)
  baserUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  // Abruf der User-Liste
  // Die nachfolgenden Funktion gibt als Ergebnis ein Observable zurück,
  // das seinerseits ein Array vom Typ User enthält.
  getUsers(): Observable<User[]> {
    // Standard-Aufruf der API ohne Übergabe des Authentifizierungs-Token
    return this.http.get<User[]>(this.baserUrl + 'users');
    // Standard-Aufruf der API mit Übergabe des Authentifizierungs-Token
    // return this.http.get<User[]>(this.baserUrl + 'users', httpOptions);
  }

  // Daten des angegebenen Users (id) laden
  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baserUrl + 'users/' + id);
    // return this.http.get<User>(this.baserUrl + 'users/' + id, httpOptions);
  }

  // Daten des angegebenen Users (id) in der Datenbank speichern.
  // Die Daten befinden sich in der Variabel user die vom Typ User ist.
  updateUser(id: Number, user: User) {
    return this.http.put(this.baserUrl + 'users/' + id,user);
  }
}
