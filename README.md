# VM-SoC ISA Assembler

## Overview
vsocasm is a tool used to compile code for the VM-SoC Instruction Set Architecture (ISA). 
It allows you to specify various arguments to control the compilation process and customize its behavior. 

### Example

```
#include "system.inc"
ORG  $H
.dim Foo
.dim Bar = "Hallo Welt!"

ORG 16h
	MOV RC, .Interrupt ; Interrupt Adress to Label: Interrupt
Main:
	ADD #5d, #3d, R1 ; R1 = 5+3 decimal
	MOV R0, R1 ; mov r1 to r0 
	PUSH R0
	
	HLT ; Never 

Interrupt:
	JMP .Main
```

### Compelieren
```
vsocasm -a x86 -i main.asm -o prog.bin
```

#### Programm Using
Arguments:
- -h: Show this help text
- -a, --arch: Specifies the platform's ISA to use. The sub folder under "Platform" name will be used. [default: '32bit']
- -d, --dump: Dumps the instructions to the output, without actually performing the compilation.
- -o, --output: Specifies the output file name for the compiled program. [default: '0.bin']
- -i, --input: Specifies the input file to compile. This is a required argument.

### License: European Union Public License (EUPL)

The **RobbiAdaptAI** project is licensed under the **European Union Public License (EUPL)**. The EUPL is compatible with other open-source licenses like the GPL and LGPL, and it is specifically tailored to meet the legal framework of the European Union. 

#### Key Points:
- The **EUPL** allows you to freely use, modify, and distribute the **VM-SoC Assembler** code within the EU, under the conditions laid out in the license.
- If you modify the code and distribute it, you must also make the source code of your modifications available under the same **EUPL** license.
- The EUPL offers legal certainty for users and contributors within the EU and is recognized by European institutions.

You can find the full terms of the **EUPL** at:  
[European Union Public License (EUPL)](LICENSE.txt) and online: https://eupl.eu/

