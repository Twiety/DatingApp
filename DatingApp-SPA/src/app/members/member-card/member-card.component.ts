// Achtung: Da diese Datei sich in einer Ordnerstruktur befindet,
//          die nicht dem Standard entspricht (Komponenten-Ordner direkt unterhalb des app-Ordners)
//          ist Angular nicht in der Lage, die Referenz zu dieser Komponente automatisch
//          in app.module.ts einzutragen. Somit kommt es erstmal auch in dieser Datei zu einem
//          Problem hinsichtlich der Referenzierung.
//          Abhilfe schafft hier ein manuelles Eintrage der Komponente in app.module.ts.
import { Component, Input, OnInit } from '@angular/core';
import { User } from './../../_models/user';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  // Nachfolgende Variabel wird mit dem @Input-Dekorator gekennzeichnet
  // damit der Inhalt von der übergeordneten Komponente an diese hier übergeben werden kann.
  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
