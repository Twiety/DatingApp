import { AlertifyService } from './../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from './../../_models/user';
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  // Nachfolgende Anweisung ermöglicht den Zugriff auf die Attribute des Forumlars
  // im Rahmen einer Two-Way-Datenbindung
  @ViewChild('editForm') editForm: NgForm;

  user: User;

  // HostListener ermöglicht das Definieren von Events, die sich außerhalb
  // der Angular-Applikation (z.B. auf Windows-Ebene) befinden.
  @HostListener('window:beforeunload',['$event'])

  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  updateUser() {
    console.log(this.user);
    this.alertify.success('updated successfully');
    // Status des Formulars zurücksetzen (u.a. wird der DirtyRead-Modus wieder eliminiert)
    // Ohne Angabe eines Parameters in der Reset-Anweisung, wird allerdings auch der Inhalt des Formulars gelöscht
    this.editForm.reset(this.user);
    // this.editForm.reset();
  }
}
