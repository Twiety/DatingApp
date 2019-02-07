import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// Das HttpClientModul muss händisch aus nachfolgender der Common-Datei importiert werden.
// Der Import aus der angular/http-Datei wird demnächst nicht mehr unterstützt
import { HttpClientModule} from '@angular/common/http';
import { FormsModule} from '@angular/forms';
import { RouterModule } from '@angular/router';

// Routing-Komponenten
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';

// Start-Komponente der Applikation
import { AppComponent } from './app.component';

// Externe Libraries
import { BsDropdownModule } from 'ngx-bootstrap';

// Eigene Services
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { AuthService } from './_services/auth.service';

// Eigene Komponenten
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';




//    providers   --> zu verwendende selbst erstellte Services
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
