module CsvViewer.Main

let defaultZeilenProSeite = 3

let rec hauptschleife (aktuelleSeite : Seite.Seite) =
    Anzeige.seiteAnzeigen aktuelleSeite
    let aktion = Eingabe.aktionAbwarten()
    let seite = aktion |> Option.bind (Seite.benutzerAktion aktuelleSeite)
    match seite with
    | None   -> 0 // Programm beenden
    | Some s -> hauptschleife s
    
let argvAuswerten (argv : string[]) : (Datei.Pfad * Seite.ZeilenProSeite) option =
    match argv.Length with
    | 1 -> Some (argv.[0], defaultZeilenProSeite)
    | 2 -> Some (argv.[0], int argv.[1]) 
    | _ -> printfn "Benutzung: CsvViewer.exe Datei [elemente pro Seite]"
           None   

[<EntryPoint>]
let main argv = 
    match argvAuswerten argv with
    | Some (pfad, zeilenProSeite)
           ->  pfad
              |> Datei.einlesen
              |> Seite.initialeSeiteErzeugen zeilenProSeite
              |> hauptschleife
    | None -> -1
