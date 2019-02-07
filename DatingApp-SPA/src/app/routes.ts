import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './member-list/member-list.component';
import { HomeComponent } from './home/home.component';
import {Routes} from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';



// Definition der Routen
// Die Reihenfolge der zu prüfenden Pfade ist von Relevanz.
// Ein Pfad mit Wildcards (**) muss daher als letztes aufgefühgrt werden.
// path = Name des Pfads/URL
// component = Name der aufzurufenden Komponente
// canActivate = Name der Klasse, die eine Logik enthält, die den Zugriff prüft
// redirect = Pfad zu dem umgeleitet werden soll
// pathMatch = Welcher Bestandteil des Pfades soll geprüft werden

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    // Definition einzelner Routen
    // { path: 'members', component: MemberListComponent, canActivate: [AuthGuard]},
    // { path: 'messages', component: MessagesComponent},
    // { path: 'lists', component: ListsComponent},
    // Definiton mittels multipler Routen
    // Hierbei erfolgt die Anwendung auf alle Child-Elemente
    // Wenn die Path-Angabe des Parent-Elements einen Wert enthält,
    // so wird dieser bei den Child-Elementen als Prefix interpretiert.
    // Beispiel: path: 'dummy'
    //             children - path: 'members'   --> http://localhost/dummymembers/
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent},
            { path: 'messages', component: MessagesComponent},
            { path: 'lists', component: ListsComponent}
        ]
    },
    // Routing für Pfade, die nicht in den obigen Angaben enthalten sind.
    // Die Interpretation der Redirect-Angabe erfolgt dann wieder entsprechend
    // den zuvor angegebenen Routen.
    // Nachfolgend wird daher die erste Route-Angabe angewendet, es kommt
    // somit zu einem Aufruf der Home-Komponente.
    // Achtung: Sollte bei der ersten Route-Angabe und der nachfolgenden Definition
    // kein leerer String angegeben sein. So hat die Applikation ein Problem,
    // wenn die URL keinen Pfad enthält.
    // Ein leerer Pfad, könnte dann trotz der Wildcards in der nachfolgenden
    // Route nicht interpretiert werden. Die Applikation könnte dann keine Komponente anzeigen.
    // Eine Anklicken der Navigation würde dann sogar zu einem Fehler führen.
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
