# Course Enrollment System

## Project Overview

Course Enrollment System built with ASP.NET MVC and Entity Framework (Code-First). It manages students, courses, and enrollments. This system supports features like:

- **Course enrollment**.
- **Preventing duplicate enrollments** (students can’t enroll in the same course multiple times).
- **Ensuring course capacity** (prevents enrolling more students than the maximum capacity of a course).
- **Displaying available slots dynamically** using jQuery.

## Features

### 1. Student Management
- Add, edit, delete, and list students.
- Each student has the following attributes:
  - Full name (required).
  - Email (unique, required).
  - Birthdate (required).
  - National ID (required, max length 14).
  - Phone number (optional, max length 11).

### 2. Course Management
- Add, edit, delete, and list courses.
- Each course has the following attributes:
  - Title (required).
  - Description (optional).
  - Maximum capacity (required).

### 3. Enrollment Management
- Enroll students in courses.
- Prevent enrollment if the course is full.
- Prevent duplicate enrollments (if the student is already enrolled in the course).

### 4. JavaScript Feature
- On the **Enrollment Form**, dynamically display the number of available slots for courses.

## Technical Requirements

### 1. Architecture
- **Presentation Layer**: ASP.NET MVC.
- **Business Layer**: Services (e.g., `IEnrollmentService`) handle all logic.
- **Data Layer**: Entity Framework for database handling.
- Implement **dependency injection** to decouple layers.

### 2. Database
- Use **Entity Framework Core In-Memory Database**.
- Seed sample data (e.g., students and courses).

### 3. Controllers
- Create controllers for students, courses, and enrollments.
- Ensure logic is kept out of controllers.

### 4. UI/UX Features
- **jQuery Bonus**: Use jQuery for dynamically displaying available slots.
- **Pagination**: Add pagination to the Course List (Bonus).
- **MVVM and Partial Views**: Use partial views for displaying course or student details.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/TarekMohamed227/Course-Enrollment-System.git
