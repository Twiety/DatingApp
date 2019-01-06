import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// Das HttpClientModul muss händisch aus nachfolgender der Common-Datei importiert werden.
// Der Import aus der angular/http-Datei wird demnächst nicht mehr unterstützt
import { HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
