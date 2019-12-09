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
Angular CLI     	--> npm install -g @angular/cli
Angular 7.x     	--> ng new ApplicationName --style css
Bootstrap 4.x   	--> npm install bootstrap
Font-Awesome    	--> npm install font-awesome
Alertify		    --> npm install alertifyjs --save
@auth0/angular-jwt	--> npm install @auth0/angular-jwt --save
ngx-bootstrap		--> npm install ngx-bootstrap --save
Boostrap-Theme		--> npm install bootswatch
ngx-gallery		    --> npm install ngx-gallery --save
Git             	--> https://git-scm.com/download/win

------------------------------------------
NPM Befehle
------------------------------------------
npm install Paket -g 	  --> globales installieren eines Paketes
npm install Paket -save   --> Installieren eines Paketes innerhalb eines Projektes
npm uninstall Paket -save --> Deinstallieren eines Paktes eines Projektes
npm ls Paket         	  --> Abhängigkeiten eines Paketes anzeigen
npm show Paket version 	  --> Anzeigen der verwendeten Version des Paketes
npm audit                 --> Prüft, ob bekannte Sicherheitslücken existieren
npm audit fix             --> versucht die Sicherheitslücken zu schließen

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

    ------------------------------------------
    Anlegen und starten der Server-Applikation
    ------------------------------------------
    Hierbei handelt es um eine serverseitige Applikation,
    die für die Client-Applikation eine API zur Verfügung stellt.
    Diese Applikation wurde mittels des folgenden Befehls erstellt:
    dotnet webapi -o DatingApp.API -n DatingApp.API
    --> Erstellt eine DOTNET.CORE Applikation mit dem Namen DatingApp.API
        im Verzeichnis -DatingApp.API


    Das Ausführen der Applikation erfolgt innerhalb des Ordners
    DatingApp.API mit folgendem Befehl:
    dotnet run (oder) dotnet watch run

    ------------------------------------------    
    CodeFirst-Modell
    ------------------------------------------    
    Zum Erstellen der Datenbank wird das Prinzip "CodeFirst-Modell" angewendet.
    Nachdem eine Klasse erstellt wurde, die durch eine Tabelle in der Datenbank abzubilden ist,
    wird wie folgt vorgegangen.
    - In der Datenkontextklasse wird ein DbSet<> vom Typ dieser Klasse angelegt
    - Erstellen eines Migrationsskriptes mit folgendem Befehl
        dotnet ef migrations add NameDerMigration
    - Anwenden des Migrationsskriptes mit folgendem Befehl
        dotnet ef database update

    ------------------------------------------
    Repository-Pattern
    ------------------------------------------    
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

    ------------------------------------------    
    Controller
    ------------------------------------------
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

    ------------------------------------------
    Datenvalidierung
    ------------------------------------------    
    Die KLassen der Daten-Objekte (Entitäten) können mit Attributen annotiert werden,
    die eine automatische Validierung der Angaben ermöglichen.
    Voraussetzung ist das der Namespace System.Component.DataAnnotations importier wird.
    Folgende Attribute können beispielhaft gesetzt werden:
    [Required]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "Message")]

    ------------------------------------------
    Authentifizierung mittels Token
    ------------------------------------------    
    Nach einem erfolgreichen Login wird dem Browser JWT-Token zugeschickt.
    Dieses wird dann in allen nachfolgenden Requests verwendet, um den User
    zu authentifizieren. Der Server prüft nur noch das Token, greift aber nicht
    mehr auf die Datenbank zu, um die Zugriffsrecht zu prüfen.
    Das JWT-Token beseht aus drei Teilen:
    - Header    --> Art des Token, verwendete Verschlüsselung
    - Payload   --> zeitlicher Gültigkeitsraum, beliebige Angaben zum User
    - Secret    --> Beinhaltet obige Angaben in gehashter Form, wird allerdings nie zum Client gesendet.
    Zur Verschlüsselung des Secret wird in appsettings.json im Bereich appsettings
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
       und konfiguriert. U.a. wird hier auch auf die Variabel Token in der Datei appsettings.json verwiesen,
       die zuvor zur Verschlüsselung des Token verwendet wurde (siehe oben).
    2) In der Ausgabe-Pipeline (siehe Funktion Configure) ist der Authentifizierungs-Service einzufügen.
       Dieser Service muss auf jeden Fall vor der MVC-Funktion aufgeführt werden, da ansonsten der
       Zugriff auf die Controller nicht geprüft wird.

    ------------------------------------------
    Abfangen von Fehlern
    ------------------------------------------
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

    ------------------------------------------
    Debuggen einer Anwendung in VSCode (siehe Kapilet 3/30)
    ------------------------------------------
    Vorbereitung
        - Button Debugger anklicken
        - Debug-Konfiguration hinzufügen
    Debuggen bei laufender Anwendung
        - Hinzugefügte Konfiguration auswählen
        - Auf Start-Button klicken
        - Thread auswählen

    ------------------------------------------
    CLI Entity-Framework
    ------------------------------------------
    dotnet ef migrations add NameOfMigration
    --> Erstellt ein neues Migrationsscript 

    donet ef database update
    --> Anwendung des/der letzten Migrationsscripte(s),
        welche noch nicht gegenüber der Datenbank ausgeführt wurden.

    dotnet ef database update NameOfMigration
    --> Anwendung des angegebenen Migrationsscriptes.
        Durch die Angabe des Scriptes kann auch zu
        einem älteren Versionsstand zurück gegangen werden.
        Achtung: Von SQLite werden nicht alle Befehle unterstützt,
                 die notwendig wären für ein DownGrade der Datenbank.

    dotnet ef migrations list
    --> Auflistung der vorhandenen Migrationsscripte
        Welche Scripte auf die Datenbank angewendet wurden, sieht man nur in der Datenbank

    dotnet ef migrations remove
    --> Löscht das letzte Migrationsscript, wenn dieses noch nicht
        angewendet wurde. Andernfalls wird ein Fehler ausgegeben.
        

    dotnet ef database drop
    --> LÖscht die Datenbank

    ------------------------------------------
    Zufallsdaten für Datenbank
    ------------------------------------------
    Auf folgender Webseite können Zufallsdaten produziert werden.
    https://www.json-generator.com
    Das Script zum Erstellen der Daten kann an die eigenen Anforderungen angepasst werden.
    Nach dem Erstellen werden die Daten in die Zwischenablage kopiert und in
    eine JSON-Datei eingefügt. (siehe Datei Data\UserSeedData.json)
    Außerdem wird eine Klasse mit dem Namen Seed angelegt, die diese JSON-Datei einliest
    und für jedes Element ein Objekt vom Typ User angelegt und dem Datenkontext hinzufügt.
    Nach dem Anlegen aller Objekt wird abschließend der Befehl _context.SaveChanges() ausgeführt.
    Dieser Befehl führt dann zum Speichern der Daten in der Datenbank.
    Der Aufruf dieser Klasse erfolgt durch eine Anpassung der Start.cs-Datei.
    Zunächst wird dort ein entsprechender Service definiert, der dann
    der Configure-Methode injeziert wird. Dort wird dann die Funktion zum Anlegen der Daten
    innerhal der Seed-Klasse aufgerufen.
    Nach dem Anlegen der Daten wird diese Funktion wieder auskommentiert,
    andernfalls würde jeder Start der Applikation zum erneuten Anlegen der Daten führen.

    ------------------------------------------
    Auto-Mapper
    ------------------------------------------
    Zum Installieren wie folgt in VS-Code vorgehen:
    CMD+SHIFT+P NuGet
        Suche nach AutoMapper  
        Auswahl von AutoMapper.Extensions.Microsoft.DependencyInjection

    Wie gewöhnlich muss auch dieser Service innerhalb der Start-Klasse der Applikation (start.cs)
    konfiguriert werden.
        services.AddAutoMapper();

    Das Tool wird wie folgt verwendet:
    1)  Zunächst ist mittels des Konstruktors des Controllers 
        ein Mapping-Objekt vom Typ der Schnittstelle IMapper zu injezieren und in einer
        Variabel des Controllers zu speichern.

    2) In der Controller-Funktion zur Ausgabe eines Objektes erfolgt dann mittels
       des Mapping-Objektes die Zuweisung der Eigenschaften des Source-Objektes
       zu den Eigenschaften des Destination-Objektes wie folgt:

        Mapping bei einem einzelnen Objekt
            var DestinationObject = _mapper.Map<DestinationClass>(SourceObject);

        Mapping bei einer Liste vom Typ IEnumerable von Objekten
            var DestinationList = _mapper.Map<IEnumerable<DestinationClass>>(SourceListObject);

    3) Es ist eine Klasse anzulegen, die von AutoMapper.Profile erbt.
       Im Konstruktor dieser Klasse ist dann mittels der Befehls CreateMap anzugeben. welche Klasse
       als Quelle und welche als Ziel für das Transferieren der Daten verwendet werden soll.
            createMap<SourceClass,DestinationClass>();
        Mit Hilfe dieser einfachen Anweisung ist AutoMapper bereits in der Lage basierend
        auf den Namen und der Typen der Properties ein Tranferieren der Daten vorzunehmen.
        Folgende Anweisung wird verwendet, um ein manuelles Mapping zu definieren.
        Die Anweisung definiert somit zuerst die Ziel-Propertie, um dann anschließend
        eine Aktion zu bestimmen, die aus der Quell-Property die Daten extrahiert.
        Hierbei werden LINQ-Anweisungen verwendet um z.B. aus einer zugeordneten 1:n Entität das
        gewünschte Objekt zu selektieren.
            createMap<SourceClass,DestinationClass>()
                .ForMember(dest => dest.PropName, opt => {
                    opt.MapFrom(src => src.PropCollection.FirstOrDefault(p => p.PropName == Condition).PropName)
                })




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
	npm install boostrap
        Ergänzend wird außerdem font-awesome mittels NPM importiert.
	npm install font-awesome
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
        muss in der Child-Komponente eine Variabel mit dem @Input-Dekorator versehen werden.
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
        laden und der Variabel in der Child-Komponente zuweisen.
        (siehe member-list- und member-card-component)
    
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
Angular - Umgebungsvariabeln
-----------------------------------------
Im Ordner src/environment befinden sich zwei Dateien, in den Variabeln definiert werden können,
die sich hinsichtlich ihres Einsatzes in einer Test- oder Produktionsumbegung unterscheiden.
In diesen Dateien ist ein JSON-Objekt definiert, das nach Belieben erweitert werden kann.
Um Variabeln aus diesen Dateneien nutzen zu können, ist es in der entsprechenden Komponente notwendig,
die folgende Import-Anweisung aufzunehmen:
    import { environment } from './../../environments/environment';


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

Die Library ist auch in der Lage mittels eines HttpInterceptors das Token 
automatisch einem Http-Client-Request anzufügen.   
Hierzu sind folgdende Schritte zu tun.
1. In app.module.ts muss das JwtModule importiert werden
2. In app.module.ts ist eine Funktion anzulegen, die das Token aus dem LocalStorage ausliest
   und zurückgibt.
3. Im Bereich des Import-Arrays ist das JwtModule zu konfigurieren.
    Hier sind drei Angaben zu machen:
        1) wie heißt die Funktion die das Token aus dem LocalStorage ausliest
        2) In einem Array ist eine Whitelist zu definieren
        3) In einem Array ist eine Blacklist zu definieren



-----------------------------------------
ngx-bootstrap    
-----------------------------------------
Um das Einbinden von Bootstrap und eigenen Templates zu vereinfachen, wird ngx-bootstrap mittel npm installiert.
    https://valor-software.com
Durch diese Bibliothek kann außerdem auf jquery verzichtet werden, welches eine Grundlage für Bootstrap ist.
    npm install ngx-bootstrap --save
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
Bootstrap Style-Angaben
-----------------------------------------
container   --> Container für die Aufnahme des GridLayouts
col-lg-x    --> Anzahl der Spalten bei einem großen Screen, x = Anzahl Spalten (1 bis 12)
col-md-x    --> Anzahl der Spalten bei einem mittleren Screen, x = Anzahl Spalten (1 bis 12)
col-sm-x    --> Anzahl der Spalten bei kleinerm Screen

mt-x        --> margintop: x pixel
text-center --> Textausrichtung zentriert
text-left   --> Textausrichtung links
text-right   --> Textausrichtung rechts

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
Soll zusätzlich beim Aufruf der Route ein Parameter angehangen werden, so ergibt sich folgende Syntax,
in der nach der Route eine beliebige Variabel angefügt wird.
    <a [routerLink]="['/routePath/', var]" routerLinkActive="router-link-active" >Dating-App</a>

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

Eine Guard-Komponente kann auch dazu verwendet werden, dem User eine Warnung anzuzeigen,
wenn er in einem Forumlar eine Änderung vorgenommen hat, die noch nicht gespeichert wurde,
und er nun auf einen Link in der Navigation klickt um die aktuelle Seite zu verlassen.
Ein solcher Rout-Guard wurde in der Klasse _guards/prevent-unsaved-changes realisiert.
Zur Realisierung einer solchen Funktion muss eine Klasse erstellt werden, die die CanDeactivate-Klasse unter Angabe der relevanten
Formular-Komponente importiert.
In dieser Klasse wird dann eine Funktion mit dem Namen canDeactivate angelegt, 
die einen Parameter vom Typ der relevanten Komponente entgegennimmt.
Wie gewöhnlich muss auch diese Klasse im app.module-File importiert werden
und in routes.ts verwendet werden.


-----------------------------------------
TypeScript: 
-----------------------------------------
Um typsichere Eigenschaften eines Objektes zu erhalten, 
werden in TypeScript Interfaces definiert, die von den Objekten zu implementieren sind.
Dies hat folgende Vorteile:
- Der TypeScript-Compiler ist in der Lage den Datentyp zu prüfen
- Intellisense in der IDE
- Autovervollständigung in der IDE
Das Anlegen eines Interfaces innerhalb der Client-Applikation erfolgt mittels Context-Menü.
(Generate Interface)
Optionale Angaben müssen immer nach den Pflichtangaben aufgeführt werden.
Ist eine Property vom Typ eines anderen Interfaces, so ist dieses Interface
mittels Import-Anweisung zu importieren.

Ein Interface-Definition innerhalb von TypeScript sieht wie folgt aus
    export interface IName {
        prop1:  number;         // numerische Eigenschaft
        prop2:  string;
        prop3:  Date;
        prop4:  boolean;
        prop5?: string[];       // Optionale Eigenschaft vom Typ String-Array
    }



-----------------------------------------
- Angular Observables
-----------------------------------------
Ein Observable wird im Rahmen eines asychronen Aufrufs als Funktionsergebnis zurückgegeben.
Das Observable beinhaltet seinerseits generische Objekte.
Allgemeine Observable-Definition:
getObservable() {
    return this.http.get(Url);
}

Die Definition eines Observable zum Ausführen eines API-Requests unter Verwendung einer 
generischen Klassen-Definition sieht wie folgt aus:
    getObservable(): Observable<DataTypeName> {
        return this.http.get<DataTypeName>(Url);
    }

Nachfolgende Funktion verwendet zusätzlich bei der Abfrage eine HttpOption
um das Authentifizieruns-Token an den Server zu senden.
  getObservable(): Observable<DataTypeName> {
    return this.http.get<DataTypeName>(Url, httpOptions);
  }    

httpOptions muss vorher wie folgt definiert werden.
Achtung: Das Leerzeichen zwischen Bearer und dem nachfolgendem Token muss vorhanden sein!!!
const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};  

Die Verwendung eines wie oben definierten Observable (im Rahmen eines Services) sieht wie folgt aus:
1. Der Service muss im Konstruktor der Komponente injeziert werden
2. Es wird innerhalb der Komponente eine Funktion erstellt, die diesen Service nutzt.
        loadData() {
            this.serviceName.getObservable().subscribe((object: DataTypeName) => {
                this.object = object;
            }, error => {
                ...
            })
        }

-----------------------------------------
- Route-Resolver
-----------------------------------------
Ohne die Verwendung eines Rout-Resolvers besteht das Problem,
dass beim Aktivieren einer Route (also Aufruf einer Komponente), 
nicht sofort die Daten zur Verfügung stehen, die angezeigt werden sollen.
Es kommt somit zu einer Angular-Fehlermeldung.
Dies beruht auf der Tatsache, dass die Daten im Gegensatz zur Komponente selbst mittels Webrequest 
noch nachgeladen werden. 

Zur Vermeidung dieses Fehlers stehen zwei Möglichkeiten zur Verfügung.
1)  Die Referenzierung der Properties eines Objektes werden mit dem Save-Navigation-Property versehen.
    An den Namen des Objektes wird ein ? angehangen. Hiermit wird ein NULL-Wert des Objektes akzeptiert.
    (Beispiel:      user?.name)

2) Es wird ein Route-Resolver verwendet.
    Hierzu wird zunächst ein eigener Ordner angelegt in dem dann eine Klasse angelegt wird,
    die die generische Resolve-Klasse unter Berücksichtigung der relevanten Datenklasse implementiert.
    Durch die Implementierung der Resolver-Klasse muss eine Funktion mit dem Namen resolve angelegt werden,
    die als Parameter die aktivierte Route entgegennimmt und als Ergebnis eine Observable zurückgibt.
    Ein Subscribe des Observables erfolgt hier nicht. Hier erfolgt nur eine Auswertung des Fehlerfalles
    mittels der Syntax von rxjs.
    Beispiel:
        resolve(route: ActivatedRouteSnapshot): Observable<DataClassName> {
            return this.dataclassService.getData(route.params['id']).pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/members']);
                    return of(null);
                })
            );
        }
    
    Die selbst definierte Resolver-Klasse ist dann in app.modul.ts im Provider-Array anzugeben.
    Wie immmer bedarf es dann einer weiteren Import-Anweisung in app.module.ts
    
    Als nächstes muss in der betreffenden Komponente die Funktion ngOnInit geändert werden.
    Statt dem bisherigen direkten Aufruf einer Funktion zum Laden der benötigten Daten, erfolgt nun
    ein Subscribe zum Empfangen des Observables des ActiveRoute-Objektes, welches im Konstruktor der Komponente injeziert wurde.
    Diese Funktion gestaltet sich wie folgt:
        this.route.data.subscribe(data => {
            this.dataclassName = data['dataclassName'];
        });


    Abschließend ist im router-Modul die Verwendung des Resolvers anzugeben (siehe routes.ts)
    Die betreffende Route wird um eine dritte Angabe erweitert. Diese zeigt an, dass eine Resolve-Methode anzuwenden ist.
    Der Typ der Variabel entspricht dem Namen der zuvor angelegten Resolver-Klasse.
             ...  , resolve: {dataclassName: DataClassResolver }

-----------------------------------------
- Ngx-Gallery
-----------------------------------------
Zur Darstellung der Images im Rahmen einer Gallerie, wird die externe Bibliothek Ngx-Gallery verwendet.
Diese wird zunächst mittels npm installiert.
    npm install ngx-gallery --save

In app.module.ts diese Bibliothek wie immer im Bereich imports und einer zugehörigen Import-Anweisung zu registrieren.
Die Anwendung erfolgt dann innerhalb der gewünschten Komponente, mittels des folgenden Schritte:
Definition einer Variabel vom Typ NgxGalleryOptions[] zur Aufnahme der Konfigurationsdaten
Definition einer Variabel vom Typ NgxGalleryImage[] zur Aufnahme der Images
Anpassung der Funktion ngOnInit, in der zuvor definierten Variabel gefüllt werden.
Das Array der Images wird hierbei durch eine Funktion befüllt, die das Datenobjekt der Komponente nutzt.
In der Html-Seite der Komponente wird folgendes Tag eingefügt.

    <ngx-gallery [options]="galleryOptions" [images]="galleryImages"></ngx-gallery>


-----------------------------------------
- Angular Formular Verarbeitung
-----------------------------------------

    1) Damit Angular mit Formularen umgehen kann, sind zunächst in app.module.ts
       Anweisungen zum Import von FormsModule einzufügen.

    2) Die TypeScript-Datei der Komponente ist wie folgt anzupassen:
        --> Anlegen einer Funktion zur Verarbeitung der Formular-Eingaben
        --> Variabel zur Aufnahme des Objekts anlegen (nachfolgend als varObjectName bezeichnet)

    3) Das Html-Formular ist wie folgt anzupassen:
        - Attribute dem Forumlar-Tag hinzufügen 
            -     #nameDesForms="ngForm"
            -     (ngSubmit)="formFunction()"
            -     Id des Formulars definieren
            Durch diese Angabe wird dem Forumlar ein Name zugewiesen, der dann als Variabel für
            den programmatischen Zugriff auf das Formular verwendet werden kann.
            Die Id des Formular wird zwar nicht von Angular verwendet, sollten 
            Außerdem erfolgt durch die Zuweisung von ngForm eine Datenbindung sowie die Zuweisung 
            verschiedenen Attribute und Funktionen, die in ngForm gekapselt sind.
            ngForm-Attribute:
                nameDesForms.dirty           --> zeigt an, ob es Änderung in den Formular-Feldern gab.

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

    4) Im Typesript der Formular-Komponente ist der Dekorator @ViewChild mit Bezug
        auf das relevante Formular einzufügen. Wie üblich ist auch für diesen Dekorator
        eine Import-Anweisung zu Beginn des TypeScripts aufzunehmen.
        Dieser ermöglicht den Zugriff auf die Attribute des Formulars mittels einer Variabel.
        Sollte eine Änderung am Formular vorgenommen werden, so spiegelt sich diese Änderung
        auch in der Variabel wieder.
          @ViewChild('NameOfForm') myFormVar: NgForm;
        Die Variabel myFormVar ist hierbei vom Typ NgForm und beinhaltet eine Two-Way-Datenbindung 
        und bietete eine Reihe von Funktionen an, z.B.:
            myFormVar.reset()           --> Beendet Dirty-Read-Modus
            myFormVar.reset(myObject)   --> Beendet Dirty-Read-Modus, behält aber die gemachten Eingaben
                                            (basierend auf dem relevanten Datenobjekt)


-----------------------------------------
- Events außerhalb der Angular-Applikation
-----------------------------------------
Mittels des Imports des HostListener-Dekorator können Events definiert werden,
die sich außerhalb der Angular-Applikation befinden. So kann z.B. auch das beforeunload-EventEmitter
der Seite definiert und abgefangen werden.

-----------------------------------------
- Upload von Bildern
-----------------------------------------
Bilder, die vom User auf die Site hochgeladen werden können,
werden in der Cloud (Coudinary.com) gespeichert.
Nach dem Anlegen eines Accounts werden in der Datei appsettings.json die Account-Daten
für den Zugriff auf die Cloud hinterlegt.
Im Ordner Helpers wird eine Klasse zur Aufnahme dieser drei Properties angelegt.
In der startup.cs-Datei der Applikation wird dann in der Funktion startup ein Objekt von dieser Klasse
initialisiert und mit den Werten aus der Konfiguration geladen. 
Dies wird durch den nachfolgenden Befehl realisiert:
    services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
