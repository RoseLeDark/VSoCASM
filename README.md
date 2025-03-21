# VM-SoC ISA Assembler (`vsocasm`)

A tool to compile code for the **EVM-ISA** (Embedded VM Instruction Set) architecture.  
`vsocasm` supports different platforms and architectures, giving you full control over the compilation process.

---

## 📑 Table of Contents
1. [Overview](#overview)
2. [Example](#example)
3. [Compiling](#compiling)
4. [Program Arguments](#program-arguments)
5. [License](#license)

---

## Overview
`vsocasm` is a modular assembler for the **EVM-ISA** architecture.  
It allows you to define your own architectures, instruction sets, and platforms, giving you flexible control over the build process.

The EVM-ISA is designed for **Embedded VM systems** – minimal, extensible, and hardware-friendly.

---

## Example

```asm
#include "system.inc"

ORG  $H

.dim Foo
.dim Bar = "Hallo Welt!"

ORG 16h

	MOV RC, .Interrupt ; Interrupt address pointing to label: Interrupt
Main:
	ADD #5d, #3d, R1  ; R1 = 5 + 3 (decimal)
	MOV R0, R1        ; move R1 to R0
	PUSH R0
	
	HLT               ; Halts the program
	
Interrupt:
	JMP .Main         ; Jumps back to the main program
```

---

## Compiling

```bash
vsocasm -a x86 -i main.asm -o prog.bin
```

- **-a**: Choose the architecture/platform (e.g., `x86`). This corresponds to a subfolder under `Platform`.  
- **-i**: The source file to compile (required).  
- **-o**: Output file for the compiled program. Default: `0.bin`.  
- **-d**: Dumps the instructions to the console without actually creating a binary file.  
- **-h**: Show help.

---

## Program Arguments

| Argument        | Description                                                                                                                                |
|-----------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| `-h`            | Displays this help message.                                                                                                                |
| `-a, --arch`    | Specifies the target architecture (ISA). Refers to the subfolder under `Platform/`. Default: `'32bit'`.                                   |
| `-d, --dump`    | Dumps the assembly instructions as a listing to the console without creating a binary output.                                              |
| `-o, --output`  | Name of the output file for the compiled program. Default: `'0.bin'`.                                                                     |
| `-i, --input`   | The input file to compile. **Required argument**.                                                                                          |

---

## License

### European Union Public License (EUPL)

The **RobbiAdaptAI** project (including `vsocasm`) is licensed under the **European Union Public License (EUPL)**.

The **EUPL** is:
- Compatible with other open-source licenses like GPL and LGPL.
- Specifically tailored for the legal framework within the European Union.
- Recognized by EU institutions.

#### Key Points:
- You are free to **use**, **modify**, and **distribute** the code.
- If you publish modifications of the code, you must make them available under the same **EUPL** license.
- The license offers legal certainty within the EU.

👉 Full license text:  
[European Union Public License (EUPL)](LICENSE.txt)  
Or online: [https://eupl.eu/](https://eupl.eu/)

---

### ToDos / Suggestions
- Maybe add a chapter about **Registers** (`README.md#register`)?  
  For example:
  ```markdown
  ## Registers
  - R0-Rn: General-purpose registers
  - F0-F3: Float registers
  - XM0-XM3: eXtended Math float registers
  - ...
  ```
- An **Instruction Overview** (`README.md#instructions`) could also be helpful:
  ```markdown
  ## Instructions
  - `MOV`: Copies values
  - `ADD`: Adds values
  - `HLT`: Halts the VM
  - ...
  ```
