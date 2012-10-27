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


        private void btnOp_Click(object sender, EventArgs e)
        {
            var operand = double.Parse(textBox1.Text);
            var op = ((Button) sender).Text;
            Rechenschritt_ausführen(new Tuple<double, string>(operand, op));
        }


        public event Action<Tuple<double, string>> Rechenschritt_ausführen;
        
        public void Ergebnis_anzeigen(double ergebnis)
        {
            textBox1.Text = ergebnis.ToString();
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 99;
            textBox1.Focus();
        }

    }
}
