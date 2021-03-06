import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// Die hiesige Klasse implementiert die Resolver-Klasse.
// Aus diesem Grund muss eine Funktion mit dem Namen resolve angelegt werden.
// Diese muss die aktive Route als Parameter entgegennehmmen und als Ergebnis
// ein Observable vom Typ der jeweiligen Datenklasse zurückliefern.
// Das sonst übliche Subscribe zum Laden eines Observables erfolgt hier nicht.
// Dies passiert in der Komponente, in der die zu ladenden Daten angezeigt werden sollen.
@Injectable()
export class MemberDetailResolver implements Resolve<User> {
    constructor(private userService: UserService,
        private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUser(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
