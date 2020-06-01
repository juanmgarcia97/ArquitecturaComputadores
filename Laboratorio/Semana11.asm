.386
.model flat, stdcall
option casemap : none
.data
IPHeader byte 45h, 00h,  00h, 28h, 14h, 02ch, 40h, 00h, 2fh, 06h, 74h, 7dh, 5bh, 0e4h, 0a6h, 2dh, 0c0h, 0a8h, 00h, 6dh

;Comprobar también con Header #2 y Header #3
.code
start:
    mov esi,0
    xor eax, eax
    mov ah, byte ptr[IPHeader]
    shl ah, 4
    shr ah, 4
    mov al, byte ptr[IPHeader]
    shr al, 4
    mul ah
    xor edx, edx
    mov dx, ax 
    xor ebx, ebx
    xor eax, eax
L11:mov   ax,word ptr[IPHeader+esi]; AX : AH AL
    xchg  al,ah 
    add esi,2
    add bx, ax
    jnc L16
    add bx, 1
L16:cmp esi, edx
    jne L11
    not bx
    xor eax, eax
    ret
    end start
