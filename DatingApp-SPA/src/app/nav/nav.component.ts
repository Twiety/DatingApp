import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  // constructor(private authService: AuthService, private alertify: AlertifyService) { }

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {

    // Dem definierten Login-Service wird das hiesige Datenmodell (model) übergeben.
    // Da der Service ein Observable zurückgibt,
    // werden in der Subscript-Methode die folgenden drei Blöcke definiert,
    // die als Callback nach dem Ausführen der Service-Funktion aufgerufen werden.
    // 1) Next => Funktion bei erfolgreichen Aufruf des Service
    // 2) Error => Funktion wenn der Service auf einen Fehler lief
    // 3) Funktion, die nach Abschluss unabhängig vom Ergebnis immer aufgerufen wird
    //    (Diese Angabe ist optional)

    this.authService.login(this.model).subscribe(next => {
        // Anweisungen, die bei Erfolg aufgerufen werden
        this.alertify.success('Logged in successfully');
        // console.log('Logged in successfully');
      }, error => {
        // Anweisungen, die bei einem Fehler aufgerufen werden
        this.alertify.error(error);
        // console.log(error);
      }, () => {
        // Funktionen, die beim Abschluss (immer) aufgerufen werden
        // (Macht in dieser Komponente so keinen Sinn, dient aber der Veranschaulichung
        // der Möglichkeiten)
        this.router.navigate(['/members']);
      });

      // this.authService.login(this.model).subscribe(next => {
      //   this.alertify.success('Logged in successfully');
      //   // console.log('Logged in successfully');
      // }, error => {
      //   this.alertify.error(error);
      //   // console.log(error);
      // });
  }


  loggedIn() {
    // Nachfolgende Funktion prüft mit Hilfe des Local-Storage,
    // ob ein Objekt unter dem Namen token gespeichert wurde.
    // Die Gütligkeit kann hier nicht geprüft werden.
    // Zugriff auf LocalStorage um die Existenz des Token zu prüfen.
    // Eine Prüfung des Token hinsichtlich der Gültigkeit erfolgt nicht.
    // const token = localStorage.getItem('token');
    // Kurzschreibweise um true oder false zurückzugeben,
    // hinsichtlich der Initialisierung der Variabel.
    // return !!token;

    // Nachfolgendes Statement nutzt die Funktion loggedIn des AuthService
    // um die Existenz und die Gültigkeit des Tokens zu kontrollieren.
    // Diese Funktion ist zu bevorzugen, da sie als Service realisiert ist
    // und eine umfassendere Prüfung mittels des Jwt-Helperservice anbietet.
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    // Auruf der Home-Seite
    this.router.navigate(['/home']);
    // console.log('logged out');
  }
}
