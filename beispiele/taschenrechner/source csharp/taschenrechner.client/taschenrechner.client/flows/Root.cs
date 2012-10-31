using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taschenrechner.client.operationen;
using taschenrechner.gui;

namespace taschenrechner.client.flows
{
    class Root
    {
        public Root(IGUI gui, Zahlenwerk zw, Berechnen berechnen)
        {
            gui.Zifferneingabe += zw.Zifferneingabe;
            gui.Rechenschritt_ausführen += zw.Zahl_entnehmen;
            zw.Zahl_entnommen += berechnen.Process;
            zw.Aktualisierte_Zahl += gui.Ergebnis_anzeigen;
            berechnen.Result += zw.Zahl_setzen;
            berechnen.Result += gui.Ergebnis_anzeigen;
        }
    }
}
