    PA EQU 30h
    PB EQU 31h
    CB EQU 33h
    
    ORG 2000H       ; Memoria de Datos

    ; LEDS - PB out()
    ; Interr - PA in(30h)
    
    IN AL,(PA)
    
    MOV AL, 00h
    OUT (CB), AL
    
LP: MOV AL, 55h
    OUT (PB), AL ; Secuencia 55h en los LEDs
    NOT AL
    OUT (PB), AL ; Secuencia aah en los LEDs
    JMP LP
    
    END
    