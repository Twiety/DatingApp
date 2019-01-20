import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// Das HttpClientModul muss händisch aus nachfolgender der Common-Datei importiert werden.
// Der Import aus der angular/http-Datei wird demnächst nicht mehr unterstützt
import { HttpClientModule} from '@angular/common/http';
import { FormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';

// Nachfolgender Dekorator definiert folgende Arrays
//    declaration --> zu verwendende Komponenten
//    import --> zu importierende Angular-Funktionen
//    providers   --> zu verwendende selbst erstellte Services
@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
