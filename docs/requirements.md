# System Requirements

This document describes the functional and non-functional requirements for the **Smart Library Management System (SLMS)**.

The system simulates a simple digital library where users can browse, borrow, return, and reserve books. Administrators can monitor the system and view reports.

---

## Functional Requirements

## User Authentication

FR1 – The system shall allow an administrator to log in using predefined credentials.

FR2 – The system shall allow normal users to log in using a username and password.

FR3 – If a user account does not exist, the system shall automatically create a new account.

FR4 – User accounts shall persist between system restarts.

---

## Book Browsing

FR5 – Users shall be able to view the full list of available books.

FR6 – Users shall be able to search for books by title or author.

FR7 – The system shall display the availability status of each book.

---

## Borrowing Books

FR8 – Users shall be able to borrow books that are currently available.

FR9 – A user shall not be able to borrow the same book more than once.

FR10 – A user shall not be able to borrow more than five books at the same time.

FR11 – When a book is borrowed, the system shall record the borrower and due date.

---

## Returning Books

FR12 – Users shall be able to return books they have previously borrowed.

FR13 – Returned books shall become available for borrowing again.

FR14 – If a book has reservations, the first user in the reservation list shall be notified.

---

## Reserving Books

FR15 – Users shall be able to reserve books that are currently borrowed.

FR16 – Reserved books shall be held for a limited time when returned.

FR17 – If the reservation is not collected within the allowed time, it shall expire.

---

## Administrator Features

FR18 – The administrator shall be able to view overdue books.

FR19 – The administrator shall be able to advance the system day.

FR20 – The administrator shall be able to view a system report summarising library activity.

---

## Edge Cases

The system must handle the following scenarios safely:

- Attempting to borrow a book already borrowed by the same user.
- Attempting to borrow more than five books.
- Attempting to return a book that was not borrowed.
- Attempting to reserve a book that is already reserved.
- Invalid menu inputs.
- Empty user input.

---

## Non-Functional Requirements

NFR1 – The system must run as a **console application**.

NFR2 – The system must follow **Object-Oriented Programming principles**.

NFR3 – The system must be structured into **separate classes and files**.

NFR4 – The system must handle invalid user input without crashing.

NFR5 – The system must be easy to build and run using **Visual Studio**.
