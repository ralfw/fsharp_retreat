namespace Taschenrechner.FsharpClient

open System

type EingabeBuffer = double

[<AutoOpen>]
module EingabeBufferOps =

    let private zifferHinzufügen (buf : EingabeBuffer) (ziffer : int) : EingabeBuffer =
        buf * 10.0 + double ziffer

    let zeichenEingeben (buf : EingabeBuffer) (zeichen : char) : EingabeBuffer =
        match zeichen with
        | ziffer when Char.IsDigit(ziffer)
            -> zifferHinzufügen buf (int ziffer - int '0')
        | _ -> failwith (sprintf "<%c> ist kein akzeptiertes Zeichen" zeichen)

    let aktuellerWert (buf : EingabeBuffer) : double =
        buf

    let leereEingabe : EingabeBuffer = 0.0