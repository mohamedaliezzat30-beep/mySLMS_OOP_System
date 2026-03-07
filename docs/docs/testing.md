# System Testing

This document describes the testing performed on the Smart Library Management System (SLMS).

Testing was conducted to ensure that the system satisfies the defined requirements and behaves correctly under normal and edge-case conditions.

---

## Test Strategy

The system was tested using **requirement-based testing**. Each test case is linked to one or more functional requirements.

Testing focused on:

- Core system functionality
- User authentication
- Book borrowing and returning
- Reservation handling
- Edge case behaviour

---

## Test Cases

| Test ID | Requirement | Test Description | Expected Result | Actual Result |
|-------|-------------|-----------------|---------------|--------------|
| TC01 | FR1 | Login as administrator | Admin menu displayed | Pass |
| TC02 | FR3 | Login with new username | Account created automatically | Pass |
| TC03 | FR5 | View book catalogue | List of books displayed | Pass |
| TC04 | FR8 | Borrow available book | Book borrowed successfully | Pass |
| TC05 | FR9 | Borrow same book twice | System prevents duplicate borrowing | Pass |
| TC06 | FR10 | Borrow more than 5 books | Borrowing denied | Pass |
| TC07 | FR12 | Return borrowed book | Book returned successfully | Pass |
| TC08 | FR15 | Reserve borrowed book | Reservation recorded | Pass |

---

## Edge Case Testing

The following edge cases were tested to ensure system robustness.

| Scenario | Expected Behaviour | Result |
|--------|------------------|-------|
| Invalid menu input | System displays error message | Pass |
| Empty username input | System requests valid input | Pass |
| Borrow book already borrowed by user | System blocks borrowing | Pass |
| Return book not borrowed | System displays error | Pass |

---

## Debugging and Fixes

During testing several issues were identified and resolved:

Issue 1  
Users were initially able to borrow the same book multiple times.  
This was fixed by adding a check to verify whether the user already borrowed the book.

Issue 2  
Invalid menu input previously caused incorrect behaviour.  
Input validation was added to ensure the system handles unexpected input safely.

After implementing these fixes, the tests were re-run and passed successfully.
