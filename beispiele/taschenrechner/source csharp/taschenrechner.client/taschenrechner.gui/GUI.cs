using System;
using System.Windows.Forms;

namespace taschenrechner.gui
{
    public partial class GUI : Form, IGUI
    {
        public GUI()
        {
            InitializeComponent();
        }


        private void btnDigit_Click(object sender, EventArgs e)
        {
            var digit = ((Button)sender).Text;
            Zifferneingabe(digit);
        }

        private void btnOp_Click(object sender, EventArgs e)
        {
            var op = ((Button) sender).Text;
            Rechenschritt_ausführen(op);
        }


        public event Action<string> Rechenschritt_ausführen;
        public event Action<string> Zifferneingabe;
        
        public void Ergebnis_anzeigen(double ergebnis)
        {
            textBox1.Text = ergebnis.ToString();
        }
    }
}
