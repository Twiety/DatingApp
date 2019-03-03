// Import-Anweisung wird für die Referenz auf das Photo-Interface benötigt.
import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    // Ab hier werden nur noch optionale Properties aufgeführt.
    // die mittels ? gekennzeichnet werden.
    // Achtung: Optional Parameter müssen IMMER nach den Pflichtangaben erfolgen.
    interest?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
