import { Injectable } from '@angular/core';
import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { CanDeactivate } from '@angular/router';


// Hiesige Klasse gibt eine Warnung aus, wenn es im Forumlar eine Änderung gab,
// die noch nicht gespeichert wurde und der User auf einen Link in der Navigation klickt.

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent) {
        if(component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost!');
        }
        return true;
    }
}
