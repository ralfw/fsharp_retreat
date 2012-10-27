using System;

namespace taschenrechner.client.operationen
{
    class Rechenwerk
    {
        public double Operator_anwenden(Tuple<double, double, string> term)
        {
            switch(term.Item3)
            {
                case "+":
                    return term.Item1+term.Item2;
                case "-":
                    return term.Item1-term.Item2;
                case "*":
                    return term.Item1*term.Item2;
                case "/":
                    return term.Item1/term.Item2;
                default:
                    return term.Item2;
            }
        }
    }
}