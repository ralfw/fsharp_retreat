namespace Taschenrechner.Tests

open System
open NUnit.Framework

open Taschenrechner.FsharpClient

// Hinweis: ich habe ein wenig "gemogelt" - eigentlich müsste ich das über die IGUI
// Schnittstelle abhandeln - damit müsste ich mich aber um einen Mock kümmern oder die
// Logik im Program nachbauen
// da es ja nur darum gehen sollte zu sehen, wie Test in F# aussehen können (ja man kann Prosa schreiben :D)
// dürfte das reichen

// PS: falls ihr das benutzeRechner nicht finden könnt (die VS Navigation sollte gehen)
// das ist sicher geschmacksache, ob man das so machen sollte: es ist in den Helpers.fs definiert
// und das Modul dort steht auf Autoopen, so dass die Funktionen im Namespace automatisch
// zur Verfügung stehen - mir gefällt das, weil es mir Schreibarbeit erledigt, kann aber natürich
// zu Verwirrung führen (wo kommt das den her) ... in dem Fall sollte es ok sein

[<TestFixture>]
type ``aceptance Tests nach Anforderung``() = 

    [<Test>]
    member test.``Eingabe von [2; +] liefert Ausgaben [2; 2]``() = 
        let eingaben = [Ziffer '2'; Operator '+']
        let erwartet = [2.0; 2.0]
        let ergebnise = benutzeRechner eingaben
        CollectionAssert.AreEqual(erwartet, ergebnise)

    [<Test>]
    member test.``Eingabe von [2;+;3;*;4;=] liefert Ausgaben [2;2;3;5;4;20]``() = 
        let eingaben = [Ziffer '2'; Operator '+'; Ziffer '3'; Operator '*'; Ziffer '4'; Operator '=']
        let erwartet = [2.0; 2.0; 3.0; 5.0; 4.0; 20.0]
        let ergebnise = benutzeRechner eingaben
        CollectionAssert.AreEqual(erwartet, ergebnise)