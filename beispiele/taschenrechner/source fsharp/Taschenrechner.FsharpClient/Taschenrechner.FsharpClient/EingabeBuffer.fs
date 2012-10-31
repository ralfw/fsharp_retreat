namespace Taschenrechner.FsharpClient

open System

type EingabeBuffer = { Wert            : double
                     ; NachkommaFaktor : double }

[<AutoOpen>]
module EingabeBufferOps =

    let private inNachkommaModus (buf : EingabeBuffer) : bool =
        buf.NachkommaFaktor >= 0.0

    let private wechsleInNachkommaModus (buf : EingabeBuffer) : EingabeBuffer =
        if inNachkommaModus buf
        then buf
        else { buf with NachkommaFaktor = 0.1 }

    let private wechsleVorzeichen (buf : EingabeBuffer) : EingabeBuffer =
        if buf.Wert <> 0.0 
        then { buf with Wert = -1.0 * buf.Wert }
        else buf

    let private zifferHinzufügen (buf : EingabeBuffer) (ziffer : int) : EingabeBuffer =
        if inNachkommaModus buf 
        then { buf with NachkommaFaktor = buf.NachkommaFaktor * 0.1;
                        Wert = buf.Wert + buf.NachkommaFaktor * double ziffer }
        else { buf with Wert = buf.Wert * 10.0 + double ziffer }

    let zeichenEingeben (buf : EingabeBuffer) (zeichen : char) : EingabeBuffer =
        match zeichen with
        | ',' | '.' 
            -> wechsleInNachkommaModus buf
        | '+' | '-'
            -> wechsleVorzeichen buf
        | ziffer when Char.IsDigit(ziffer)
            -> zifferHinzufügen buf (int ziffer - int '0')
        | _ -> failwith (sprintf "<%c> ist kein akzeptiertes Zeichen" zeichen)

    let aktuellerWert (buf : EingabeBuffer) : double =
        buf.Wert

    let leereEingabe = { Wert = 0.0; NachkommaFaktor = -1.0 }

