module CsvViewer.CsvFormat

open DatenDefs

let zeilenEndeBei = '\n'
let trennZeichen  = ';'

let trenneZeilen (newLineAt : char) (s : string) : string seq =
    s.Split(newLineAt) |> Seq.ofArray

let trenneZeile (splitAt : char) (s : string) : Zeile =
    s.Split(splitAt) |> List.ofArray