Zur Erstellung der Applikationen wird folgende Software verwendet
Dotnet Core 2.1
Sqlite
VS Code (mit nachfolgenden Extensions)
--> NuGet
--> C#
--> C# Extensions
--> Angular v7 Snippets
--> Angular Files
--> Angular Language Service
--> angular2-Switcher
--> Auto Rename Tag         (Öffnende und schließende Tags werden automatisch angepasst)
--> Bracket Pair Colorizer  (färbt Klammern unterschiedlich ein)
--> Debugger for Chrome
--> Material Icon Theme     (Icons für Dateitypen)
    => Zum Aktivieren SHIFT+STRG+P   --> Material Icon Theme Activate
--> Path Intellisense
--> Prettier - Code formatter
--> TSLint
Angular CLI     --> npm install -g @angular/cli
Angular 7.x     --> ng new ApplicationName --style css
Bootstrap 4.x   --> npm install Bootstrap
Font-Awesome    --> npm font-awesome
Git             --> https://git-scm.com/download/win


Die Applikation besteht aus zwei Einzel-Applikationen, die in jeweils
eigenen Unterordnern angelgt wurden.

GIT
Das Git-Repository wird zur Vereinfachung so angelegt, dass es beide Applikationen beinhaltet.
Hierzu wird im Prompt das übergeordnete Verzeichnis beider Applikationen aufgerufen.
Anschließend wird mittels dem Befehl "git init" ein neues Repository angelegt.
In der Applikation DatingApp.API wird anschließend (im dortigen Root)
eine neue Datei mit dem Namen .gitignore angelegt.
In dieser Datei werden zeilenweise alle Verzeichnisse und Dateien aufgelistet,
die von Git nicht im Repository zu berücksichtigen sind.
Wildcards (*) können dabei in den Angaben verwendet werden.
Um das Repository zu initialisieren ist wie folgt vorzugehen:
    1) Anklicken des Source Control Buttons in VS Code
    2) Alle vorgeschlagenen Änderungen dem "Staging hinzufügen"
       durch ein Anklicken des "+" Buttons
    3) In der Kommandozeile folgenden Befehl eingeben und mit STRG+ENTER bestätigen
        Initial Commit
    4) Auf der Git-Hub-Seite ist ein neues Repository anzulegen.
       Nach dem dortigen Erstellen wird u.a. dort folgender Link angezeigt,
       der dann innerhalb des Applikationsverzeichnisses am Prompt auszuführen ist:
        git remote add origin https://github.com/Twiety/DatingApp.git
       Anschließend kann dann innerhalb von VS Code (Seite Source-Control)
       ein Push der Dateien zu Github ausgelöst werden.


1. DatingApp.Api (Server-Applikation)
    Hierbei handelt es um eine serverseitige Applikation,
    die für die Client-Applikation eine API zur Verfügung stellt.
    Diese Applikation wurde mittels des folgenden Befehls erstellt:
    dotnet webapi -o DatingApp.API -n DatingApp.API
    --> Erstellt eine DOTNET.CORE Applikation mit dem Namen DatingApp.API
        im Verzeichnis -DatingApp.API


    Das Ausführen der Applikation erfolgt innerhalb des Ordners
    DatingApp.API mit folgendem Befehl:
    dotnet run (oder) dotnet watch run
    
    CodeFirst-Modell
    Zum Erstellen der Datenbank wird das Prinzip "CodeFirst-Modell" angewendet.
    Nachdem eine Klasse erstellt wurde, die durch eine Tabelle in der Datenbank abzubilden ist,
    wird wie folgt vorgegangen.
    - In der Datenkontextklasse wird ein DbSet<> vom Typ dieser Klasse angelegt
    - Erstellen eines Migrationsskriptes mit folgendem Befehl
        dotnet ef migrations add NameDerMigration
    - Anwenden des Migrationsskriptes mit folgendem Befehl
        dotnet ef database update

    Repository-Pattern
    Der Zugriff auf die User-Dasten erfolgt mittels des Repository-Pattern.
    Ein Controller der auf User-Daten zugreifen soll, wird somit von der Logik zum Zugriff
    auf die EF-Daten entkoppelt.
    Hierzu wurde im Ordner Data das Interface IAuthRepository angelegt.
    Das Interface beschreibt die Funktionen, die die konkrete Klasse für
    den Zugriff auf die EF-Daten zur Verfügung stellen muss.
    Die konkrete Klasse wurde ebenfalls im Ordner Data erstellt und
    trägt den Namen AuthRepository.CS und implementiert die zuvor
    genannte Schnittstelle.
    In der StartUp-Klasse der Server-Applikation wird dann die konkrete Klasse
    unter Angabe der Schnittstelle als Service registriert. (siehe Funktion ConfigureServices)
    
    Controller
    Im Verzeichnis Controllers werden C#-Klassen angelegt, deren NameDerMigration
    (per Definition) immer auf Controller endet. Die Klassen und deren Funktionen werden mit speziellen
    Attributen aus dem Namespace Microsoft.AspNetCore.Mvc ausgezeichnet.
    Dies ist notwendig, um zu bestimmen wie die Methoden der Klasse im Rahmen der Web-API
    aufgerufen werden.
    Die eigenen Controller-Klassen müssen von einer der beiden Dot.Core Klassen erben,
    um als Web-Api verwendet zu werden.
    - ControllerBase    --> ausreichend, wenn Klasse als reine Web-Api verwendet werden soll.
    - Controller        --> erforderlich bei Verwendung von serverseitigen Views (MVC)
    
    Data-Transfer-Object (DTO)
    Die im Ordner Models erstellten Objekte können manchmal einen Struktur aufweisen,
    die zum Austausch der Daten zwischen Client und Server ungeeignet ist.
    Manche Attribute (z.B. PasswordHash, PasswordSalt) sollten außerdem ausschließlich
    auf der Serverseite bekannt sein.
    Aus diesem Grund werden sogenannte DTOs erstellt, 

    Datenvalidierung
    Die KLassen der Daten-Objekte (Entitäten) können mit Attributen annotiert werden,
    die eine automatische Validierung der Angaben ermöglichen.
    Voraussetzung ist das der Namespace System.Component.DataAnnotations importier wird.
    Folgende Attribute können beispielhaft gesetzt werden:
    [Required]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "Message")]

    Authentifizierung mittels Token
    Nach einem erfolgreichen Login wird dem Browser JWT-Token zugeschickt.
    Dieses wird dann in allen nachfolgenden Requests verwendet, um den User
    zu authentifizieren. Der Server prüft nur noch das Token, greift aber nicht
    mehr auf die Datenbank zu, um die Zugriffsrecht zu prüfen.
    Das JWT-Token beseht aus drei Teilen:
    - Header    --> Art des Token, verwendete Verschlüsselung
    - Payload   --> zeitlicher Gültigkeitsraum, beliebige Angaben zum User
    - Secret    --> Beinhaltet obige Angaben in gehashter Form, wird allerdings nie zum Client gesendet.
    Zur Verschlüsselung des Scret wird in appsettings.json im Bereich appsettings
    eine Variabel mit dem Namen Token angelegt. Der Value dieser Variabel wird auf
    ein möglichts komplexes Passwort (min 12 Zeichen) gesetzt.
    Anschließend wird ein Token-Descriptor erstellt.
    1) Der Aufbau des Tokens (Bereich Payload) wird durch ein sogenanntes Claim-Array definiert.
    2) Der Key zur Verschlüsselung wird gehashed und zur Erstellung eines Credentials verwendet.
    3) Erstellung eines Token-Descriptors unter Verwendung des Claim-Array, eines Ablaufdatums
       und dem erstellten Credentials.
    Nach dem Erstellen eines Token-Handlers erfolgt nun mit diesem durch die Verwendung
    des Token-Descriptors die eigentliche Erstellung des Tokens.
    Das erstellte Token wird abschließend als Parameter des Return-Code an den aufrufenden Browser
    zurückgegeben. Auf der Website https://jwt.io kann der Inhalt des Token geprüft werden.

    Um einen API-Controller und seine Funktionen vor einem nicht authentifizierten Zugriff zu schützen,
    muss der Controller oder einer seiner Funktionen mit dem Attribut [Authorize] versehen werden.
    Sollte der Controller mit diesem Attribut versehen sein, so ist für alle Funktionen
    nur ein authentifierter Zugriff möglich. Mit dem Attribut [AllowAnonymous] kann eine einzelne
    Funktion wieder für einen nicht authentifierter Zugriff freigegeben werden.

    Damit die Applikation die Authentifizierung überhaupt prüft, ist die start.cs in zwei Punkten anzupassen.
    1) Die Authentifizierungsprüfung ist als Service in der Funktion ConfigureServices zu registrierten.
       In dieser Funktion wird mittels der Anweisung services.AddAuthentication der Service registriert
       und konfiguriert. U.a. wird hier auch auf die Variabel Token in der Datei appsettings.json Verweisen,
       die zuvor zur Verschlüsselung des Token verwendet wurde (siehe oben).
    2) In der Ausgabe-Pipeline (siehe Funktion Configure) ist der Authentifizierungs-Service einzufügen.
       Dabei muss dieser auf jeden Fall, vor der MVC-Funktion aufgeführt werden.

    Abfangen von Fehlern
    Fehler können lokal durch Try-Catch abgefangen und verarbeitet werden.
    Durch die Defintion eines Exception-Handlers in der Datei start.cs kann ein globales Abfangen von Fehlern erfolgen.
    Durch die Berücksichtigung des Ausführungs-Kontextes der Applikation erfolgt die Ausgabe eines vollständigen
    Fehlertextes nur im Entwicklungsmodus. In der Datei Properties/launchSettings.json (Angabe ASPNETCORE_ENVIRONMENT)
    kann bestimmt werden ob die Applikation im Development- oder Production-Modus ausgeführt wird.
    Der Exception-Handler startet einen eigenen Task in dem der aufgetretene Fehler ausgegeben wird.
    Hierbei kann zunächst nur auf die Standardmethoden der Klasse HttpContext zugegriffen werden (also z.B. write, writeAsync).
    Mittels einer Erweiterungsmethode, die als static definiert ist, kann die Klasse HttpContext 
    um neue Funktionen ergänzt werden. Mit Hilfe der Erweiterungsmethode können somit
    auch eigene Header-Angaben definiert werden.
    Durch eigene Header-Angaben kann der irritierende Fehler des Cross-Domain-Zugriffs verhindert werden.
    Auch in der Client-App können Fehler gobal abgefangen werden, 
    siehe hierzu Kapitel "Angular - Globales Abfangen von Fehlern"





2. DatingApp-SPA (Client-Applikation)
    Hierbei handelt es sich um eine clientseitige Single-Page-Applikation (SPA),
    in der Angular verwendet wird.
    Diese Applikation wurde mit Hilfe der Angular.CLI erstellt.
    Zum Installieren der CLI muss folgender Befehl verwendet werden:
    npm install -g @angular/cli
    Die Client-Appliktion kann dann mit folgendem Befehl erstellt werden:
    ng new DatingApp-SPA --style css
    --> Als Style-Vorlage wird gewöhnliches CSS verwendet
        Alternativ kann bei Verzicht auf den Parameter --style
        während des Setups der Applikation die Style-Vorlage ausgewählt werden
    --> Die Option der Installation des Router-Moduls wurde bei der
        Ausführung des Setups der Applikation verneint.

    Das Ausführen der Applikation erfolgt innerhalb des Ordners
    DatingApp-SPA mit folgendem Befehl:
    ng serve  (oder) npm start

    Jede Angular-Applikation muss mindestens ein Angular-Modul besitzen.
    Dieses definiert den Einstiegspunkt (Startpunkt) der Applikation. 
    Zu finden ist dieses Modul im Order app und dort in der Datei
    app.modul.ts
    In diesem Modul ist dann der Verweis auf die erste Komponente (app.component) zu finden,
    die initialisiert werden soll. 
    Weiterhin werden im Modul Provider definiert, die zu einem späteren
    Zeitpunkt der Applikation benötigt werden.
    Außerdem ist im Modul definiert ob Bootstrap verwendet werden soll.

    Die im Modul definierte (erste) Komponente ist in der Datei
    app.component.ts zu finden.
    Innerhalb dieser Datei ist eine JavaScript-Klasse zu finden, dessen
    Dekorator die folgenden drei Informationen beinhaltet.
    1) selector     --> bestimmt das HTML-Tag der Index-Seite, das durch
                        die Logik und das Template der Komponente zu ersetzen ist.
    2) templateUrl  --> Verweis auf das HTML-Template
    3) styleUrls    --> Array mit Verweisen auf die zu verwendenden Stylesheets

    Im Root der Applikation befinden sich die Dateien index.html und main.ts
    Diese HTML-Datei ist (eigentlich) per Definition die einzige HTML-Datei
    der Applikation. Sie beinhaltet das in der obigenen Komponenten definierte
    HTML-Tag. Danebben gibt es nur wenige weitere HTML-Tags.
    Ein Verweis auf JavaScript zur Einbindung des Angular-Frameworks existiert
    tatsächlich nicht in dieser Datei.
    Beim Compilieren der Applikation werden die TypeScript-Dateien nach JavaScript
    übersetzt und mittels Webpack werden die Referenzen zu den Angular-Dateien
    in die Index-Datei injeziert. Die Datei main.ts ist dabei verantwortlich
    in der Index-Datei die für Bootstrap erforderlichen Befehle zur Verfügung
    zu stellen. 
    
    Die von Webpack benötigten Informationen zum Erstellen (Bündeln) der JavaScript-
    Dateien befinden sich in der Datei angular.json.
    U.a. gibt es dort den Verweis auf die relevante HTML-Seite (index.html)
    und die zuvor genannte TypeScript-Datei (main.js).

    Angular - Anlegen einer Komponente
    --> Mittels Context-Menü / Generate Component können Komponenten angelegt werden.
        Dies hat den Vorteil, dass folgende Dinge automatisch erstellt werden.
        - Ordner wird für neue Komponente angelegt
        - Relevante Dateien werden angelegt, diese enthalten die notwendigen Befehle/Tags
        - In main.ts werden die notwendigen Angaben zum Verweis auf die neue Komponente eingefügt.
          (Import-Anweisung und Auflistung im Declartions-Bereich)

    Angular - HttpClient-Moduls
    --> Um Daten von einer Website (Server) abrufen zu können, wird ein HttpClient benötigt.
        Dieser Client wird im Rahmen eines eigenen Angular-Moduls zur Verfügung gestellt.
        Das Modul muss allerdings händisch in der Applikation importiert und als Service zur Verfügung gestellt werden.
        Hierzu sind entsprechende Anweisungen in der Datei main.ts vorzunehmen.
        Import der Datei:
            import { HttpClientModule} from '@angular/common/http';
        Als Service zur Verfügung stellen:
           imports: [ HttpClientModule ],
        
        Achtung: Bisher war dieser Client in der Datei @angular/http enthalten.
        Ab Angular V7 steht diese Datei aber nicht mehr zur Verfügung.
        Innerhalb der Common-Datei ist der Client nun zu finden.

        Um den HttpClient innerhalb einer eigenen Komponente dann nutzen zu können,
        ist es erforderlich innerhalb des Konstrukturs der Komponente den HttpClient
        zu injezieren. Der Komponente ist daher zunächst eine Import-Anweisung hinzuzufügen
        und der Konstruktor ist um den entsprechenden Parameter zu erweitern.

    Bootstrap und eigene Styles einbinden
    --> Zunächst muss Bootstrap mittels NPM importiert werden.
        Ergänzend wird außerdem font-awesome mittels NPM importiert.
        Nach dem Import ist die Datei src/style.css um folgende Zeilen zu ergänzen.
            @import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
            @import '../node_modules/font-awesome/css/font-awesome.min.css';
        Bei Bedarf könnten dann eigene CSS-Dateien hier aufgeführt werden.
        Die Reihenfolge der Zeilen ist dabei von Bedeutung, da nachgelagerte Dateien
        die Angaben in zuvor genannten Dateien überschreiben.
    Folgende Beispielseite wird als Grundlage für die Navigation verwendet.
    https://getbootstrap.com/docs/4.2/examples/jumbotron/
    Aus dem Quellcode dieser Seite wurde die Navigation (Tag-Nav) in die Zwischenablage kopiert.
    In der Applikation DatingApp-SPA wurde dann eine neue Komponente mit dem Namen Nav angelegt.
    Visual Code erstellt dann einen Ordner mit dem Namen Nav, legt die entsprechenden Dateien an
    und fügt in der Datei app.module.ts die entsprechenden Verweise ein.
    In die Datei nav.component.html wird anschließend der Inhalt der Zwischenablage kopiert.
    Der eingefügte HTML-Code wurde abschließend auf das Wesentlichste reduziert und an die 
    gewünschte Struktur und Namensgebung angepasst.
    Hinweis: Das oberste Tag enthält die CSS-Anweisung fixed-top, welche allerdings dazu führt,
    dass nachfolgende HTML-Tags (wie z.B. die Headline der Applikation) im oberen Bereich
    durch die Navigation verdeckt werden.

    Angular-Formulare
    1) Damit Angular mit Formularen umgehen kann, sind zunächst in app.module.ts
       Anweisungen zum Import von FormsModule einzufügen.

    2) Ts-Datei der Komponente wie folgt anzupassen
        --> Anlegen einer Funktion zur Verarbeitung der Formular-Eingaben
        --> Variabel zur Aufnahme des Objekts anlegen (nachfolgend als varObjectName bezeichnet)

    3) Das Html-Formular ist wie folgt anzupassen:
        - Attribute dem Forumlar-Tag hinzufügen 
            -     #nameDesForms="ngForm"
            -     (ngSubmit)="formFunction()"
        - Attribute den Eingabe-Controls hinzufügen
            - name="NameDesControls"
            - required               
              --> wenn Eingabe zwingend erforderlich ist
            - [(ngModel)]="varObjectName.attributname"
              --> Verbindet das Control mit dem Datenmodell
              --> mittels Code-Snippet a-N kann die Eingabe erleichtert werden
            - #VariabelName="ngModel" 
            --> Diese Angabe ist NICHT erforderlich, dient aber einem einfacheren Zugriff auf das Control
            --> Mit Hilfe dieser Angabe wird eine Variabel erstellt,
                die ebenfalls an das Datenmodell und das Control gebunden wird.
                Der Control-Value wird damit automatisch in diese Variabel übertragen
            --> Mit folgendem Code könnte dann auf den Inhalt des Controls auch außerhalb
                des Formulars zugegriffen werden.
                {{VariabelName.value}}
        - Attribut dem Button hinzufügen
            - [disabled]="!nameDesForms.valid"
            --> Durch die Angabe, wird der Button erst aktiviert, wenn die erforderlichen
                Angaben für das Formular vollständig sind.

    Angular-Service
    Um Funktionen innerhalb von Angular wieder verwenden zu können, ist es ratsam diese
    Funktionen als Service zu definieren. Ein Service kann dann mit seiner Funktionalität
    in die Komponente injeziert werden.
    Die Erstellung eines Service in Angular sieht wie folgt aus:
    - Erstellung eines eigenen Verzeichnisses zur Aufnahme der Services (optional)
    - Erstellung des Service mittels des entsprechenden Kontext-Menüs von VS-Code
    --> Es werden entsprechende Template-Dateien erstellt.
        Entscheidend für einen Service ist der Dekorator in den angelegten Dateien.
        Dieser kennzeichnet die Datei als injezierbar. Hierbei wird auch angegeben,
        in welchem Modul der Service injeziert werden kann. (Standard Root)
    - In der Datei app.modul.ts ist der Bereich providers (Array) um den Eintrag
      des neuen Service zu ergänzen..
      Da es sich um einen lokalen Service handelt, wird VS-Code automatisch
      eine entsprechende Import-Anweisung in app.modul.ts anlegen.
    - Die Anwendung des Service innerhalb eine Komponente gestaltet sich wie folgt:
        --> Der Konstruktor der Komponente muss eine Variabel vom Typ des erstellten
            Service entgegennehmen. Hierzu ist außerdem eine entsprechend Import-Anweisung
            für die Komponente zu erstellen. VS-Code erstellt diese automatisch,
            allerdings kann die Pfadangabe fehlerhaft sein.

    Angular - Strukturelle Direktive
    Html-Tags können mit einer strukturellen Direktive versehen werden.
    Kennzeichnen für eine Direktive ist der führende * in der Bezeichnung.
    In Abhängikeit der Expression, die der Direktive folgt, nimmt Angular Einfluss auf das
    hiermit ausgezeichnete Tag und alle untergeordneten DOM-Objekte.
    *ngIf='functionName'  --> In Abhängigkeit des zurückgegebenen Wertes der angegebenen Funktion
                              (true/false) wird das betreffende Html-Tag ein- bzw. ausgeblendet.

    Angular - Kommunikation zwischen Komponenten
    1) Parent zu Child
        Um einen Datenaustausch von einer Parent- zu einer Child-Komponente zu ermöglichen,
        muus in der Child-Komponente eine Variabel mit dem @Input-Dekorator versehen werden.
        Zur Verwendung dieses Dekorator muss die Import-Anweisung in TS-Datei der Child-Komponente
        angepasst werden. 
            import { Component, OnInit, Input } from '@angular/core';
        In der TS-Datei wird dann eine Variabel zum Datenaustausch dann wie folgt angelegt.
            @Input() valuesFromParent: any;
        Die Parent-Komponente wird hinsichtlich des Tags zur Einbindung der Child-Komponente
        so angepasst, dass in dem Tag die zuvor zum Input definierte Variabel aufgeführt wird.
        Beispiel:
        <app-childcomp [valuesFromParent]="values"></app-childcomp>
        Die Parent-Komponente wird somit den Inhalt von values mittels einer eigenen Logik
        laden und der Variabel in Child-Komponente zuweisen.
    
    2) Child zu Parent
        Hierzu wird zunächst in der Child-Komponente eine Variable mit dem Dekorator @Output versehen.
        Der Typ der Variabel wird auf EventEmitter gesetzt.
          @Output() eventFromChild = new EventEmitter();
        Sowohl der Output-Dekorator als auch der Variabelntyp EventEmitter benötigen eine Anpassung
        der Import-Anweisung. Beide Namespaces befinden sich wie zuvor der Input-Dekorator in der Datei @angular/core.
        Die vollständige Import-Anweisung sieht somit wie folgt aus:
            import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
        In der Parent-Komponente müssen dann zwei Anpassungen vorgenommen werden.
        Das Tag zur Einbindung der Child-Komponente ist wie folgt anzupassen:
        (eventFromChild)="myFunctionInParent($event)"
        In der TS-Datei der Parent-Komponente ist eine Funktion zur Verarbeitung des Events anzulegen.
        (In diesem Fall wäre diese mit myFunctionInParent zu bezeichnen)

-----------------------------------------
Angular - globales Abfangen von Fehlern
-----------------------------------------
Zum globalen Abfangen von Fehlern wird eine Interceptor-Klasse eingerichtet,
die den Response-Stream analysiert.
Diese Klasse (siehe Verzeichnis _services) importiert u.a. den Namenspace HttpInterceptor.
und muss als injectable gekennzeichnet werden.
In dieser Klasse werden dediziert die Rückgabe-Header analysiert. 
Beim Auftreten spezieller Header werden dann in unterschiedlichen If-Blöcken
entsprechende Header-Informationen zur Ausgabe der eigentlichen Fehlermeldung verwendet.
Danach ist ein Provider zu erstellen (Objekt-Variabel), der auf die zuvor erstelte Klasse verweist.
Dieser Provider ist dann in app.modul.ts zu registrieren. Hierzu wird der neue Provider namentlich
im Provider-Array eingefügt. Außerdem ist eine entsprechende Import-Anweisung in app.modul.ts einzufügen.

-----------------------------------------
Alertify
-----------------------------------------
Dies ist eine JavaScript Bibliothek um Frontend-Dialoge zu erstellen.
Die Installation erfolgt innerhalb der Angular-Applikation mit dem Befehl
    npm install alertifyjs --save
Damit der Build-Prozess der Angular-Applikation dieses Framework berücksichtigt
ist die Datei angular.json anzupassen. In dieser Datei ist im Bereich Scripts der Pfad
zur JavaScript-Bibliothek anzugeben.
Damit auch die zugehörigen CSS-Styles berücksichtigt werden ist die Datei
src/styles.css anzupassen. In dieser ist ein Verweis auf die Standard-CSS und ein Verweis
auf das Bootstrap-Theme dieser Bibliothek einzufügen.
Die Verwendung dieser Bibliothek erfolgt im Rahmen eines Angular-Service.
Dieser Service dient als Wrapper zum Aufruf der Funktionen dieser Bibliothek.
Alternativ könnte diese Bibliothek auch durch entsprechende Befehle direkt verwendet werden.
Wie jeder Service muss auch dieser in app.moduel.ts registriert werden.
Hierzu ist daher wie immer das Providers-Array und die Import-Anweisung zu ergänzen.
Um den Service in einer Komponente nutzen zu können, ist der Service (wie gewohnt)
im Konstruktor der Komponente zu übergeben.
Innerhalb der Funktion der Komponente können dann die Methoden des Service und somit
die Funktion von Alertify.js genuitzt werden.

-----------------------------------------
Auth0/angular-jwt
-----------------------------------------
Um die Token des Servers zu validieren wird die zuvor genannte Bibliothek verwendet
    https://github.com/auth0/angular2-jwt
Die Installation erfolgt mittels NPM im DatingApp-SPA Ordner
    npm install @auth0/angular-jwt

Um eine zentrale Validierung des Token zu ermöglichen, wird der AuthService um eine Funktion
zur Validierung des Token erweitert (siehe Funktion loggedIn() in auth.service.ts).
Zur Anzeige, ob der User eingeloggt ist oder nicht, verwendet die Navigations-Komponente dann
diese vom Service angebotene Funktion. Das HTML-Template der Navigations-Komponente könnte dann
mittels Interpolation auf einzelne Eigenschaften des Token zugreifen.
    {{authService.decodedToken.unique_name}}
Hierbei gibt es allerdings eine Reihe von Problemen.
- Der authService wird der Komponente als privates Objekt injeziert.
  VS Code bemängelt dies, eine Ausführung ist allerdings trotzdem möglich.
  Dies begründet sich in der Tatsache, dass in JavaScript (im Gegensatz zu Typescript) 
  keine Unterscheidung zwischen public und private Variabeln existiert.
  Durch die Angabe von public im Konstruktor kann dieses Problem behoben werden.
- Durch die Angabe des Fragezeichens in der Interpolations-Answeisung ist die Existenz der Property optional.
    {{authService.decodedToken?.unique_name}}
  Bei einem Seiten-Refresh kommt somit zu keiner Fehlermeldung während der Laufzeit,
  die Interpolation gibt dann aber auch nur noch einen leeren String aus
  
  Dies begründet sich durch die Struktur der Login-Methode.
  Die Login-Funktion der Komponente und somit der Service werden nur beim Anklicken des Login-Buttons
  aufgerufen. Somit wird auch nur in diesem Fall der Wert der Variabel decodedToken gesetzt.
  Bei einem Seiten-Refresh steht der Inhalt nicht mehr zur Verfügung.
  Durch eine Anpassung der Start-Komponente (also app.component) kann bereits beim Initialisieren
  der Applikation das Token, wenn vorhanden, geladen werden.
  Hierzu sind folgende Anpassungen in app.component.ts vorzunehmen.

  - Anpassung der Klassendefintion
    export class AppComponent implements OnInit ...
  - Anpassung der Import-Anweisung um den Namespace OnInit, AuthService und JwtHelperService zu importieren.
  - Konstruktor anlegen, in dem der AuthService in die Komponente injeziert wird
  - Funktion ngOnInit() anlegen. In diesem wird zunächst das Token aus dem LocalStorage geladen.
    Wenn dieses existiert, wird mittels des JwtHelper-Service das Token dekodiert
    und in der Variabel decodedToken im AuthService gespeichert.

-----------------------------------------
ngx-bootstrap    
-----------------------------------------
Um das Einbinden von Bootstrap und eigenen Templates zu vereinfachen, wird ngx-bootstrap mittel npm installiert.
    https://valor-software.com
Durch diese Bibliothek kann außerdem auf jquery verzichtet werden, welches eine Grundlage für Bootstrap ist.
    npm install -ngx-bootstrap --save
Nach dem Installieren der Bibliothek muss die Datei app.module.ts hinsichtlich des Imports und
der Verwendung angepasst werden. Der Import sollte dabei vor den eigenen Komponenten erfolgen.
    import { BsDropdownModule } from 'ngx-bootstrap';  
Anpassung des Import-Arrays
    BsDropdownModule.forRoot()  

DropdownMenu (ngx-bootstrap Komponente)
Um ein Dropdown-Menü mittels dieser Bibliothek zu realisieren, sind drei Anpassungen
im Bereich des HTML-Templates vorzunehmen.
1.  Der Bereich auf den sich dieses Modul beziehen soll, ist hinsichtlich des ersten Tags
    anzupassen. Diese beinhaltet alle nachfolgenden Tags.
    Im öffnenden Tag ist das Attribut dropdown einzufügen.
    Bei Bedarf können hier verschiedene Events des Dropdown-Menüs definiert werden.
    Beispiel:
    <span dropdown (onShown)="onShown()" (onHidden)="onHidden()" (isOpenChange)="isOpenChange()">
        ...
    </span>
2.  Der Button/Link der für das Öffnen des Menüs zuständig ist, wird
    mit dem Attribut dropdownToggle versehen.
    Um das Standardverhalten des Anker-Tags zu deaktivieren kann außerdem
    folgender Code eingetragen werden.
    (click)="false"
    Beispiel für ein Anker-Tag
    <a href id="basic-link" dropdownToggle (click)="false">Link</a>
3.  Das eigentliche Dropdown-Menü, welches eingeblendet werden soll,
    wird dann um die Seiten-Direktive *dropdownMenu ergänzt.
    Beispiel:
    <ul id="basic-link-dropdown" *dropdownMenu class="dropdown-menu">...</ul>        

-----------------------------------------
Theme für Bootstrap
-----------------------------------------
Auf folgender Seite befinden sich kostenlose Bootstrap-Themes
    https://bootswatch.com/
Folgende NPM-Anweisung installiert ein entsprechendes Paket in dem
alle Themes enthalten sind. Zu finden sind diese dann im Ordner node_modules/bootswatch
    npm install bootswatch

Im zentalen CSS-File (src/style.css)  der Applikation kann dann das gewünschte Theme durch
das Einfügen einer neuen Import-Anweisung eingebunden werden.    
Beispiel:
    @import '../node_modules/bootswatch/dist/united/bootstrap.min.css';
  

-----------------------------------------
Angular Routes
-----------------------------------------  
Da es sich um eine Singe Page Application handelt, wird der Austausch der Seiten
durch eine Logik von Angular durchgeführt. 
Hierzu ist eine TS-Datei im Ordner app mit dem Namen routes.ts zu erstellen, die das Routing steuert.
In dieser Datei ist zunächst der Namespace Routes auf @angular/router zu importieren.
Anschließend wird eine Konstante vom Typ Routes definiert.
Dieser wird dann in Form eines Arrays eine Liste von Rout-Objekten zugewiesen.
Das Rout-Objekt besteht zumindest aus den Angaben des Rout-Namen und der aufzurufenden Komponente.
Für jede aufgeführte Komponente muss wie gewöhnlich eine zugehörige Import-Anweisung in der Router-Datei erfolgen.
Nach dem Auflisten aller relevanten Komponenten erfolgt auch eine Rout-Angabe, die Anwendung findet,
wenn durch den User eine Route aufgerufen wird, die nicht definiert ist.
Die Reihenfolge der Rout-Angaben ist grundsätzlich von Bedeutung, denn die erste Übereinstimmung
der angegebenen Route führt zum Aufruf der definierten Komponente.
Beispiel:

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    { path: '**', redirectTo: 'home', pathMatch: 'full'}
]

Als nächstes muss in app.module.ts das Imports-Array wie folgt erweitert werden.
    RouterModule.forRoot(appRoutes)
Hierfür werden die nachfolgenden zwei Import-Anweisungen benötigt:
    import { RouterModule } from '@angular/router';
    import { appRoutes } from './routes';

Die Routing-Angaben können dann in einem A-Tag wie folgt verwendet werden.
(Snippet a-routerlink zur schnelleren Eingabe verwenden)
    <a [routerLink]="['/routePath']" routerLinkActive="router-link-active" >Dating-App</a>
Die Angabe /routePath ist durch den Namen der gewünschten Route zu ersetzen.
Das Attribut routerLinkActive führt zur Anwendung eines speziellen Stylesheets, wenn die Route ausgewählt wurde.
Diese Angabe kann entweder gelöscht oder zu einem übergeordneten Tag verschoben werden.
Damit der Austausch der Seite tatsächlich erfolgt muss abschließend noch eine Anpassung der app.component.html
vorgenommen werden.
Das bisher dort vorhandene Tag <app-home></app-home>, welches zur Anzeige Home-Komponente führte,
ist nun durch folgendes Tag zu ersetzen. Dieses Tag berücksichtigt, dynamische die Auswahl
der jeweiligen Komponente.
    <router-outlet></router-outlet>
    
Damit eine Komponente durch eigene Logik den Inhalt der Seite austauschen kann,
ist es erforderlich die Routing-Objekt in die jeweilige Komponente mittels Übergabe
an den Konstruktor zu injezieren.
In der hiesigen Applikation passiert dies z.B. in der Navigations-Komponente.

-----------------------------------------
Route-Guards
-----------------------------------------
Seiten können vor einem nicht authentifierten Zugriff geschützt werden,
indem eine Route-Guard eingerichtet wird.
VS Code bietet an dieser Stelle leider kein entsprechendes Context-Menü zum
Anlegen einer solchen Komponente an. Aus diesem Grund ist das Erstellen mittels
Angular-CLI notwendig. Hierzu ist folgender Befehl im gewünschten Verzeichnis auszuführen:
ng g guard nameofgurad --spec=false
    --> Parameter --spec=false verhindert das Erstellen eines zugehörigen Testfiles.

In der Guard-Komponente wird definiert, was zu prüfen ist und was passieren soll,
wenn die Prüfung zu einem Fehler führte.
Im Rahmen dieser Applikation wird z.B. geprüft, ob der User eingeloggt ist,
sollte das nicht der Fall sein, so wird mittels alterify eine Fehlermeldung ausgegeben
und es erfolgt eine Umlzeitung zur Startseite.

Die Guard-Komponente muss dann als nächste im app.module eingebunden werden.
Hierzu wird das Providers-Array um einen entsprechenden Eintrag ergänzt.
Wie immer muss auch hierfür eine Import-Anweisung aufgenommen werden, die auf
die zuvor erstelle Guard-Komponente verweist.
Beispiel:
    import { AuthGuard } from './_guards/auth.guard';

Als nächstes muss dann die Router-Komponente (routes.ts) so angepasst werden,
dass sie die Guard-Komponente zum Prüfen der aufgerufenen Route verwendet.
Beispiel:
    { path: 'members', component: MemberListComponent, canActivate: [AuthGuard]},

Jede Route kann entweder einzeln oder mittels einer Multiple-Route definiert werden.
Bei einer Multiple-Route wird die anzuwendende Klasse nur einmal vorgegeben.
Die Child-Element enthalten dann die einzelnen Routen, wenden aber die im Parent-Element
angegebene Klasse zum Prüfen der Zugriffsrecht an.
Im Parent-Element kann ergänzend eine Pfad-Angabe definiert werden, die beim Prüfen
der Child-Elemente als Prefix der Pfadangabe verwendet wird. Im Standardfall ist der Pfad
des Parent-Elementes daher eher nur ein leerer String.