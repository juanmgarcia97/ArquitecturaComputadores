%include "io.inc"
section .data
BD_A db "1567.34"
BD_B db "6935.57"
BD_C db "0000.00"

section .text
global CMAIN
CMAIN:
    mov ebp, esp; for correct debugging
    xor eax, eax
    xor ebx, ebx
    xor ecx, ecx
    xor edx, edx
    
    mov edx, 6
ST: mov al, [BD_A+edx]
    mov bl, [BD_B+edx]
    cmp al, '.'
    je L23
    add al,bl
    aaa 
    add al, 30h
    cmp ah, 0
    jnz L22
    mov cl, [BD_C+edx]
    cmp ah, 0
    jnz L23
L22:add al, bh
    aaa
    add al, 30h
    xor bh, bh
L23:mov [BD_C+edx], al
    mov bh, ah
    xor ah,ah
L31:dec edx
    cmp edx, 0
    jmp ST
    
    xor eax, eax
    ret