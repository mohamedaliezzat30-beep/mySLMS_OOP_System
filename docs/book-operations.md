# Book Operations Pseudocode

This document describes the logic used for book borrowing, returning, and reservation processes.

---

## Borrow Book Process

DISPLAY list of available books  

INPUT selected book  

IF selected book is already borrowed by the current user THEN  
 DISPLAY "You already borrowed this book"  

ELSE IF the user has already borrowed 5 books THEN  
 DISPLAY "Borrowing limit reached"  

ELSE IF the selected book is available THEN  
 ASSIGN book to user  
 SET due date  
 DISPLAY "Book borrowed successfully"  

ELSE  
 DISPLAY "Book is not available"  

ENDIF  

---

## Return Book Process

DISPLAY list of books borrowed by the user  

INPUT selected book  

IF selected book belongs to the user THEN  
 REMOVE book from user account  
 MARK book as available  
 DISPLAY "Book returned successfully"  

ELSE  
 DISPLAY "Invalid return request"  

ENDIF  

---

## Reserve Book Process

DISPLAY borrowed books  

INPUT selected book  

IF selected book is currently borrowed by another user THEN  
 ADD reservation for the current user  
 DISPLAY "Reservation successful"  

ELSE  
 DISPLAY "Reservation not allowed for available books"  

ENDIF
