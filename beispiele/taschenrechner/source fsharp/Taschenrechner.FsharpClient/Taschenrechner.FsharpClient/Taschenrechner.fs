namespace Taschenrechner.FsharpClient

open System

// Hinweis: einfach mal die Kommentare weglöschen ...
// das "Programm" besteht nur aus ein paar Zeilen und
// wenn ich mir die Arbeit mit den Typabkürzungen nicht machne würde ...

// in der FP sagen die Typen in der Regel sehr viel aus
// meistens ist das Programm bereits richtig, wenn die Typenprüfung des
// Kompilers das Programm als korrekt befindet
// Deshalb arbeitet man gerne mit aussagekräftigen Typen, dafür aber mit
// sehr kurzen (oder gar keinen) Argumenten

// ich treibe das jetzt mal nicht zu sehr auf die Spitze, aber hier sind
// die Typen, die ich für das konkrete Problem vorschlagen würde:

/// Typabkürzung: ein Zwischenergebnis ist eine Zahl vom Typ double
type Zwischenergebnis = double

/// Typabkürzung: zum Speichern, der Operation, die mit einer
/// Eingabe erst begonnen wurde dient eine Continuation, d.h.
/// eine Funktion, die mit dem Rest der Eingabe aufgerufen werden
/// soll um dann das Ergebnis des Schrittes zu ermitteln:
type OperationCont = double -> double

/// der interne Status des Taschenrechners ist dann einfach
/// ein Zwischenergebnis und die teilweise Operation, die noch
/// abgeschlossen werden muss (das Zwischenergebnis brauchen wir eigentlich
/// nur zur Anzeige des Zwischenstandes - würde man sich um die Operation mehr
/// gedanken machen, könnte man das leicht aus dieser extrahieren (hier z.B.
/// würde es reichen die neutralen Elemente der Operationen einzusetzen,
/// dann müsste man die Operationen aber z.B. durch einen algebraischen
/// Datentyp darstellen - ein Aufwand, der mir hier als zu hoch erscheint)
type Status = Zwischenergebnis * OperationCont

/// die Eingabe: nach Anforderung eine Zahl und die Operation
/// wie schon beim IGUI angemerkt, ist für mich ein char sinnvoller
/// deshalb werde ich lieber die per Schnittstelle erhaltene Eingabe
/// "parsen" und hier meiner Intuition trauen
type Eingabe = double*char

// aus den Typen ergibt sich eigentlich schon direkt der "Algorithmus"
// um "rein" zu bleiben und keinen Zustand, sprich Seiteneffekte schon
// an dieser Stelle einzuführen, gehen wir den üblichen Weg in der FP
// und machen den Zustand zur Eingabe - letztlich implementieren wir
// hier also einen Automat
module Rechner =

    /// Hilffunktion: übersetze die Operation von einem Character in unsere
    /// Continuation
    let private toOperationCont (ze : Zwischenergebnis) (c : char) : OperationCont =
        match c with
        | '+' -> (fun z -> ze + z)
        | '-' -> (fun z -> ze - z)
        | '*' -> (fun z -> ze * z)
        | '/' -> (fun z -> ze / z)
        | '=' -> id
        | _   -> failwith (sprintf "kann Operator '%c' nicht verarbeiten" c)

    /// der initiale Zustand des Rechner - Automaten: Zwischenergebnis ist 0
    /// und es gibt keine angefangene Operation, sprich Continuation = Identität
    let initial : Status = (0.0, id)

    /// Verarbeitungsschritt bzw. Übergangfunktion
    /// man beachte das schöne Patternmatching-Feature von F#
    /// die Eingaben werden gleich in passende Stücke zerlegt
    /// der rest ist dank Typen und Hilsfuntkion trivial!
    let verarbeiteEingabe ((_, oc) : Status) ((z, op) : Eingabe) : Status =
        let ze' = oc z                   // neues Zwischenergebnis berechnen aus der eingegebenen Zahl
        let oc' = toOperationCont ze' op // neue Continuation für die nächste Eingabe, aus dem eingegeben Operator
        (ze', oc')
