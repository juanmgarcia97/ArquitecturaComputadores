.386
.model flat, stdcall
option casemap : none
.data
myBytes BYTE 10h,20h,30h,40h
myWords WORD 8Ah,3Bh,72h,44h,66h
myDoubles DWORD 1,2,3,4,5
myPointer DWORD myDoubles

.code
start:
    mov esi,OFFSET myBytes
    mov al,[esi]
    mov al,[esi+3]
    mov esi,OFFSET myWords + 2
    mov ax,[esi]
    mov edi,8
    mov edx,[myDoubles + edi]
    mov edx,myDoubles
    mov ebx,myPointer
    mov eax,[ebx+4]
    end start