%include "io.inc" ;NASM
section .data
BD_dato1 db "00005.10",0
BD_delta db "-0100.00",0

section .text
global CMAIN
CMAIN:
    
    mov ebp, esp; for correct debugging
    xor eax, eax
    xor ebx, ebx
    xor ecx, ecx
    
    lea esi,[BD_dato1]
    push esi ;0x402000
    call length 
    add esp,4
    ;mov ecx, 8
    lea esi,[BD_dato1] ; dir OpeA
    push esi
    lea edx,[BD_delta] ; dir opeB
    push edx
    lea edi,[BD_dato1] ; dir Result    
    push edi
    push ecx
    mov ecx, 2
    push ecx
L6: 
    call subtrac
    loop L6
    mov esp, ebp
    xor   eax,eax
    ret  ;CMAIN
     
 ;-----------------------------------------------  
 ; length  input: direcci?n base del array en la pila
 ;         output; tama?o del array en ECX 
length:
    push    ebp
    mov     ebp, esp
    mov     edi,[ebp+8]
    xor ecx,ecx
Lini:mov al, [edi + ecx]
     cmp al,0
     je fin
     inc ecx
     jmp Lini
     
fin: dec ecx
     mov esp, ebp
     pop ebp
     ret   
;------------------------------------------------

subtrac:
    
    push    ebp
    mov     ebp, esp
    mov [ebp + 8], ecx
    xor eax, eax
    mov esi, [ebp+24]
    mov edx, [ebp+20]
    mov edi, [ebp+16]
    ;+4 dir call
    mov ecx, [ebp+12]
    call ver
L1: mov al, [ esi+ ecx]
    mov bl, [ edx + ecx]   
    cmp al, '.'
    je L2
    add al, ah
    xor ah, ah
    cmp al, bl
    jge L3
    add al, 0ah
    mov ah, 0fh 
    push ecx
    mov ecx, [edi]
    cmp ecx, '-'
    pop ecx
    jne L3
    add al,bl
    aaa 
    add al, 30h
    jmp L2
L3: sub al, bl
    AAS
    add al, 30h
L2: mov [edi +ecx], al
    dec ecx
    cmp ecx, -1
    jne L1
    mov ecx, [ebp+8]
    mov esp, ebp
    pop ebp
    ret
    
ver:
    push ecx
    push eax
    push ebx
    not ecx
L5:    
    mov al, [ esi+ ecx]         ; pasa los valores de izquierda a derecha
    mov bl, [ edx + ecx]        ; para poder verificar el numero mas grande
    cmp al, '.'
    je L7
    cmp al, bl
    jge L7
    mov [esi + ecx], 2dh    ; coloca '-' un espacio a la izquierda en el dato1 
L7:                             ; si el delta es mayor
    mov [edi +ecx], al
    inc ecx
    cmp ecx, 0
    jne L5
    pop ebx
    pop eax
    pop ecx
    ret