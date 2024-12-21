[![Top Language](https://img.shields.io/github/languages/top/Parasayte/Exam-Management-System?style=plastic&color=teal)](https://github.com/Parasayte/Exam-Management-System)
[![Repo Size](https://img.shields.io/github/repo-size/Parasayte/Exam-Management-System?style=plastic&color=yellowgreen)](https://github.com/Parasayte/Exam-Management-System)
[![Last Commit](https://img.shields.io/github/last-commit/Parasayte/Exam-Management-System?style=plastic&color=crimson)](https://github.com/Parasayte/Exam-Management-System/commits)
[![Community](https://img.shields.io/badge/Community-Active-gold?style=plastic)](https://github.com/Parasayte/Exam-Management-System)
[![Stars](https://img.shields.io/github/stars/Parasayte/Exam-Management-System?style=plastic&color=darkblue)](https://github.com/Parasayte/Exam-Management-System/stargazers)
[![Forks](https://img.shields.io/github/forks/Parasayte/Exam-Management-System?style=plastic&color=purple)](https://github.com/Parasayte/Exam-Management-System/network/members)


# Exam Management System  ![C# Project](https://img.shields.io/badge/Csharp-Project-%23200020?style=plastic)


## Project Overview 📋

The Exam Management System is a user-friendly platform that streamlines the management of exams, users, and results. With clearly defined roles for administrators, teachers, and students, the system offers an organized and efficient approach to handling exams, making it easy for each role to manage tasks and access necessary information.
---

## Key Features ⚙️

### 1. **Student Role** 👩‍🎓

- **Exam Access**: Students can log in to view available exams.
- **Result Checking**:Students can check their results once they have been recorded by the teacher.
- **Exam Participation**: Students can take exams with 5 question as configured by the teacher or admin.

### 2. **Teacher Role** 👨‍🏫

- **Exam Creation**: Teachers can delete or create exams with 5 number of questions.
- **Student Management**: Teachers can add students to the system.
- **Result Entry**: Teachers can input and update the results for students.

### 3. **Admin Role** 🛠️

- **User Management**: Admins can add or delete teachers and students.
- **Exam Management**: Admins can add or remove exams .

---

## System Design 🧩

### Roles and Permissions 🔐

- **Admin**: Full control over user and exam management.
- **Teacher**: Limited permissions for creating exams and managing students and results.
- **Student**: Restricted access to participate in exams and view results.


---

## Technical Specifications 🖥️

### Frontend 💻

- Basic user interfaces for:
  - Login and authentication.
  - Dashboards tailored to Admin, Teacher, and Student functionalities.
  - Simplified navigation for viewing and managing exams and results.

### Backend 🔙

- Role-based authentication to ensure proper access control.
- CRUD operations for managing users, exams, and results.
- Logic to handle exam participation and result storage.

### Database 💾

-  tables created for:
 - Exams, Teacher, and Student user data.

   

## Technology Stack ⚡

The Exam Management System is built using the following technologies:

- **Programming Language**: C#  ![C# Project](https://img.shields.io/badge/Csharp-Language-%23200020?style=plastic)
- **Database**: Microsoft SQL Server  	![SqlClient](https://img.shields.io/badge/SqlClient-Language-%23DC143C?style=plastic)
- **Development Tools**: Visual Studio  [![Visual Studio](https://custom-icon-badges.demolab.com/badge/Visual%20Studio-5C2D91.svg?&logo=visual-studio&logoColor=white&style=plastic)](#)



---
### **Student Role** 👩‍🎓

| **Screenshot**               | **Description**                                                                                       | **Link**                                                                                   |
|------------------------------|-------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| **Student Login 👩‍🎓**             | Login page for students to access the system.                                                         | ![Student Login](https://i.imgur.com/4Jtgyqd.png)                                          |
| **Incorrect User Data ❌**      | Error message displayed when incorrect user credentials are entered.                                  | ![Incorrect User Data](https://i.imgur.com/4J280sH.png)                                    |
| **Sign Up as Student ✍️**       | The page tells students to sign up by connecting with a teacher or admin.                                            | ![Sign Up as Student](https://i.imgur.com/f8Y8P2P.png)                                     |
| **Student Menu 🖥️**             | Dashboard displaying options available to students after logging in.                                  | ![Student Menu](https://i.imgur.com/PGtNAGV.png)                                           |
| **Exam Not Available 🚫**       | Message shown when there are no exams available for the student to take.                              | ![Exam Not Available](https://i.imgur.com/8CWaUjy.png)                                     |
| **Exam Page 📝**                | Page where students can view and take the exam.                                                       | ![Exam Page](https://i.imgur.com/FDcrWp5.png)                                              |
| **Finish Exam ✅**              | Confirmation page after completing the exam, showing the option to submit it.                         | ![Finish Exam 1](https://i.imgur.com/sLomR4d.png) ![Finish Exam 2](https://i.imgur.com/YGjQsnQ.png) |

---

### **Teacher Role 👨‍🏫**

| **Screenshot**               | **Description**                                                                                       | **Link**                                                                                   |
|------------------------------|-------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| **Teacher Login 👩‍🏫**            | Login page for teachers to access their account.                                                      | ![Teacher Login](https://i.imgur.com/E5vVcJo.png)                                          |
| **Incorrect Teacher Data ❌**   | Error message displayed when incorrect credentials are entered by the teacher.                        | ![Incorrect Teacher Data](https://i.imgur.com/yeXNMTN.png)                                |
| **Read Student Answers 📚**     | Page where teachers can read and review the students' exam answers.                                   | ![Read Student Answers](https://i.imgur.com/FsHC5eB.png)                                  |
| **Add Result ➕**               | Page where teachers can add or modify the results for students.                                       | ![Add Result](https://i.imgur.com/5yThhs8.png)                                             |
| **Add Exam Menu 📋**            | Menu page for teachers to add a new exam to the system.                                               | ![Add Exam Menu](https://i.imgur.com/fd7vqLc.png)                                          |
| **Add Exam 🖊️**                 | Form to create a new exam with questions for students.                                                | ![Add Exam](https://i.imgur.com/CIIU54e.png)                                               |
| **Delete Exam 🗑️**              | Option for teachers to delete an existing exam from the system.                                       | ![Delete Exam](https://i.imgur.com/ncob16s.png)                                            |
| **Add Student ➕**              | Page where teachers can add new students to the system.                                               | ![Add Student](https://i.imgur.com/z6Y7UTI.png)                                            |
| **Delete Student 🗑️**           | Option for teachers to remove students from the system.                                               | ![Delete Student](https://i.imgur.com/HYz2J3m.png)                                        |

---

### **Admin Role 🛠️**

| **Screenshot**               | **Description**                                                                                       | **Link**                                                                                   |
|------------------------------|-------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| **Admin Login 👨‍💻**              | Login page for admins to access the admin panel.                                                     | ![Admin Login](https://i.imgur.com/0VwANdb.png)                                            |
| **Incorrect Admin Password ❌** | Error message shown when incorrect login credentials are entered by the admin.                       | ![Incorrect Admin Password](https://i.imgur.com/YD5Swq6.png)                              |
| **Admin Menu ⚙️**               | Dashboard displaying options available to admins for managing users, exams, and other system settings. | ![Admin Menu](https://i.imgur.com/CwpH3MS.png)                                             |
| **Add Teacher ➕**              | Page where admins can add new teachers to the system.                                                | ![Add Teacher](https://i.imgur.com/dDOudhS.png) ![Add Teacher 2](https://i.imgur.com/UZqzJaq.png) |
| **Add Teacher ➕**              | Page where admins can add new teachers to the system.                                                | ![Add Teacher](https://i.imgur.com/Qu5ae4E.png)                                            |



---
## Database Schema 📑 :




 ```Sql
-- Create the Exam1 table
CREATE TABLE Exam1 (
    exam_id INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the exam
    exam_name NVARCHAR(100), -- Name of the exam
    student_id INT, -- ID of the student taking the exam
    q1 NVARCHAR(MAX), -- Question 1
    a1 NVARCHAR(MAX), -- Answer 1
    q2 NVARCHAR(MAX), -- Question 2
    a2 NVARCHAR(MAX), -- Answer 2
    q3 NVARCHAR(MAX), -- Question 3
    a3 NVARCHAR(MAX), -- Answer 3
    q4 NVARCHAR(MAX), -- Question 4
    a4 NVARCHAR(MAX), -- Answer 4
    q5 NVARCHAR(MAX), -- Question 5
    a5 NVARCHAR(MAX), -- Answer 5
    result FLOAT, -- Result or score
    finished CHAR(10) -- Status of completion
);

-- Create the Students table
CREATE TABLE Students (
    id INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the student
    name NVARCHAR(50), -- Full name of the student
    nick_name NVARCHAR(50), -- Nickname
    birth_date NCHAR(10), -- Birthdate of the student
    password NVARCHAR(50), -- Login password
    gmail NVARCHAR(50) -- Gmail or email address
);

-- Create the Teacher table
CREATE TABLE Teacher (
    teacher_id INT PRIMARY KEY IDENTITY(1,1), -- Unique identifier for the teacher
    teacher_name NVARCHAR(50) NOT NULL, -- Full name of the teacher
    password NVARCHAR(50), -- Login password
    gmail NVARCHAR(50), -- Gmail or email address
    birth_date DATE, -- Birthdate of the teacher
    phone_no NVARCHAR(15) -- Contact phone number
);
```
- Database Tables 📊 : 

![sql](https://i.imgur.com/Lk1VpLd.png)

---

## Implementation 🛠️

### Development Environment 🖥️

- **Language**: C#
- **Framework**: .NET (if applicable)
- **Database**: Any relational database supported by the application.

### Installation and Setup ⚙️

### Prerequisites 📋

- **System Requirements**: Ensure C# development tools (e.g., Visual Studio) are installed.
- **Database**: Set up a relational database like SQL Server or MySQL.

### Steps to Install ⬇️

1. Clone the repository:
   ```bash
   git clone https://github.com/Parasayte/Otomasyon.git




