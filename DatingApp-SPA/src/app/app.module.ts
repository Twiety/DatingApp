import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// Das HttpClientModul muss händisch aus nachfolgender der Common-Datei importiert werden.
// Der Import aus der angular/http-Datei wird demnächst nicht mehr unterstützt
import { HttpClientModule} from '@angular/common/http';
import { FormsModule} from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

// Routing-Komponenten
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';

// Start-Komponente der Applikation
import { AppComponent } from './app.component';

// Externe Libraries zur Erweiterung der Html-Controls
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';

// Eigene Services
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { AuthService } from './_services/auth.service';
import { UserService } from './_services/user.service';

// Eigene Komponenten
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';

// Route-Resolver
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';

// Funktion für den HttpInterceptor um das Token bei einem Request
// automatisch der Anfrage hinzuzufügen.
export function tokenGetter() {
   return localStorage.getItem('token');
}

//    providers   --> zu verwendende selbst erstellte Services
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      JwtModule.forRoot({
        config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
        }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService,
      MemberDetailResolver,
      MemberListResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
