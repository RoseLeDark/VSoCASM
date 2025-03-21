# VM-SoC ISA Assembler (`vsocasm`)

Ein Tool zum Kompilieren von Code für die **EVM-ISA** (Embedded VM Instruction Set) Architektur.  
`vsocasm` unterstützt verschiedene Plattformen und Architekturen und bietet dir volle Kontrolle über den Kompilierungsprozess.

---

## 📑 Inhaltsverzeichnis
1. [Überblick](#überblick)
2. [Beispiel](#beispiel)
3. [Kompilieren](#kompilieren)
4. [Programm Argumente](#programm-argumente)
5. [Lizenz](#lizenz)

---

## Überblick
`vsocasm` ist ein modularer Assembler für die **EVM-ISA** Architektur.  
Er erlaubt dir, eigene Architekturen, Instruktionssätze und Plattformen zu definieren und deinen Build-Prozess flexibel anzupassen.

Die EVM-ISA ist für **Embedded VM Systeme** gedacht – minimalistisch, erweiterbar und hardwarefreundlich.

---

## Beispiel

```asm
#include "system.inc"

ORG  $H

.dim Foo
.dim Bar = "Hallo Welt!"

ORG 16h

	MOV RC, .Interrupt ; Interruptadresse auf Label: Interrupt
Main:
	ADD #5d, #3d, R1  ; R1 = 5 + 3 (dezimal)
	MOV R0, R1        ; R1 nach R0 kopieren
	PUSH R0
	
	HLT               ; Stoppt das Programm
	
Interrupt:
	JMP .Main         ; Springt zurück zum Hauptprogramm
```

---

## Kompilieren

```bash
vsocasm -a x86 -i main.asm -o prog.bin
```

- **-a**: Wähle die Architektur / Plattform (z.B. `x86`). Wird als Ordner unter `Platform` verwendet.  
- **-i**: Die Quelldatei, die kompiliert werden soll (erforderlich).  
- **-o**: Zieldatei des kompilierten Programms. Standard: `0.bin`.  
- **-d**: Zeigt einen Dump der Instruktionen an, ohne die eigentliche Kompilierung auszuführen.  
- **-h**: Hilfe anzeigen.

---

## Programm Argumente

| Argument        | Beschreibung                                                                                                                                         |
|-----------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| `-h`            | Zeigt diese Hilfe an.                                                                                                                                |
| `-a, --arch`    | Gibt die Zielarchitektur (ISA) an. Verweist auf den Unterordner unter `Platform/`. Standard: `'32bit'`.                                              |
| `-d, --dump`    | Gibt die Assemblierung als Listing auf der Konsole aus, ohne eine Binärdatei zu erstellen.                                                           |
| `-o, --output`  | Name der Ausgabedatei für das kompilierte Programm. Standard: `'0.bin'`.                                                                             |
| `-i, --input`   | Die Eingabedatei, die kompiliert werden soll. **Pflichtargument**.                                                                                   |

---

## Lizenz

### European Union Public License (EUPL)

Das **RobbiAdaptAI** Projekt (inklusive `vsocasm`) steht unter der **European Union Public License (EUPL)**.

Die **EUPL** ist:
- Kompatibel mit anderen Open-Source-Lizenzen wie GPL und LGPL.
- Speziell für die Rechtsordnung innerhalb der Europäischen Union ausgelegt.
- Von EU-Institutionen anerkannt.

#### Wichtige Punkte:
- Du darfst den Code frei **verwenden**, **verändern** und **weitergeben**.
- Änderungen am Code, die du veröffentlichst, müssen ebenfalls unter der **EUPL** zur Verfügung gestellt werden.
- Die Lizenz bietet rechtliche Sicherheit innerhalb der EU.

👉 Vollständige Lizenzbestimmungen:  
[European Union Public License (EUPL)](LICENSE.txt)  
Oder online: [https://eupl.eu/](https://eupl.eu/)

---

### ToDos / Vorschläge
- Vielleicht ein Kapitel zu den **Registern** (`README.md#register`)?  
  Zum Beispiel:
  ```markdown
  ## Register
  - R0-Rn: Allgemeine Register
  - F0-F3: Float Register
  - XM0-XM3: eXtend Math float Register
  - ...
  ```
- Eine **Instruktionsübersicht** (`README.md#instruktionen`) könnte auch helfen:
  ```markdown
  ## Instruktionen
  - `MOV`: Kopiert Werte
  - `ADD`: Addiert Werte
  - `HLT`: Stoppt die VM
  - ...
  ```


