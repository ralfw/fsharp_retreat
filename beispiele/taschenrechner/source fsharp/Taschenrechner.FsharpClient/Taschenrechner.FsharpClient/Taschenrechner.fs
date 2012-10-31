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

type Status = { Eingabe     : EingabeBuffer;
                Akku        : Akku }

type Eingabe = 
    | Ziffer   of char
    | Operator of char

module Rechner =

    /// der initiale Zustand des Rechner 
    let initial : Status =
        { Eingabe     = leereEingabe
        ; Akku        = Akkumulator.initial }

    let private verarbeiteZiffer (s : Status) (z : char) : Status =
        let buf' = zeichenEingeben s.Eingabe z
        { s with Eingabe = buf'; Akku = Akkumulator.setzeAkku s.Akku <| aktuellerWert buf'; }

    let private verarbeiteOperator (s : Status) (o : char) : Status =
        let akku' = Akkumulator.verarbeiteOperator s.Akku o
        { s with Akku = akku'; Eingabe = leereEingabe }

    /// Verarbeitungsschritt bzw. Übergangfunktion
    let verarbeiteEingabe (s : Status) (e : Eingabe) : Status =
        match e with
        | Ziffer z   -> verarbeiteZiffer   s z
        | Operator o -> verarbeiteOperator s o
