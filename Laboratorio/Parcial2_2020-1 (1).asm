%include "io.inc" ;NASM         Realizado por Douglas López, Wbeymerth Gallego 
section .data     ;             y Juan Martín García
BD_dato1 db "000005.30",0   ;   011025.10  
BD_delta db "001013.90",0   ;   000102.00
Result db "000000.00",0

section .text
global CMAIN
CMAIN:
    mov ebp, esp  ;for correct debugging
    xor eax, eax
    xor ebx, ebx
    xor ecx, ecx
    
    lea esi,[BD_dato1]
    push esi ;0x4020000
    call length 
    add esp,4
    lea esi,[BD_delta] ; direccion de BD_delta en memoria.
    push esi           ; esi= 0x40200a
    lea edx,[BD_dato1] ; direccion de BD_dato1 en memoria
    push edx           ;edx = 0x402000
    lea edi,[Result] ; dir Result    
    push edi          ; edi = 0x402014
    push ecx


comparador:
           xor al,al
           xor bl,bl
           
           mov al, [esi+ecx] ; delta eax
           mov bl, [edx+ecx] ; dato1 ebx
           cmp al,bl  ; compara si delta >= dato1
           jg L7
           je L5  
                    
L8:        inc bh
           dec ecx
           cmp ecx, 0
           je continuacion
           jmp comparador
           
L7:        inc ah
           dec ecx
           cmp ecx, -1
           je continuacion
           jmp comparador
           
L5:        dec ecx
           cmp ecx,0
           je continuacion
           jmp comparador
    
continuacion:  
            
             mov ecx, 10 ; cantidad de veces del ciclo.
             push ecx
             cmp ah, bh 
             jg  Ciclo ; comparo si delta es mayor a dato 1
             jle CicloMajor ;comparo su dato1 es mayor a delta
             
CicloMajor:  call subtractN
             Loop CicloMajor  ; para el caso en que dato1 > delta
             jmp terminado

Ciclo:       
             xor eax,eax
             xor ebx,ebx
Ciclo2:      call subtract    ; para el caso del parcial, va entrar a este. (delta > dato1)
             Loop Ciclo2  
             
                           
terminado:   mov esp, ebp
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
;   input: dos valores A y B a restar (cuando B > A, siendo A - B)
;   output: valor negativo
subtract:   
    push ebp
    mov  ebp, esp
    mov [ebp + 8], ecx
    mov esi, [ebp+24] ; 001013.90, 001013.90
    mov edx, [ebp+20] ; 000005.30, -01008.60 
    mov edi, [ebp+16] ; 000000.00, -01008.60
    mov ecx, [ebp+12] ; ecx = 8
    mov al, [edx]
    cmp al, 2dh
    je Suma
    
L1: 
    cmp ecx,0
    je L20
    jmp L21
L20:mov bh, '-'
    mov [edi+ecx], bh
    jmp final
L21:mov al, [ esi+ ecx] ; delta
    mov bl, [ edx + ecx]   ; dato1
    cmp al, '.'
    je L2
    add al, ah
    xor ah, ah
    cmp al, bl
    jge L3
    add al, 0ah
    mov ah, 0fh 
L3: sub al, bl
    AAS
    add al, 30h
L2: mov [edi +ecx], al  ;edi = 000000.00
    dec ecx
    cmp ecx, -1
    jne L1
final:mov ecx, [ebp+8]
      mov ebx, [Result]    
      mov eax, [Result+4]
      mov [BD_dato1], ebx
      mov [BD_dato1+4], eax
      mov ah, [Result+8]
      mov [BD_dato1+8],ah
      mov esp, ebp
      pop ebp
      ret
    
;------------------------------------------------- end subtract ---------------

Suma:
      xor al, al
      xor bl, bl
      cmp ecx, 0
      je final
      mov al,[edx+ecx] ;dato1
      cmp al,'.'
      je punto
      cmp ah,31h
      je carreo
carreo2:      
      mov bl,[esi+ecx] ;delta
      add al,bl
      AAA
      add al, 30h
      mov [edi+ecx], al
      dec ecx
      jmp Suma
carreo:
      add al,ah
      dec ah
      jmp carreo2
      
punto:mov [edi+ecx],al
      dec ecx
      jmp Suma
      
;------------------------------- end suma ----------------------
;   input: dos valores A y B a restar (cuando A > B, siendo A - B)
;   output: valor positivo 
   
subtractN: 
    push ebp
    mov  ebp, esp
    mov [ebp + 8], ecx
    xor eax, eax
    mov esi, [ebp+20] ;dato1
    mov edx, [ebp+24] ;delta
    mov edi, [ebp+16]
    mov ecx, [ebp+12]
L10: mov al, [ esi+ ecx] ;al = n-dato1
    mov bl, [ edx + ecx] ;bl = n-delta
    cmp al, '.'
    je L40
    add al, ah
    xor ah, ah
    cmp al, bl
    jge L30
    add al, 0ah
    mov ah, 0fh 
L30: sub al, bl
     AAS
     add al, 30h
L40: mov [edi +ecx], al
    dec ecx
    cmp ecx, -1
    jne L10
    mov ecx, [ebp+8]
    mov ebx, [Result]    
    mov eax, [Result+4]
    mov [BD_dato1], ebx
    mov [BD_dato1+4], eax
    mov ah, [Result+8]
    mov [BD_dato1+8],ah
    mov esp, ebp
    pop ebp
    ret