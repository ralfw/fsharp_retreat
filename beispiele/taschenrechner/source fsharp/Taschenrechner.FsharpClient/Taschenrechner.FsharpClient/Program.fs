open System
open System.Windows.Forms
open taschenrechner.gui
open Taschenrechner.FsharpClient

// ***************************
// wie ihr seht ist das "Programm" praktisch vollkommen gleich 
// zum C# Äquivalent...

// der schon angesprochene Agent/Workflow-Mechanismus
// kann man sich in diesem Fall als eine Art asycnhronen Fold
// vorstellen, bzw. als Wrapper, der den Status verwaltet
// und auf Eingaben wartet
let rechner (gui : IGUI) : IDisposable =
    // wir brauchen den Kontext, weil wir zurück in den GUI-Thread müssen
    let context = System.Threading.SynchronizationContext.Current
    // wegen der lästigen GUI-Thread geschichte, leider nötig
    let zeigeZwischenergebnis (ze : Zwischenergebnis) =
        async {
            do! Async.SwitchToContext context
            gui.Ergebnis_anzeigen ze
        }
    let mb = 
        MailboxProcessor.Start
            (fun (inbox : MailboxProcessor<Eingabe option>) -> 
                let rec loop (status : Status) =
                    async {
                        // warten auf Eingabe
                        let! eingabe = inbox.Receive()
                        match eingabe with
                        | Some eingabe -> 
                            let status' = Rechner.verarbeiteEingabe status eingabe
                            // Zwischenergebnis ans GUI:
                            do! zeigeZwischenergebnis (fst status')
                            // und weiter (sozusgaen das Wnd im While)
                            return! loop status'
                        | None -> return () // <- worklfow beenden
                    }
                loop Rechner.initial)
    // Eingabe an Agent übergeben (dabei den ersten Char aus dem Operator extrahieren)
    let übergebe (z : double, op : string) =
            mb.Post (Some (z, op.[0]))
    // Ereignis des GUIs binden
    gui.add_Rechenschritt_ausführen(new Action<_>(übergebe))
    // schöner Trick: wir geben ein IDisposable zurück, wird Dispose darauf angewendet, wird
    // der Rechner "heruntergefahren"
    { new IDisposable with member i.Dispose() = mb.Post None }
        

// eher klassisches Modell:
let imperativerRechner (gui : IGUI) =
    // hier der "mutable" Zustand
    // das ist eine Referenzzelle, die mit := gesetz und mit ! abgefragt wird
    let zustand = ref Rechner.initial

    let aufEingabeReagieren (z : double, op : string) = 
        zustand := Rechner.verarbeiteEingabe !zustand (z, op.[0])
        gui.Ergebnis_anzeigen (fst !zustand)

    gui.add_Rechenschritt_ausführen (new Action<_>(aufEingabeReagieren))

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
