using System;

namespace taschenrechner.client.operationen
{
    class Akkumulator
    {
        private string _vorheriger_Operator = "+";
        private double _zwischenergebnis = 0; // neutrales Element zum initialen vorherigen Operator

        public Tuple<double, string> Operatoren_tauschen(Tuple<double, string> rechenschritt)
        {
            var _relevanter_Operator = _vorheriger_Operator;
            _vorheriger_Operator = rechenschritt.Item2;
            return new Tuple<double, string>(rechenschritt.Item1, _relevanter_Operator);
        }

        public Tuple<double, double, string> Zwischenergebnis_laden(Tuple<double, string> rechenschritt)
        {
            return new Tuple<double, double, string>(_zwischenergebnis, rechenschritt.Item1, rechenschritt.Item2);
        }

        public double Zwischenergebnis_speichern(double ergebnis)
        {
            _zwischenergebnis = ergebnis;
            return ergebnis;
        }
    }
}