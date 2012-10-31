namespace Taschenrechner.FsharpClient

open System

// Bemerkungen zur Iteration 2
// Wie ihr sehen könnt musste ich eigentlich nur
// das anpassen, was sich verändert hat:
// die Eingabe und der interne Status
// die Eingabe ist jetzt entweder eine Ziffer oder ein Operator
// und der Status muss die eingegene Zahl, den Wert im "Akkumulator"
// und die letzte, unvollständige Operation mitführen
// ÜBRIGENS: die von Dir gelieferte Version ist etwas buggy!
// Bei mir muss ich erst einmal "=" drücken. 
// Außerdem funktionieren Sachen wie "2,+,3,=,+,5,=" nicht
// das sollte IMHO 10 ergeben - meine Version macht das entsprechend
// kann aber leicht geändert werden - wenn die Operation "=" den Akku mit löscht

type OperationCont          = double -> double
type Zwischenergebnis       = double

type Status = { Eingabe     : EingabeBuffer;
                Akku        : Zwischenergebnis;
                LtOperation : OperationCont }

type Eingabe = 
    | Ziffer   of char
    | Operator of char

module Rechner =

    /// Hilffunktion: übersetze die Operation von einem Character in unsere
    /// Continuation
    let private toOperationCont (ze : double) (c : char) : OperationCont =
        match c with
        | '+' -> (fun z -> ze + z)
        | '-' -> (fun z -> ze - z)
        | '*' -> (fun z -> ze * z)
        | '/' -> (fun z -> ze / z)
        | '=' -> id
        | _   -> failwith (sprintf "kann Operator '%c' nicht verarbeiten" c)

    /// der initiale Zustand des Rechner 
    let initial : Status =
        { Eingabe     = leereEingabe
        ; Akku        = 0.0
        ; LtOperation = id }

    let private verarbeiteOperator (s : Status) (o : char) : Status =
        let ze' = s.LtOperation s.Akku
        let oc' = toOperationCont ze' o
        { Eingabe     = leereEingabe
        ; Akku        = ze'
        ; LtOperation = oc' }

    let private verarbeiteZiffer (s : Status) (z : char) : Status =
        let buf' = zeichenEingeben s.Eingabe z
        { s with Eingabe = buf'; Akku = aktuellerWert buf'; }

    /// Verarbeitungsschritt bzw. Übergangfunktion
    let verarbeiteEingabe (s : Status) (e : Eingabe) : Status =
        match e with
        | Ziffer z   -> verarbeiteZiffer   s z
        | Operator o -> verarbeiteOperator s o
