module CsvViewer.Anzeige

let tabBreite = 3

let private zeileFormatieren : string list -> string =
    List.reduce (sprintf "%s\t%s")

let private horLinie (len : int) : string =
    List.replicate len "-" 
    |> List.reduce (+)

let private kopfAnzeigen (kopf : Seite.Kopf) =
    let zeile = zeileFormatieren kopf
    printfn "%s" zeile
    printfn "%s" <| horLinie (zeile.Length + (tabBreite - 1) * kopf.Length)

let private zeileAnzeigen (z : Seite.Zeile) = 
    printfn "%s" <| zeileFormatieren z

let private fussAnzeigen() = 
    printfn "" // eine leerzeile
    printfn "N(ext page, P(revious page, F(irst page, L(ast page, eX(it"

let private anzeigeLöschen() =
    System.Console.Clear()

let seiteAnzeigen (seite : Seite.Seite) =
    anzeigeLöschen()
    kopfAnzeigen seite.kopf
    Seite.zeilenAufSeite seite
         |> Seq.iter zeileAnzeigen
    fussAnzeigen()



