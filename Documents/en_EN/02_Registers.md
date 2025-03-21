# 2. Registers

The **EVM-ISA** register model is streamlined and minimal, offering full control where needed and a clean separation between programmer-accessible and internal registers.

---

## General-Purpose Registers (`R0` - `RF`)

- **16 general-purpose registers**, labeled `R0` to `RF`.
- Each register can store **32-bit** or **64-bit** values, depending on the Platform.
- There is **no special-purpose R0**; however, `RA` is **always 0** and typically acts as a null value or discard target.

#### Example:
```asm
MOV R1, #5d, REGISTER_FULL_MASK
ADD #10d, R1, R2 ; R2 = 5 + 10 → R2 = 15
```

---

## Floating-Point Registers (`F0` - `F3`)

- 4 **32-bit float registers**.
- Used exclusively for **floating-point operations**.

#### Example:
```asm
FMOV F1, #3.14f
FMOV F2, #2.0f
FADD F1, F2, F4 ; F4 = 3.14 + 2.0 → F4 = 5.14
```

---

## Extended Math Registers (`XM0` - `XM3`)

- 4 **extended math registers**, optimized for **vector math** and **SIMD**-style operations.
- Width and configuration depend on `extendMath.inc`.

#### Example:
```asm
#include 'system.inc'
#include 'extendMath.inc'

.dim exERG EXTEND_MATH_REGISTER_SIZE
.dim exRE  EXTEND_MATH_REGISTER_SIZE

eXPUSH #3.14f, XM0
eXPUSH #4.9f, XM0
eXPUSH #0.1f, XM0

eXPUSH #2.0f, XM1
eXPUSH #3.0f, XM1

eXADD  XM0, XM1, #3d
exMOV  XM0, XME

eXDIV  XM0, XM1, #2d
exMOV  exERG, XME
exMOV  exRE, XMF

PUSH NO_ERROR
RET
```

---

## Instruction Pointer (IP)

- **`IP`** (Instruction Pointer) replaces what other systems call `PC`.  
- Automatically **increments** after each instruction.  
- Controls **program flow**, but **cannot be directly modified**, except via:
  - **JMP**
  - **CALL**
  - **RET**
- **Programmer Control via RF Register**:
  - **RF register** can be used to influence the **IP** in the context of a **normal execution**, especially when coming from **ISR** context.

#### Example:
```asm
JMP label
label:
    NOP
```

---

## Return Address Register (`RA`)

- `RA` always holds **0**.
- Acts as a **null destination** or **discard register**.
- Pushing `RA` onto any stack is equivalent to **deleting** the value:

#### Example:
```asm
PUSH RA, R3, R5 ; Deletes the top of the stack, like writing to /dev/null
```

---

## Status Flags (FLAGS)

The **FLAGS register** holds important condition codes, updated by arithmetic and logic instructions.

### Available Flags:
| Flag         | Description                              |
|--------------|------------------------------------------|
| **CF**    | Carry occurred in the last operation     |
| **OF** | Arithmetic overflow detected             |
| **UF**| Arithmetic underflow detected            |
| **ZF** | Result is zero (Zero flag)               |
| **SF**| Result is negative (Signed flag)        |
| **IPFF**| From ISR Context new IP setted | 

#### Usage:
- Flags are **automatically** updated.

---

## Programmer-Controlled Stack (Custom Stack)

You control your own **custom stack(s)** with:
- `R3` → **Start address**  
- `R5` → **End address / Current pointer**

### Behavior:
- Stack **grows downward**: `R5` decrements on **PUSH**.  
- Stack is **full** when `R3 == R5`.  
- Stack is **empty** when `R5 == Initial End Address`.

#### PUSH Example:
```asm
MOV R3, #120d   ; Start address of stack
MOV R5, #135d   ; End address (initial pointer)

PUSH #2d, R3, R5 ; Store 2d at [R5], R5 = 134
PUSH #10d, R3, R5 ; Store 10d at [R5], R5 = 133
```

#### POP Example:
```asm
POP R1, R3, R5  ; Load from [R5], R5 = 134, R1 = 10d
POP R2, R3, R5  ; Load from [R5], R5 = 135, R2 = 2d
```

---

## Internal CALL Stack

- **CALL** and **RET** use an **internal call stack**.
- This call stack is **not visible** or **modifiable**.
- `CALL` stores the **return address** (IP) on this **internal stack**.
- You **cannot access or manipulate** this stack in user code.

---

## Interrupt Service Routine (ISR) and Instruction Pointer (IP)

### Key Concept:
When entering an **Interrupt Service Routine (ISR)**, the **ISR context** takes over the execution flow. Manipulating the **Instruction Pointer (IP)** during ISR requires special care.

### Problem with Direct IP Manipulation in ISR Context:
- If you use `MOV IP, ...` during ISR execution, you can unintentionally **corrupt the RET stack** or cause **conflicting control flow**.
- The **IP register** is **automatically updated** during ISR execution, and direct manipulation can cause the system to **return to unexpected addresses** after exiting the ISR.

### Safe Method to Control IP in ISR Context:
- If you need to modify the IP in **ISR Context** and have the system return to a specific location, you must set a **flag** indicating the desired action after exiting ISR.
- You can store the new IP in **RF register** and use a flag to signal that the IP should be modified after ISR execution.

#### Example for handling IP modification after ISR:
1. Set a flag in **FLAGS register** to indicate that **IP should be modified** after exiting ISR.
2. In the main code, check this flag and **adjust the IP** using the value stored in **RF register**.

#### Example:
```asm
ORG 4h
ISR_HANDLER:
    ; Do ISR work
	POP R4 ; Get IInterrupt code
	POP R5 ; Get Interrupt ARG
	
    STO  IPFF ; Set flag to modify IP after ISR
	MOV RF, .Main ; Set IP to new Adress after ISR while IPFF is set
	
	PUSH #0d ;  Return 0
    RET

ORG 16h
Main:
    ..... ; Working code
   HLT ; Halt Core , start after Interrupt
```

---

### Interacting with IP in the Main Code via ISR:

If you want to modify the IP based on the ISR context after the ISR has returned, 
you can leverage the **RF** to store the new IP. 
By setting an appropriate flag (like `IPFF`), you can ensure that the **IP** is safely 
updated once the ISR is completed, without corrupting the **RET stack** or affecting other interrupts.

---

## Register Summary Table

| Register Set  | Name(s)         | Purpose                                 |
|---------------|-----------------|-----------------------------------------|
| General       | `R0` - `RF`     | General-purpose integer registers       |
| Floating-Point| `F0` - `F3`     | Core bit length * 2 float operations    |
| Extended Math | `XM0` - `XM3`   | SIMD / vector / high-precision math     |
| Special       | `IP`            | Instruction pointer (internal)          |
| Special       | `RA`            | Return Address (always zero)            |
| Flags         | `FLAGS`         | Carry, Overflow, Underflow, Zero, Sign  |

---

### RA and /dev/null Analogy

`RA` works like a **black hole** or **/dev/null**:
- Any operation writing to `RA` results in **nothing**.
- Any **PUSH RA, R3, R4** effectively **discards** the data.

#### Example:
```asm
PUSH RA, R3, R5 ; Deletes the value at the top of the stack
```

---

👉 For more details, see:  
- **`03_Memory.md`** for memory addressing  
- **`05_Instructions.md`** for PUSH, POP, CALL usage  

