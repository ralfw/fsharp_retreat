using System;
using taschenrechner.client.operationen;

namespace taschenrechner.client.flows
{
    class Berechnen
    {
        public Berechnen()
        {
            var akku = new Akkumulator();
            var rechenwerk = new Rechenwerk();

            _process += rechenschritt =>
                            {
                                rechenschritt = akku.Operatoren_tauschen(rechenschritt);
                                var term = akku.Zwischenergebnis_laden(rechenschritt);
                                var ergebnis = rechenwerk.Operator_anwenden(term);
                                Result(akku.Zwischenergebnis_speichern(ergebnis));
                            };
        }

        private readonly Action<Tuple<double, string>> _process;
        public void Process(Tuple<double, string> rechenschritt) { _process(rechenschritt); }

        public event Action<double> Result;
    }
}