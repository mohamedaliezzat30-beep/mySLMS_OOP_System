# Book Operations Pseudocode

## Borrow Book and return Process

```text
DISPLAY available books
INPUT selected book

IF selected book is already borrowed by current user THEN
    DISPLAY "You already borrowed this book"

ELSE IF current user has borrowed 5 books THEN
    DISPLAY "Borrowing limit reached"

ELSE IF selected book is available THEN
    ASSIGN book to current user
    SET due date
    DISPLAY "Book borrowed successfully"

ELSE
    DISPLAY "Book is unavailable"
ENDIF
----
DISPLAY borrowed books
INPUT selected book

IF selected book belongs to current user THEN
    REMOVE book from current user
    MARK book as available
    DISPLAY "Book returned successfully"

ELSE
    DISPLAY "Invalid return"
ENDIF
