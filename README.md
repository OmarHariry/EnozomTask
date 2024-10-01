# EnozomTask
# AspDotNetCore Library System

This project is a ASP.NET Core-based library system that manages books, copies, students, and borrow records. It uses repository, unit of work and service design patterns for clean and maintainable code.

## Requirements

- ASP.NET Core
- Entity Framework Core
- SQL Server

## Design Patterns

This project employs several design patterns to enhance code organization and maintainability:

- **Generic Repository Pattern**: Provides a reusable repository interface and implementation for data access, allowing for a clean separation of concerns and easier testing. This pattern helps in managing CRUD operations without redundancy.

- **Service Layer Pattern**: Encapsulates the business logic of the application, providing a clear interface for the controllers. This separation improves the organization of the code and allows for easier maintenance and testing.

- **Unit of Work Pattern**: Manages changes to multiple repositories within a single transaction. This pattern ensures that all changes are committed together, helping to maintain data integrity and consistency.


## Installation

1. Clone the repository:
    ```bash
   git clone https://github.com/OmarHariry/EnozomTask.git
   cd EnozomTask/App
    ```
    
2. Update your connection string in appsettings.json file
    ```dotenv
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost\\SQLEXPRESS;database=EnozomDB;encrypt=False;Integrated Security=True;"
    }
    ```

3. Run migrations PM console to create the database schema:
    ```bash
    Add-Migration InitialCreate
    Update-Database 
    ```
4. Run the Application:
    ```bash
    dotnet run
    ```

## Database Schema

The database schema consists of the following tables:

### Books Table

| Column     | Type       | Description            |
|------------|------------|------------------------|
| id         | INT     | Primary key            |
| title      | STRING     | Title of the book      |


### Copies Table

| Column     | Type       | Description                     |
|------------|------------|---------------------------------|
| id         | INT     | Primary key                     |
| bookId    | INT     | Foreign key to books table      |
| statusId  | INT     | Foreign key to statuses table   |


### Students Table

| Column         | Type       | Description                   |
|----------------|------------|-------------------------------|
| id             | INT     | Primary key                   |
| name           | STRING     | Name of the student           |
| email          | STRING     | Email of the student          |
| phone          | STRING     | Phone number of the student   |


### BorrowingRecords Table

| Column             | Type       | Description                       |
|--------------------|------------|-----------------------------------|
| id                 | INT     | Primary key                       |
| studentId         | INT     | Foreign key to students table     |
| copyId            | INT     | Foreign key to copies table       |
| borrowedDate        | TIMESTAMP  | Timestamp of borrowing            |
| expectedReturnDate | TIMESTAMP  | Expected return timestamp         |
| actualReturnedDate        | TIMESTAMP  | Actual return timestamp           |
| statusId  | INT     | Foreign key to statuses table   |

### BookStatus Table

| Column     | Type       | Description            |
|------------|------------|------------------------|
| id         | INT     | Primary key            |
| status      | STRING     | status of the copy      |


## Main Endpoints

- `GET /api/report` - Generate a report of books and its status
- `POST /api/book/borrow/{copyId}/{studentId}` - Borrow a book from the library
- `POST /api/book/return/` - Return a book to the library
