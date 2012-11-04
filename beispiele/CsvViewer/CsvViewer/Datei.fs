module CsvViewer.Datei

open System.IO

type Pfad = string

let zeilenEndeBei = '\n'
let trennZeichen  = ';'

let private dateiAuslesen (pfad : Pfad) : string =
    use reader = File.OpenText(pfad) // bitte beachten: OpenText öfnet die Datei bereits in UTF-8!
    reader.ReadToEnd()

let einlesen (pfad : Pfad) : (Seite.Kopf * Seite.Zeile seq) =
    let inhalt = dateiAuslesen pfad
    let zeilen = inhalt |> CsvFormat.trenneZeilen zeilenEndeBei
                        |> Seq.map (CsvFormat.trenneZeile trennZeichen)
    (Seq.head zeilen, Seq.skip 1 zeilen)