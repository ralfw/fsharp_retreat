namespace Taschenrechner.FsharpClient

open System

type OperationCont          = double -> double
type Zwischenergebnis       = double

type Akku = { Wert          : Zwischenergebnis
            ; Teiloperation : OperationCont }

module Akkumulator =

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

    /// der initiale Zustand des Akkumulators 
    let initial : Akku =
        { Wert          = 0.0
        ; Teiloperation = id }

    let verarbeiteOperator (akku : Akku) (o : char) : Akku =
        let ze' = akku.Teiloperation akku.Wert
        let oc' = toOperationCont ze' o
        { Wert          = ze'
        ; Teiloperation = oc' }

    let setzeAkku (akku : Akku) (w : Zwischenergebnis) : Akku =
        { akku with  Wert = w } 