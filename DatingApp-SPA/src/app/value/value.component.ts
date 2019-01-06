import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any;

  // Konstruktor der Klasse
  constructor(private http: HttpClient) { }

  // Nach der Initialisierung wird automatisch folgende Funktion von Angular aufgerufen
  ngOnInit() {
    this.getValues();
  }

// Funktion zum Laden der Daten mittels HttpClient-Zugriff
  getValues() {
    // Nach der Angabe der aufzurufenden URL erfolgt eine zweiteilige
    // Definition von Funktionen:
    // 1: Definition einer Funktion bei erfolgreichem HttpClient-Zugriff
    // 2: Definition einer Funktion beim Eintreten eines Fehlers
    // Der Aufruf von localhost:5000 stellt bei Verwendung des hier verwendeten Server-Ports (4200)
    // einen Cross-Domain-Zugriff dar.
    // Um diesen Zugriff dennoch zu gestatten, muss in der serverseitigen Applikation eine Anpassung
    // hinsichtlich der Zugriffsregeln vorgenommen werden.
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

}
