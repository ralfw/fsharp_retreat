namespace Taschenrechner.Tests

open System

open Taschenrechner.FsharpClient

[<AutoOpen>]
module Helpers =

    let benutzeRechner (eingaben : Eingabe list) : Zwischenergebnis list =
        eingaben 
        |> List.scan Rechner.verarbeiteEingabe 
                        Rechner.initial
        |> List.map (fun s -> s.Akku.Wert)
        |> List.tail  // <- erstes ZE von scan ist irrelevant