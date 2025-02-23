# Blazor with PostgreSQL Learning Project

## 📚 About the Project
This is an educational project focused on learning Blazor, PostgreSQL, and development best practices. The project implements a modern architecture following the principles of Domain-Driven Design (DDD), Clean Code, and Object-Oriented Programming.

## 🛠 Technologies Used
- **Backend**: C# (.NET 8)
- **Frontend**: Blazor (Server-Side)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core

## 🏗 Project Architecture
The project follows a layered architecture based on DDD:

### Layers
1. **Domain Layer**
   - Domain entities
   - Repository interfaces
   - Business rules
   - Value Objects
   - Domain Events

2. **Application Layer**
   - Services
   - DTOs (Data Transfer Objects)
   - Service interfaces
   - Mappings

3. **Infrastructure Layer**
   - Repository implementations
   - Entity Framework Context
   - Database configurations
   - External services

4. **Presentation Layer (Blazor)**
   - Components
   - Pages
   - Services
   - Models

## 💡 Principles and Practices
- **Clean Code**
  - Meaningful names
  - Small, focused functions
  - DRY (Don't Repeat Yourself)
  - SOLID Principles

- **Domain-Driven Design**
  - Bounded Contexts
  - Aggregate Roots
  - Entities & Value Objects
  - Domain Events

- **Object-Oriented Programming**
  - Encapsulation
  - Inheritance
  - Polymorphism
  - Abstraction

## 🚀 How to Run the Project
1. Prerequisites
   - .NET 8 SDK
   - PostgreSQL
   - Visual Studio 2022 or VS Code

2. Database Setup
   ```bash
   # Create database
   createdb database_name
   
   # Apply migrations
   dotnet ef database update
   ```

3. Run the Application
   ```bash
   dotnet run
   ```

## 📦 Folder Structure
```
src/
├── Domain/
├── Application/
├── Infrastructure/
└── WebUI/
    └── Blazor/
tests/
├── Unit/
├── Integration/
└── E2E/
```

## 🎯 Learning Objectives
- Implement a complete application using Blazor
- Work with PostgreSQL and Entity Framework Core
- Apply DDD principles in practice
- Develop clean and maintainable code
- Understand and apply design patterns
- Implement automated tests

## 📝 Next Steps
1. Initial project setup
2. Domain implementation
3. Infrastructure configuration
4. Application layer development
5. User interface creation with Blazor
6. Test implementation
7. Continuous documentation

## 🤝 Contribution
This is an educational project, feel free to fork and adapt it to your learning needs.

## 📄 License
This project is under the MIT license.