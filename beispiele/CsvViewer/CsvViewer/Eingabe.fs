module CsvViewer.Eingabe

open DatenDefs
open System

let aktionAbwarten () : Aktion option =
    match Console.ReadKey(true).Key with
    | ConsoleKey.N -> Some NächsteSeite
    | ConsoleKey.P -> Some VorherigeSeite
    | ConsoleKey.F -> Some ErsteSeite
    | ConsoleKey.L -> Some LetzteSeite
    | ConsoleKey.X -> Some ProgrammVerlassen
    | _            -> None

