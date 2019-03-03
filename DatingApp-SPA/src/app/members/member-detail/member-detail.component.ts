import { AlertifyService } from './../../_services/alertify.service';
import { UserService } from './../../_services/user.service';
import { User } from './../../_models/user';
// Achtung: Da diese Datei sich in einer Ordnerstruktur befindet,
//          die nicht dem Standard entspricht (Komponenten-Ordner direkt unterhalb des app-Ordners)
//          ist Angular nicht in der Lage, die Referenz zu dieser Komponente automatisch
//          in app.module.ts einzutragen. Somit kommt es erstmal auch in dieser Datei zu einem
//          Problem hinsichtlich der Referenzierung.
//          Abhilfe schafft hier ein manuelles Eintrage der Komponente in app.module.ts.
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];


  constructor(private userService: UserService, private altertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
     // this.loadUser();

     // Konfiguration der Ngx-Gallery
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];

    // Inhalt der Ngx-Gallery
   this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for(let i=0; i < this.user.photos.length; i++) {
      imageUrls.push({
        small: this.user.photos[i].url,
        medium: this.user.photos[i].url,
        big: this.user.photos[i].url,
        description: this.user.photos[i].description
      });
    }

    return imageUrls;
  }

  // Funktion ist durch die Verwendung des Route-Resolvers nicht mehr notwendig!
  // loadUser() {
  //   // Der Aufruf eines Users erfolgt mittels einer URL, die wie folgt aufgebaut ist.
  //   // members/id
  //   // Die Id des Users kann daher aus der URL extrahiert werden.
  //   // Hierzu ist die Klasse ActivatedRoute in die hiesige Komponente zu injezieren.
  //   // Die Anweisung this.rout.snapshot.params['id'] ermittelt die ID aus der URL.
  //   // Die Rückgabe erfolgt dabei allerdings als String. Die Funktion getUser erwartet
  //   // allerdings eine Zahl.
  //   // Durch die Voranstellung des + wird der String in eine Zahl komvertiert.
  //   this.userService.getUser(+this.route.snapshot.params['id']).subscribe((user: User) => {
  //     this.user = user;
  //   }, error => {
  //     this.altertify.error(error);
  //   });
  // }
}
