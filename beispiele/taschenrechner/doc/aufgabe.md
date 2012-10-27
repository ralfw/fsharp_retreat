# Taschenrechner
## Anforderungen
Es soll ein Taschenrechner entwickelt werden. Der kann die Grundrechenarten ohne Operatorpräzedenzen (2+3*4=20).

Das UI sieht so aus:

![](GUI skizze.png)

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

