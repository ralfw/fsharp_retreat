module CsvViewer.Seite

type Kopf     = string list
type Zeile    = string list
type Inhalt   = Zeile seq

type ZeilenProSeite = int

/// Daten der aktuellen Seite
/// beinhaltet außerdem alles um weitere Seiten zu erzeugen
type Seite    = { kopf     : Kopf
                ; proSeite : ZeilenProSeite
                ; seitenNr : int
                ; gesamt   : Inhalt }
/// erzeugt eine Seitendarstellung, die die erste Seite anzeigt
let initialeSeiteErzeugen (zeilenProSeite : ZeilenProSeite) ((kopf, zeilen) : Kopf*Inhalt) : Seite =
    { kopf = kopf
    ; proSeite = zeilenProSeite
    ; seitenNr = 1
    ; gesamt = zeilen }

/// liefert die Zeilen die auf der Seite angezeigt werden sollen
let zeilenAufSeite (seite : Seite) : Inhalt =
    let skip = (seite.seitenNr - 1) * seite.proSeite
    seite.gesamt
         |> Seq.skip skip
         |> Seq.truncate  seite.proSeite

let private anzahlSeiten (seite : Seite) : int = 
    let anzElemente = Seq.length seite.gesamt
    int <| ceil (float anzElemente / float seite.proSeite)

let private springeZu (nr : int) (seite : Seite) : Seite =
    let gültigeNr = min (anzahlSeiten seite) <| max 1 nr
    { seite with seitenNr = gültigeNr }
    
let private seiteZurück (seite : Seite) = 
    springeZu (seite.seitenNr - 1) seite

let private seiteVor (seite : Seite) = 
    springeZu (seite.seitenNr + 1) seite

let private ersteSeite (seite : Seite) = 
    springeZu 0 seite

let private letzteSeite (seite : Seite) = 
    springeZu (anzahlSeiten seite) seite

/// Aktion durchführen
/// liefert Some Seite, falls eine Seite angzeigt werden soll
/// und None, falls das Programm verlassen werden soll
let benutzerAktion (seite : Seite) (aktion : Eingabe.Aktion) : Seite option =
    match aktion with
    | Eingabe.ProgrammVerlassen -> None
    | Eingabe.VorherigeSeite    -> Some <| seiteZurück seite
    | Eingabe.NächsteSeite      -> Some <| seiteVor seite
    | Eingabe.ErsteSeite        -> Some <| ersteSeite seite
    | Eingabe.LetzteSeite       -> Some <| letzteSeite seite

