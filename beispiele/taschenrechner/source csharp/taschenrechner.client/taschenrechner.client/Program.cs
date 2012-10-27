using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using taschenrechner.client.flows;
using taschenrechner.gui;

namespace taschenrechner.client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gui = new GUI();
            var berechnen = new Berechnen();

            gui.Rechenschritt_ausführen += berechnen.Process;
            berechnen.Result += gui.Ergebnis_anzeigen;

            Application.Run(gui);
        }
    }
}
