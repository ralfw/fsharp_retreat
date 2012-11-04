module CsvViewer.Eingabe

open System

/// die möglichen Benutzeraktionen
type Aktion = NächsteSeite 
            | VorherigeSeite 
            | ErsteSeite 
            | LetzteSeite 
            | ProgrammVerlassen

let aktionAbwarten () : Aktion option =
    match Console.ReadKey(true).Key with
    | ConsoleKey.N -> Some NächsteSeite
    | ConsoleKey.P -> Some VorherigeSeite
    | ConsoleKey.F -> Some ErsteSeite
    | ConsoleKey.L -> Some LetzteSeite
    | ConsoleKey.X -> Some ProgrammVerlassen
    | _            -> None

