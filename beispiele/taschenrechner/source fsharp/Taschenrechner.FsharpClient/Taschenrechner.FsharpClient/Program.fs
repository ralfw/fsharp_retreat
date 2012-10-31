open System
open System.Windows.Forms
open taschenrechner.gui
open Taschenrechner.FsharpClient

let imperativerRechner (gui : IGUI) =
    let zustand = ref Rechner.initial

    // für Iteration 2 müssen jetzt eben zwei Ereignisse des GUI
    // gebunden werden

    let aufZahlReagieren (z : string) = 
        zustand := Rechner.verarbeiteEingabe !zustand (Ziffer z.[0])
        gui.Ergebnis_anzeigen ((!zustand).Akku)

    let aufOperatorReagieren (op : string) = 
        zustand := Rechner.verarbeiteEingabe !zustand (Operator op.[0])
        gui.Ergebnis_anzeigen ((!zustand).Akku)

    gui.add_Zifferneingabe          (new Action<_>(aufZahlReagieren))
    gui.add_Rechenschritt_ausführen (new Action<_>(aufOperatorReagieren))

// Der Einstiegspunkt - wie ihr seht komplett analog zum C#-Fall
[<EntryPoint>]
[<STAThread>]
let main argv = 
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    let gui = new GUI();
    imperativerRechner gui

    Application.Run(gui);
    0
