.386
.model flat, stdcall
option casemap : none


.data
ArrayNumbers DWORD 19, 21, 20, 18, 20
tam byte $-ArrayNumbers
sum DWORD 0
i DWORD 0
.code
start:
    mov     ecx, 5
L3:
    mov     eax, DWORD PTR [i]
    mov     eax, DWORD PTR [ArrayNumbers + eax*4]
    add     DWORD PTR [sum], eax
    inc     DWORD PTR [i]
    loop     L3
L2:
    xor eax, eax
    mov ebx, sum
    ret
    end start
