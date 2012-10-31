using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taschenrechner.client.operationen
{
    class Zahlenwerk
    {
        private double _zahl = 0;
        private bool _zurücksetzen_bei_nächster_Ziffer = false;


        public void Zifferneingabe(string ziffer)
        {
            if (_zurücksetzen_bei_nächster_Ziffer)
                _zahl = int.Parse(ziffer);
            else
                _zahl = _zahl * 10 + int.Parse(ziffer);

            _zurücksetzen_bei_nächster_Ziffer = false;
            Aktualisierte_Zahl(_zahl);
        }


        public void Zahl_entnehmen(string op)
        {
            var rechenschritt = new Tuple<double, string>(_zahl, op);
            Zahl_entnommen(rechenschritt);
        }


        public void Zahl_setzen(double zahl)
        {
            _zahl = zahl;
            _zurücksetzen_bei_nächster_Ziffer = true;
        }


        public event Action<double> Aktualisierte_Zahl;
        public event Action<Tuple<double, string>> Zahl_entnommen;
    }
}
