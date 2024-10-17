# Lagermanagementsystem

Es soll ein Lagermanagementsystem implementiert werden.
Dieses soll folgende Funktionen unterstützen:
- Einlagern von Waren
- Entnehmen von Waren
- Aufräumen des Lagers
- Aktuell eingelagerte Waren anzeigen.

### Rahmenbedingungen
- Das Lager hat 10 Regale mit je 20 Stellplätzen.

### Einlagerung:
- Es kann ein Name vergeben werden für das eigelagerte Gut
- Ware die eingelagert wird verbraucht eine eingegebene Menge Stellplätze
- Ware die eingelagert wird kann nicht auf mehrere Regale aufgeteilt werden
- Wenn die Ware nicht eingelagert werden kann (kein Platz) wird ein Fehler ausgegeben.

### Eingelagerte Waren anzeigen:
- Es wird eine Liste der Regale aufgeführt.
- In jedem Regal werden die eingelagerten Waren angezeigt (Hier exemplarisch für 12 Stellplätze):

Regal 1:
- 0 Artikel 1
- 1 -
- 2 -
- 3 Artikel 2
- 4 -
- 5 Artikel 3
- 6 -
- 7 -
- 8 -
- 9 -
- 10
- 11

- Besetzte Stellplätze werden durch Striche markiert.
- Als Namen werden die eingegebenen Namen angezeigt.

### Entnehmen:
- Es wird zuerst das Regal angegeben
- Anschließend wird der Stellplatz angegeben an dem die zu entnehmende Ware steht (erster Platz den der Artikel belegt)
- Die Ware wird dann aus dem Regal "gelöscht"
- Es wird ausgegeben welche Ware entnommen wurde
- Falls ein dem Regal an dem Stellplatz keine Ware existiert, wird ein Fehler ausgegeben

### Aufräumen:
- Durch das Entnehmen von Waren aus den Regalen können Lücken entstehen
- Das Aufräumen schließt diese Lücken (alle Waren rücken auf)

