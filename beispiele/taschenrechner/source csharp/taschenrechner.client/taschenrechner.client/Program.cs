using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using taschenrechner.client.flows;
using taschenrechner.client.operationen;
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

            var root = new Root(gui, new Zahlenwerk(), new Berechnen());

            Application.Run(gui);
        }
    }
}
