# Class Design

This document describes the object-oriented structure of the Smart Library Management System (SLMS).

The system follows key object-oriented principles including encapsulation, inheritance, and separation of responsibilities.

---

## System Structure

The project is organised into three main folders:

Models  
Services  
Users  

Each folder contains classes responsible for a specific part of the system.

---

## Book Class

The `Book` class represents a book stored in the library.

Properties

- Id
- Title
- Author
- BorrowedByUserId
- ReservedByUserId

Responsibilities

- Store book information
- Track borrowing status
- Track reservations

---

## User Class (Base Class)

The `User` class represents a general system user.

Properties

- Id
- Username
- Password
- Name

Responsibilities

- Provide shared attributes for all users
- Act as a base class for Member and Admin

---

## Member Class

The `Member` class inherits from `User`.

Responsibilities

- Borrow books
- Return books
- Reserve books
- View borrowed books

---

## Admin Class

The `Admin` class also inherits from `User`.

Responsibilities

- View overdue books
- Advance system time
- Generate system reports

---

## Library Class

The `Library` class manages the book collection.

Responsibilities

- Store books
- Handle borrowing and returning
- Manage reservations
- Track overdue books

---

## UserStore Class

The `UserStore` class manages user persistence.

Responsibilities

- Load users from storage
- Save users to storage
- Generate new user IDs
