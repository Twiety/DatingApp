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

Das Git-Repository wird zur Vereinfachung so angelegt, dass es beide Applikationen beinhaltet.
Hierzu wird im Prompt das übergeordnete Verzeichnis beider Applikationen aufgerufen.
Anschließend wird mittels dem Befehl "git init" ein neues Repository angelegt.
In der Applikation DatingApp.API wird anschließend (im dortigen Root)
eine neue Datei mit dem Namen .gitignore angelegt.
In dieser Datei werden zeilenweise alle Verzeichnisse und Dateien aufgelistet,
die von Git nicht im Repository zu berücksichtigen sind.
Wildcards (*) können dabei in den Angaben verwendet werden.


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

