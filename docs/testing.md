# System Testing

This document records the testing performed on the Smart Library Management System (SLMS).

Testing was conducted to verify that the system behaves correctly and satisfies the functional requirements.

---

# Test Strategy

The system was tested using **requirement-based testing**.

Each test case is linked to one or more functional requirements defined in the requirements document.

Testing focused on the following areas:

- User authentication
- Viewing the book catalogue
- Borrowing books
- Returning books
- Reservation behaviour
- System limits and edge cases

---

# Test Cases

| Test ID | Requirement | Test Description | Expected Result | Actual Result |
|--------|-------------|-----------------|----------------|--------------|
| TC01 | FR1 | Login as administrator | Admin menu displayed | Pass |
| TC02 | FR3 | Login with new username | Account created automatically | Pass |
| TC03 | FR5 | View book catalogue | List of books displayed | Pass |
| TC04 | FR8 | Borrow available book | Book borrowed successfully | Pass |
| TC05 | FR9 | Borrow same book twice | System prevents duplicate borrowing | Pass |
| TC06 | FR10 | Borrow more than 5 books | Borrowing denied | Pass |
| TC07 | FR12 | Return borrowed book | Book returned successfully | Pass |
| TC08 | FR15 | Reserve borrowed book | Reservation recorded | Pass |

---

# Edge Case Testing

The following edge cases were tested to ensure the system handles unusual input safely.

| Scenario | Expected Behaviour | Result |
|---------|-------------------|-------|
| Invalid menu input | System displays error message | Pass |
| Empty username input | System asks for valid input | Pass |
| Borrow book already borrowed by user | Borrowing blocked | Pass |
| Return book not borrowed | System shows error message | Pass |

---

# Debugging and Fixes

During testing several issues were identified and corrected.

Issue 1  
Users were initially able to borrow the same book multiple times.  
This was resolved by adding a validation check before borrowing.

Issue 2  
Invalid menu inputs previously caused incorrect behaviour.  
Input validation was added to ensure the program handles unexpected input safely.

After implementing these fixes, all tests were executed again and passed successfully.
