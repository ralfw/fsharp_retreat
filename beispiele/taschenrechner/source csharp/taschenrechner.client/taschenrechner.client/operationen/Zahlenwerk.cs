using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taschenrechner.client.operationen
{
    class Zahlenwerk
    {
        private double _zahl = 0;


        public void Zifferneingabe(string ziffer)
        {
            _zahl = _zahl*10 + int.Parse(ziffer);
            Aktualisierte_Zahl(_zahl);
        }


        public void Zahl_entnehmen(string op)
        {
            var rechenschritt = new Tuple<double, string>(_zahl, op);
            _zahl = 0;
            Zahl_entnommen(rechenschritt);
        }


        public event Action<double> Aktualisierte_Zahl;
        public event Action<Tuple<double, string>> Zahl_entnommen;
    }
}
