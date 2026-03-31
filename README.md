# Library Management System API

A comprehensive **RESTful API** for managing library operations built with **.NET 8** using a clean 3-layer architecture pattern.

## 🎯 Project Overview

The Library Management System is a modern, scalable backend service designed to handle all aspects of library operations including book management, user administration, borrowing records, fines tracking, and system settings. The system implements industry best practices with separation of concerns, dependency injection, comprehensive unit testing, and JWT-based authentication.

---

## ✨ Key Features

### 📚 **Book Management**
- Add, update, delete, and retrieve books
- Manage book copies with availability tracking
- Organize books by categories and authors
- Book details including ISBN, publication date, and additional metadata

### 👥 **User Management**
- User registration and authentication
- Role-based access control (Admin, Staff, User roles)
- JWT token generation for secure API access
- Refresh token management for extended sessions

### 🔄 **Borrowing System**
- Borrow and return books with automatic tracking
- Due date management
- Automatic fine calculation for late returns
- Borrowing history and records

### 💰 **Fine Management**
- Automatic fine generation for overdue books
- Fine amount calculation based on late days
- User fine history and summary

### 🔧 **System Configuration**
- Configurable default borrow duration
- Adjustable fine rates per day
- Centralized settings management

### 🔐 **Security & Authorization**
- JWT-based authentication
- Role-based authorization on endpoints
- Refresh token support for session extension
- Password hashing with BCrypt

---

## 🏗️ Architecture

### **3-Layer Architecture Pattern**

```
┌─────────────────────────────────────────┐
│        API Layer (Controllers)           │
│    - HTTP Request Handling              │
│    - Validation & Authorization         │
│    - Error Response Formatting          │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│    Business Logic Layer (Services)       │
│    - Business Rules Implementation      │
│    - Data Transformation (DTOs)         │
│    - AutoMapper Integration             │
│    - Service Orchestration              │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│   Data Access Layer (Repositories)       │
│    - Database Operations                │
│    - Stored Procedure Calls             │
│    - Data Entity Mapping                │
└─────────────────────────────────────────┘
```

---

## 📦 Project Structure

```
LibraryMS_API/
│
├── Library.API/                    # ASP.NET Core Web API Layer
│   ├── Controllers/                # API endpoint controllers
│   │   ├── AuthController.cs
│   │   ├── BooksController.cs
│   │   ├── UsersController.cs
│   │   ├── BorrowingController.cs
│   │   ├── BooksCategoriesController.cs
│   │   ├── BooksCopiesController.cs
│   │   ├── AuthorsController.cs
│   │   ├── FinesController.cs
│   │   └── SettingsController.cs
│   ├── DTOs/                       # Data Transfer Objects
│   └── Program.cs                  # Dependency Injection & Configuration
│
├── Library.BLL/                    # Business Logic Layer
│   ├── Services/                   # Service implementations
│   ├── Interfaces/                 # Service contracts
│   ├── AutoMapper/                 # DTO to Entity mappings
│   └── DI/                         # Dependency Injection registration
│
├── Library.DAL/                    # Data Access Layer
│   ├── Repositories/               # Repository implementations
│   ├── Interfaces/                 # Repository contracts
│   └── DI/                         # DAL DI registration
│
├── Library.Core/                   # Core Models & DTOs
│   ├── Models/                     # Database entities
│   └── Dtos/                       # Transfer objects
│
└── Library.BLL.Tests/              # Unit Tests
    ├── *ServiceTests.cs            # Service layer tests
```

---

## 🛠️ Technologies & Dependencies

### **Core Framework**
- **.NET 8** - Latest long-term support framework
- **ASP.NET Core 8** - Web API framework
- **Entity Framework Core** - ORM (for future implementation)

### **Authentication & Security**
- **JWT (JSON Web Tokens)** - Bearer token authentication
- **BCrypt.Net** - Password hashing
- **Microsoft.AspNetCore.Authorization** - Role-based access control

### **Data Access**
- **SQL Server** - Database provider
- **Stored Procedures** - Data access pattern
- **ADO.NET** - Direct database access

### **Business Logic & Mapping**
- **AutoMapper** - DTO to Entity mapping
- **Dependency Injection** - Built-in .NET DI container

### **Testing**
- **xUnit** - Unit testing framework
- **Moq** - Mocking library for unit tests

### **API Documentation**
- **Swagger/OpenAPI** - API documentation & testing

---

## 📋 API Endpoints Overview

### **Authentication**
```
POST   /api/Auth/Login              - User login (JWT token generation)
```

### **Books**
```
GET    /api/Books/GetAllBooks       - Retrieve all books
GET    /api/Books/GetBook           - Get specific book
POST   /api/Books/AddBook           - Create new book [Admin]
PUT    /api/Books/UpdateBook        - Update book [Admin]
DELETE /api/Books/DeleteBook        - Delete book [Admin]
```

### **Users**
```
GET    /api/Users/GetAllUsers       - List all users [Admin]
GET    /api/Users/GetUser           - Get user details
POST   /api/Users/AddUser           - Create user [Admin]
PUT    /api/Users/UpdateUser        - Update user [Admin]
DELETE /api/Users/DeleteUser        - Delete user [Admin]
```

### **Borrowing**
```
POST   /api/Borrowing/BorrowBook    - Borrow a book [Staff, Admin]
PUT    /api/Borrowing/ReturnBook    - Return a book [Staff, Admin]
GET    /api/Borrowing/CheckLateFine - Check late fine
GET    /api/Borrowing/GetBorrowingRecord - Get record details
GET    /api/Borrowing/GetAllBorrowingRecords - List all records [Admin]
```

### **Book Categories**
```
GET    /api/BooksCategories/GetAllCategories      - List all categories
GET    /api/BooksCategories/GetBookCategoryById   - Get category
POST   /api/BooksCategories/AddBookCategory       - Create category [Admin]
PUT    /api/BooksCategories/UpdateBookCategory    - Update category [Admin]
DELETE /api/BooksCategories/DeleteBookCategory    - Delete category [Admin]
```

### **Book Copies**
```
GET    /api/BooksCopies/GetAllBookCopies  - List all copies
GET    /api/BooksCopies/GetBookCopy       - Get copy details
GET    /api/BooksCopies/GetCopyByBook     - Get copy by book ID
POST   /api/BooksCopies/AddBookCopy       - Add copy [Admin]
DELETE /api/BooksCopies/DeleteBookCopy    - Delete copy [Admin]
```

### **Authors**
```
GET    /api/Authors/GetAllAuthors         - List all authors
GET    /api/Authors/GetAuthor             - Get author details
GET    /api/Authors/GetAuthorByName       - Search author by name
POST   /api/Authors/AddAuthor             - Add author [Admin]
PUT    /api/Authors/UpdateAuthor          - Update author [Admin]
```

### **Fines**
```
GET    /api/Fines/GetAllFines      - List all fines
GET    /api/Fines/GetFineById      - Get fine details
GET    /api/Fines/GetUserFines     - Get user fines with total
```

### **Settings**
```
GET    /api/Settings/GetSettings                    - Get system settings [Admin]
PUT    /api/Settings/UpdateDefaultBorrowDays        - Update borrow duration [Admin]
PUT    /api/Settings/UpdateDefaultFinePerDay        - Update fine rate [Admin]
PUT    /api/Settings/UpdateSettings                 - Update multiple settings [Admin]
```

---

## 🔐 Authorization & Roles

### **Role-Based Access Control**

| Endpoint Type | Public | Staff | Admin |
|--------------|--------|-------|-------|
| GET (Read)   | ❌     | ✅    | ✅    |
| POST (Create)| ❌     | ❌    | ✅    |
| PUT (Update) | ❌     | ❌    | ✅    |
| DELETE       | ❌     | ❌    | ✅    |

**Special Cases:**
- **BorrowBook** - Staff, Admin
- **ReturnBook** - Staff, Admin
- **CheckLateFine** - Staff, Admin

---

## 🚀 Getting Started

### **Prerequisites**
- .NET 8 SDK or later
- SQL Server 2019 or later
- Visual Studio 2022 or VS Code
- Git

### **Installation**

1. **Clone the repository**
```bash
git clone https://github.com/deemah-dev/LibraryMS_API.git
cd LibraryMS_API
```

2. **Install dependencies**
```bash
dotnet restore
```

3. **Configure database connection**
- Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=LibraryDB;Trusted_Connection=true;"
  },
  "JWT_SECRET_KEY": "your-secret-key-min-32-characters"
}
```

4. **Run the application**
```bash
cd Library.API
dotnet run
```

5. **Access the API**
- Swagger UI: `https://localhost:5001/swagger`
- API Base: `https://localhost:5001/api`

---

## 🧪 Unit Testing

### **Running Tests**

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test Library.BLL.Tests

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### **Test Coverage**

The project includes **56+ comprehensive unit tests** covering:
- ✅ Service layer operations (Add, Update, Delete, Get)
- ✅ Repository interactions with mocking
- ✅ DTO mapping and transformations
- ✅ Success and failure scenarios
- ✅ Null/empty edge cases

**Test Files:**
- `AuthorsServiceTests.cs` - 5 tests
- `BooksServiceTests.cs` - 7 tests
- `BorrowingServiceTests.cs` - 7 tests
- `BooksCategoriesServiceTests.cs` - 7 tests
- `BooksCopiesServiceTests.cs` - 6 tests
- `UsersServiceTests.cs` - 9 tests
- `FinesServiceTests.cs` - 3 tests
- `SettingsServiceTests.cs` - 6 tests
- `AuthServiceTests.cs` - 4 tests

---

## 📊 Request/Response Examples

### **Authentication - Login**
```http
POST /api/Auth/Login
Content-Type: application/json

{
  "username": "admin",
  "password": "SecurePassword123!"
}
```

**Response:**
```json
{
  "accessToken": "eyJhbGc...",
  "refreshToken": "base64encodedtoken...",
  "message": "Login successful"
}
```

### **Create Book**
```http
POST /api/Books/AddBook
Authorization: Bearer eyJhbGc...
Content-Type: application/json

{
  "title": "The Great Gatsby",
  "subTitle": "A Classic Novel",
  "authorId": 1,
  "categoryId": 2,
  "isbn": "978-0743273565",
  "publicationDate": "1925-04-10",
  "additionalDetails": "Fiction Classic"
}
```

**Response:**
```json
{
  "message": "Book created successfully.",
  "data": {
    "bookId": 1,
    "title": "The Great Gatsby",
    "isbn": "978-0743273565"
  },
  "bookId": 1
}
```

### **Borrow Book**
```http
POST /api/Borrowing/BorrowBook
Authorization: Bearer eyJhbGc...
Content-Type: application/json

{
  "userId": 1,
  "copyId": 1,
  "borrowingDate": "2024-01-15"
}
```

---

## 🔄 DTOs (Data Transfer Objects)

DTOs are used for API communication to decouple client-facing models from internal entities:

### **Request DTOs**
- `AddBookDTO` - Create book
- `UpdateBookDto` - Update book
- `AddBookCopyDto` - Add book copy
- `BorrowBookDto` - Borrow operation
- `ReturnBookDto` - Return operation

### **Response DTOs**
- `ReadBookDto` - Book with author & category
- `ReadBookCopyDto` - Copy with book details
- `ReadBorrowingRecordDto` - Record with user & copy

---

## 🔧 Configuration

### **appsettings.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LibraryDB;..."
  },
  "JWT_SECRET_KEY": "your-secret-key-here",
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### **Dependency Injection Setup**
The project uses .NET's built-in DI container configured in `Program.cs`:

```csharp
builder.Services.AddBusinessServices();  // BLL services
builder.Services.AddDataLayer();         // DAL repositories
builder.Services.AddAutoMapper();        // AutoMapper profiles
```

---

## 📝 Database Design

### **Key Entities**
- **Users** - System users with roles
- **Authors** - Book authors
- **BookCategories** - Book classification
- **Books** - Book details
- **BookCopies** - Physical book copies
- **BorrowingRecords** - Borrow/return tracking
- **Fines** - Late fee records
- **RefreshTokens** - Token management
- **Settings** - System configuration

---

## 🌟 Striking Points & Best Practices

### **✅ Clean Architecture**
- Clear separation of concerns (API, BLL, DAL)
- Loose coupling between layers
- Dependency Injection throughout

### **✅ Security First**
- JWT authentication on all sensitive endpoints
- Role-based authorization
- Secure password hashing with BCrypt
- Refresh token support

### **✅ Comprehensive Testing**
- 56+ unit tests with high coverage
- Moq framework for dependency isolation
- DTO and service layer testing

### **✅ Code Quality**
- Consistent naming conventions (PascalCase)
- Proper error handling with meaningful messages
- Unified response format across all endpoints

### **✅ Data Transfer Objects (DTOs)**
- Separation of API contracts from database models
- Automatic mapping with AutoMapper
- Type-safe data transformations

### **✅ Stored Procedure Pattern**
- Direct database access via stored procedures
- Parameterized queries to prevent SQL injection
- Optimized database operations

### **✅ RESTful API Design**
- Proper HTTP methods (GET, POST, PUT, DELETE)
- Meaningful status codes (200, 201, 400, 404, 409)
- Consistent endpoint naming

### **✅ Swagger/OpenAPI Integration**
- Auto-generated API documentation
- Interactive testing interface
- Clear endpoint descriptions

---

## 📚 Additional Resources

- [Microsoft .NET 8 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [ASP.NET Core Best Practices](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/best-practices)
- [JWT Authentication](https://jwt.io)
- [AutoMapper Documentation](https://automapper.org/)

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👤 Author

**Deemah Dev**  
GitHub: [@deemah-dev](https://github.com/deemah-dev)

---

## 📞 Support & Contact

For issues, questions, or suggestions, please open an issue on the [GitHub repository](https://github.com/deemah-dev/LibraryMS_API/issues).

---

**Last Updated:** January 2024  
**Version:** 1.0.0  
**Status:** ✅ Production Ready
