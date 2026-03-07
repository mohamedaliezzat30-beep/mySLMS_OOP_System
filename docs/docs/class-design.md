# Class Design

This document describes the object-oriented design used in the Smart Library Management System (SLMS).

The system follows core Object-Oriented Programming principles including **encapsulation, inheritance, and separation of responsibilities**.

---

## System Architecture

The application is structured into three main components:
Models/
Services/
Users/


Each component is responsible for a different part of the system.

---

# Core Classes

## Book

The `Book` class represents a book stored in the library catalogue.

Properties

- Id  
- Title  
- Author  
- BorrowedByUserId  
- ReservedByUserId  
- DueDay  
- PickupUntilDay  

Responsibilities

- Store information about a book
- Track borrowing status
- Track reservations

---

## User (Base Class)

The `User` class represents a general system user and serves as a base class.

Properties

- Id  
- Username  
- Password  
- Name  

Responsibilities

- Provide shared functionality for all users
- Define common attributes

---

## Member (inherits from User)

The `Member` class represents a normal library member.

Responsibilities

- Borrow books
- Return books
- Reserve books
- View borrowed books

Members interact with the `Library` class to perform actions.

---

## Admin (inherits from User)

The `Admin` class represents the system administrator.

Responsibilities

- View overdue books
- Advance the system day
- Generate system reports

---

## Library

The `Library` class manages the entire book collection.

Responsibilities

- Store the list of books
- Handle borrowing operations
- Handle book returns
- Manage reservations
- Track system day

---

## UserStore

The `UserStore` class manages user persistence.

Responsibilities

- Load saved users
- Save users to storage
- Generate new user IDs

This ensures user accounts remain available after the system is restarted.

---

## Relationships Between Classes

User
│  
├── Member  
└── Admin  

Library manages a collection of `Book` objects.

Members interact with the library to borrow, return, and reserve books.

UserStore handles persistent storage of user accounts.
