# Reservation and Admin Pseudocode

## Reserve Book Process

```text
DISPLAY borrowed books
INPUT selected book

IF selected book is currently borrowed THEN
    ADD reservation for current user
    DISPLAY "Reservation successful"

ELSE
    DISPLAY "Reservation not allowed"
ENDIF


--------------


# Reservation and Admin Pseudocode

## Reserve Book Process

```text
DISPLAY borrowed books
INPUT selected book

IF selected book is currently borrowed THEN
    ADD reservation for current user
    DISPLAY "Reservation successful"

ELSE
    DISPLAY "Reservation not allowed"
ENDIF


-----------------


WHEN a user account is created OR updated
    SAVE user data to file

WHEN the system starts
    LOAD user data from file
