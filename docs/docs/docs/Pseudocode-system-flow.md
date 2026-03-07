# System Flow Pseudocode

## Main Program Flow

```text
START PROGRAM

LOAD saved users
INITIALISE library
LOAD book catalogue

WHILE program is running

    DISPLAY login menu
    INPUT username
    INPUT password

    IF administrator login is valid THEN
        OPEN administrator menu

    ELSE
        IF user account exists THEN
            LOG IN user
        ELSE
            CREATE new user account
        ENDIF

        OPEN member menu
    ENDIF

END WHILE

END PROGRAM
