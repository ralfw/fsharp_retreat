namespace Taschenrechner.Tests

open System

open Taschenrechner.FsharpClient

[<AutoOpen>]
module Helpers =

    let benutzeRechner (eingaben : Eingabe list) : Zwischenergebnis list =
        eingaben 
        |> List.scan Rechner.verarbeiteEingabe 
                        Rechner.initial
        |> List.map fst
        |> List.tail  // <- erstes ZE von scan ist irrelevant