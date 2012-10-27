using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taschenrechner.gui
{
    public interface IGUI
    {
        event Action<Tuple<double, string>> Rechenschritt_ausführen;
        void Ergebnis_anzeigen(double ergebnis);
    }
}
