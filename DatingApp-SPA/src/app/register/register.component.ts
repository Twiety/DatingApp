import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Ermöglicht den Datenaustausch zwischen der übergeordneten
  // Komponente (parent) und dieser Komponente (child)
  // @Input() valuesFromHome: any;

  // EventEmitter um mittels des Auslösens eines Events
  // einen Wert bzw. ein Objekt von der Child-Komponente zur Parent-Komponente
  // zu übertragen.
  @Output() cancelRegisterHome = new EventEmitter();

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registration successfull');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    // Verwendet den oben genannten EventEmitter um einen Wert/Objekt (in diesem Fall false)
    // an die übergeordnete Komponente zu übertragen.
    this.cancelRegisterHome.emit(false);
    console.log('canceld');
  }
}
