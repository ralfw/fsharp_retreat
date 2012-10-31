# Taschenrechner
## Anforderungen - Iteration 1
Es soll ein Taschenrechner entwickelt werden. Der kann die Grundrechenarten ohne Operatorpräzedenzen (2+3*4=20).

Das UI sieht so aus:

![](https://raw.github.com/ralfw/fsharp_retreat/master/beispiele/taschenrechner/doc/gui%20skizze.png)

Die Zahlen werden im Textfeld eingegeben. Wenn man dann einen Operator betätigt, wird weitergerechnet. Hier eine Beispieleingabesequenz:

_(Zahleneingabe, Operator)=Angezeigtes Ergebnis_

1. (2, +)=2
1. (3, *)=5
1. (4, =)=20
1. (7, *)=7
1. (5, =)=35
1. (35, +)=35
1. (5, =)=40

## Flow
Ein Flow-Design-Entwurf für die Lösung könnte so aussehen:

#### Root Flow

![](http://yuml.me/9bfaf8da)

_Grafiken realisiert mit [yuml.me Aktivitätsdiagrammen](http://yuml.me/diagram/scruffy/activity/draw)_

	[GUI Rechenschritt ausfuehren]->(Berechnen)->[GUI Ergebnis anzeigen]

#### Berechnen
![](http://yuml.me/400f8564)

	(start)->(Operatoren tauschen)->(Zwischenergebnis laden)->(Operator anwenden)->(Zwischenergebnis speichern)->(end)

* Start: Vom GUI fließen die aktuelle Zahl (Operand) und ein Operator ein, z.B. (3, *)
* Operatoren tauschen: Es wird der einfließende Operator durch den des vorherigen Rechenschritts ersetzt und selbst für den nächsten gespeichert, z.B. (3, +)
* Zwischenergebnis laden: Dem vorherigen Operator und der aktuellen Zahl wird das bisherige Zwischenergebnis "zugemischt", z.B. (2, 3, +)
* Operator anwenden: Jetzt werden die beiden Operanden mit dem Operator verknüpft, z.B. (5)
* Zwischenergebnis speichern: Das Verknüpfungsergebnis wird als Zwischenergebnis für den nächsten Rechenschritt gespeichert und fließt dann als Ergebnis des aktuellen Rechenschritts hinaus.

## Interface
Wenn wir "in Flow Design denken", dann haben die Funktionseinheiten der Lösung keine Abhängigkeiten. Es gibt keine Objekthierarchie. Stattdessen werden Operationen von einer integrierenden Instanz zu Datenflüssen zusammengesteckt.

Wie das in F# für die Logik der Anwendung aussieht, soll hier nicht vorweggenommen werden. Um aber eine gewisse Vergleichbarkeit mit einer reinen C#-Lösung zu bekommen, sei ein kleinster gemeinsamer Nenner vorgegeben: ein Interface für das GUI.

	interface IGUI {
		event Action<double,string> Rechenschritt_ausführen;
		void Ergebnis_anzeigen(double zwischenergebnis);
	}

Statt _Action<double,string>_ könnte auch _Action&lt;Rechenschritt>_ oder _Action<Tuple<double,string>>_ benutzt werden. Welche Variante liegt F# am nächsten?

## Fragen an F# 
1. Wie werden fließende Tupel-Daten implementiert?
1. Wie werden Integrationen wie die Funktionseinheit _Berechnen_ implementiert? Diese Funktionseinheit des Modells darf in der Implementation nicht verlorengehen.
1. Wie wird Zustand implementiert, wie ihn einige der Operationen entweder für sich allein oder sogar gemeinsam mit anderen halten?
1. Wie wird die C#-Form eingebunden?

### Antworten bzg. _F#_
1. Tuple in F# werden direkt unterstütz und ein _Tuple<tA,tB,tC>_ kommt als _tA*tB*tC_ raus, ein Beispiel für _Tuple<Int, String, Double>_ bzw. _int*string*double_ wäre _(1, "Hallo", 2.0)_

1. Bin mir nicht 100% sicher was gemeint ist - ich hätte das jetzt als "Funktion" implementiert - wenn man das noch extra verpackt will, kann man seperate Module dafür anlegen.

1. Zustand ist ja eher unerwünscht, deshalb bekommt ihn die Berechen-Einheit als zustäzliche Eingabe und liefert einen solchen als Ausgabe, die Zustandsverwaltung wird dann so weit wie möglich hochgedrückt - in meinem Implementationsfall bis in die Runtime/MailboxProzessor. Das ist normalerweise auch der Punkt, wo man Monaden einführen kann (es gibt einen State-Monad).

1. Ganz genau wie die C#-Form auch - die Übersetzung ist ähnlich einfach wie C#->VB.net ... einfach mal in den Quelltext schauen (weiß nicht ob es in VS2012 bzw. F# 1.0 schon ging, aber man kann jetzt nicht nur Dlls und Consolenanwendungen, sondern auch Windowsanwendungen kompilieren, s.d. auch kein hässliches Consolenfenster zustäzlich erscheint - nur die Designertools gibts (noch?) nicht, aber auch da kann man abhilfe schaffen, z.B. mit den XAML-Typprovider von S. Forkmann).

## Anforderungen - Iteration 2
Der Taschenrechner soll nun erweitert werden um Zifferntasten. Dafür fällt allerdings die direkte Eingabe einer Zahl weg.

![](https://raw.github.com/ralfw/fsharp_retreat/master/beispiele/taschenrechner/doc/gui%20skizze v2.png)

## Flow
#### Root Flow
Zur bisherigen Interaktion der Operatoreingabe kommt die der Zifferneingabe hinzu:

![](http://yuml.me/742e2a47)

	[GUI Zifferneingabe]->(Zahl aktualisieren)->[GUI Ergebnis anzeigen]

Dadurch verändert sich aber auch der bisherige Flow, denn nun kommt der aktuelle Operand nicht mehr aus dem UI. Es ist ja nicht mehr für seine Bestimmung zuständig.

![](http://yuml.me/f8fd74e3)

	[GUI Rechenschritt ausfuehren]->(Zahl entnehmen)->(Berechnen)->[GUI Ergebnis anzeigen],
	(Berechnen)->(Zahl eintragen)

Die Operationen _Zahl aktualisieren_ und _Zahl entnehmen_ teilen sich natürlich Zustand. Eine weitere Verfeinerung scheint jedoch nicht nötig.

Nach der Entnahme ist die aktuelle Zahl auf 0 zurückgesetzt.

__Zusammenfassung:__

![](http://yuml.me/ecd5bb4c)

	[GUI Zifferneingabe]-Ziffer>(Zahlenwerk)-Zahl>[GUI Ergebnis anzeigen],
	[GUI Rechenschritt ausfuehren]-Op>(Zahlenwerk)-Rechenschritt>(Berechnen)-Zahl>[GUI Ergebnis anzeigen],
	(Berechnen)-Zahl>(Zahlenwerk)

## Interface

	interface IGUI {
		event Action<string> Rechenschritt_ausführen;
		event Action<string> Zifferneingabe;
		void Ergebnis_anzeigen(double zwischenergebnis);
	}

