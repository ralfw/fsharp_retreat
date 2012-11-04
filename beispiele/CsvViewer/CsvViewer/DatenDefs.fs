module CsvViewer.DatenDefs

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

/// die möglichen Benutzeraktionen
type Aktion = NächsteSeite 
            | VorherigeSeite 
            | ErsteSeite 
            | LetzteSeite 
            | ProgrammVerlassen
