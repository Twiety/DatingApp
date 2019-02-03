import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  // constructor(private authService: AuthService, private alertify: AlertifyService) { }

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {

    // Dem definierten Login-Service wird das hiesige Datenmodell (model) übergeben.
    // Da der Service ein Observable zurückgibt,
    // ist mittels Subscribe-Funktion zu definieren,
    // wie ein positiver und ein negativer Response zu verarbeiten ist.

    this.authService.login(this.model).subscribe(next => {
        this.alertify.success('Logged in successfully');
        // console.log('Logged in successfully');
      }, error => {
        this.alertify.error(error);
        // console.log(error);
      });
  }

  // Nachfolgende Funktion prüft mit Hilfe des Local-Storage,
  // ob ein Objekt unter dem Namen token gespeichert wurde.
  // Die Gütligkeit kann hier nicht geprüft werden.
  // Grundsätzlich sollte auf diese Funktion hier zu Gunsten
  // einer (globalen) Service-Funktion verzichtet werden.
  // In auth.service.ts wurde daher eine gleichnamige Funktion
  // erstellt, die zusätzlich auch die zeitliche Gültigkeit des Token
  // prüft.
  loggedIn() {
    // Zugriff auf LocalStorage um die Existenz des Token zu prüfen.
    // Eine Prüfung des Token hinsichtlich der Gültigkeit erfolgt nicht.
    // const token = localStorage.getItem('token');
    // Kurzschreibweise um true oder false zurückzugeben,
    // hinsichtlich der Initialisierung der Variabel.
    // return !!token;

    // Nachfolgendes Statement nutzt die Funktion loggedIn des AuthService
    // um die Existenz und die Gültigkeit des Tokens zu kontrollieren.
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    // console.log('logged out');
  }
}
