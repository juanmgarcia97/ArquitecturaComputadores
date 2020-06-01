    PA EQU 30h
    PB EQU 31h
    CB EQU 33h
    
    EOI EQU 20h
    IMR EQU 21h
    
    INT0 EQU 24h
    INT1 EQU 25h
    INT2 EQU 26h
    INT3 EQU 27h
    ORG 2000H       ; Memoria de Datos

    ; LEDS - PB out()
    ; Interr - PA in(30h)
    mov AL, 0e6h ; Id IRQ0
    out (INT0), AL
    mov AL, 0feh ; Id IMR
    out (IMR), AL
    IN AL,(PA)
    
    MOV AL, 00h
    OUT (CB), AL
    
LP: MOV AL, 55h
    OUT (PB), AL ; Secuencia 55h en los LEDs
    NOT AL
    OUT (PB), AL ; Secuencia aah en los LEDs
    JMP LP
    
    ORG 3000h ; subrutina de atencion a la interurpcion
    
    IN AL,(PA)
    inc bx
    mov AL, 20h
    out (EOI), AL
    iret
    
    ORG 0398h ;IDx4
    dir dw 3000h

    
    END
    