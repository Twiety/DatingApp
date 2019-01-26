import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // values: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.getValues();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  // getValues() {
  //   // Nach der Angabe der aufzurufenden URL erfolgt eine zweiteilige
  //   // Definition von Funktionen:
  //   // 1: Definition einer Funktion bei erfolgreichem HttpClient-Zugriff
  //   // 2: Definition einer Funktion beim Eintreten eines Fehlers
  //   // Der Aufruf von localhost:5000 stellt bei Verwendung des hier verwendeten Server-Ports (4200)
  //   // einen Cross-Domain-Zugriff dar.
  //   // Um diesen Zugriff dennoch zu gestatten, muss in der serverseitigen Applikation eine Anpassung
  //   // hinsichtlich der Zugriffsregeln vorgenommen werden.
  //   this.http.get('http://localhost:5000/api/values').subscribe(response => {
  //     this.values = response;
  //   }, error => {
  //     console.log(error);
  //   });
  // }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
