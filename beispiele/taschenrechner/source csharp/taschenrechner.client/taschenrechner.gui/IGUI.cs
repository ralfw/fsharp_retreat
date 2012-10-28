using System;

// Anmerkungen Carsten:
// 1.
// die Verarbeitung in F# fällt leichter, wenn die Events den
// Richtlinien (http://msdn.microsoft.com/de-de/library/w369ty8x%28v=vs.80%29.aspx)
// entsprechen (also EventHandler<...> mit entsprechenden EventArgs)
// 2.
// warum String? Eingegeben wird offensichtlich nur ein Char
// die Wahl des Typs Char würde Fehleingaben durch mehrere Buchstaben, bzw. leere Strings
// ausschließen (ok, der GUI-Trick über den Text ginge nicht mehr so schön, aber IMHO
// ist die Schnittstelle wichtiger als GUI-Tricks)


namespace taschenrechner.gui
{
    public interface IGUI
    {
        event Action<Tuple<double, string>> Rechenschritt_ausführen;
        void Ergebnis_anzeigen(double ergebnis);
    }
}
