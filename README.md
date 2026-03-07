# Smart Library Management System (SLMS) 

## Project Description 

The Smart Library Management System (SLMS) is a console-based application developed in C# using Object-Oriented Programming (OOP) principles. 
The system simulates a small digital library where members can browse books, borrow and return them, and reserve books that are currently unavailable. 
Administrators can monitor the system and view reports. 

This project was developed as part of coursework for Object-Oriented Analysis and Design.

---

# System Features

## Member Features

Members can perform the following actions:

- Create a new account
- View the list of available books
- Borrow books from the library
- Return borrowed books
- Reserve books that are currently borrowed
- View currently borrowed books

---

## Administrator Features

Administrators have additional system management capabilities:

- View the full library catalogue
- View overdue books
- Advance system time
- Generate a simple system report

---

## Book Catalogue

The system contains a predefined catalogue of well-known books:

- The Great Gatsby – F. Scott Fitzgerald
- 1984 – George Orwell
- To Kill a Mockingbird – Harper Lee
- Pride and Prejudice – Jane Austen
- The Hobbit – J.R.R. Tolkien
- The Catcher in the Rye – J.D. Salinger
- Moby Dick – Herman Melville
- War and Peace – Leo Tolstoy
- Crime and Punishment – Fyodor Dostoevsky
- The Lord of the Rings – J.R.R. Tolkien

---

## Object-Oriented Design

The project demonstrates key object-oriented programming concepts.

### Inheritance
The `Member` and `Admin` classes inherit from the base `User` class.

### Encapsulation
Classes manage their internal data and expose behaviour through controlled methods.

### Polymorphism
Different user types implement their own menu behaviour using overridden methods.

---

## Repository Structure
Models/
Book.cs

Services/
Library.cs
ConsoleUI.cs
Input.cs

Users/
User.cs
Member.cs
Admin.cs
UserStore.cs

Program.cs


---

## How to Run the System

1. Open the project in **Visual Studio**
2. Build the solution
3. Run the program using:
Ctrl + F5 
