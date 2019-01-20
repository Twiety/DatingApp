import { AuthService } from '../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {

    // Dem definierten Login-Service wird das hiesige Datenmodell (model) übergeben.
    // Da der Service ein Observable zurückgibt,
    // ist mittels Subscribe-Funktion zu definieren,
    // wie ein positiver und ein negativer Response zu verarbeiten ist.

    this.authService.login(this.model).subscribe(next => {
        console.log('Logged in successfully');
      }, error => {
        console.log('Failed to login');
      });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // Kurzschreibweise um true oder false zurückzugeben,
    // hinsichtlich der Initialisierung der Variabel.
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
