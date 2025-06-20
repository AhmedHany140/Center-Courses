# Center Courses - My First MVC Project 🎓

> **Milestone Achievement**: This project marks my first full-scale ASP.NET Core MVC implementation, incorporating modern architectural patterns and payment integration.

---

## 🌟 Project Significance 

As my inaugural MVC project, this represents:
- Successful implementation of MVC architecture
- First use of Repository Pattern with EF Core
- Initial experience with ASP.NET Core Identity
- First Code-First database approach
- First usage of AutoMapper for clean data transformation

---

## 🛠️ Tech Stack Breakdown

| Category        | Technologies                          |
|-----------------|---------------------------------------|
| **Framework**   | ASP.NET Core MVC                      |
| **Database**    | SQL Server (EF Core Code-First)       |
| **Auth**        | ASP.NET Core Identity                 |
| **Frontend**    | HTML, CSS, JavaScript, Razor Views    |
| **Patterns**    | Repository Pattern, Dependency Injection |
| **Tools**       | Visual Studio 2022, SQL Server Mgmt Studio |

---

## 🚀 Key Features Implemented

### 🎓 Core Functionality
- **Course Management System**
  - Three learning tracks: Frontend, Backend, and Full Stack
  - Dynamic course enrollment
  - Role-based access control (students/admins)

### ⚙️ Technical Achievements
- **First MVC Structure**:
```mermaid
graph TD
    A[Controller] --> B[Services]
    B --> C[Repositories]
    C --> D[DbContext]
