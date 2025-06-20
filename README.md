# BookStore
A **RESTful Web API** for a BookStore application, built with **ASP.NET Core** and **Entity Framework Core**, applying Clean Architecture principles.

Tech Stack

- **Backend**: ASP.NET Core Web API (.NET 6)
- **ORM**: Entity Framework Core (EF Core) + MSSQL Server
- **Architecture**: Clean Architecture, Repository Pattern, UnitOfWork Pattern, 
- **Authentication and Authorization**: ASP.NET Core Identity with JWT Bearer Tokens
- **Object mapping**: AutoMapper
- **Dev Tools**:Postman

API endpoints
### Authentication

| Method | Route                                 | Description             | Auth Required |
|--------|---------------------------------------|-------------------------|---------------|
| POST   | `/api/v1/authenticate/login`          | Login user              | -             |
| POST   | `/api/v1/authenticate/register`       | Register new user       | -             |
| POST   | `/api/v1/authenticate/register-admin` | Register new admin      | -             |

---

### Authors

| Method | Route                    | Description         | Auth Required |
|--------|--------------------------|---------------------|---------------|
| GET    | `/api/v1/authors`        | Get all authors     | -             |
| GET    | `/api/v1/authors/{id}`   | Get author by ID    | -             |
| POST   | `/api/v1/authors`        | Create new author   | Admin         |
| PUT    | `/api/v1/authors/{id}`   | Update author       | Admin         |
| DELETE | `/api/v1/authors/{id}`   | Delete author       | Admin         |

---

### Books

| Method | Route                                | Description             | Auth Required |
|--------|--------------------------------------|-------------------------|---------------|
| GET    | `/api/v1/books`                      | Get all books           | -             |
| GET    | `/api/v1/books/{id}`                 | Get book by ID          | -             |
| GET    | `/api/v1/books/by-author/{id}`       | Get books by author     | -             |
| GET    | `/api/v1/books/by-category/{id}`     | Get books by category   | -             |
| GET    | `/api/v1/books/by-publisher/{id}`    | Get books by publisher  | -             |
| POST   | `/api/v1/books`                      | Create new book         | Admin         |
| PUT    | `/api/v1/books/{id}`                 | Update book             | Admin         |
| DELETE | `/api/v1/books/{id}`                 | Delete book             | Admin         |
| POST   | `/api/v1/books/{id}/reviews`         | Create book review      | User          |

---

### Categories

| Method | Route                        | Description           | Auth Required |
|--------|------------------------------|-----------------------|---------------|
| GET    | `/api/v1/categories`         | Get all categories    | -             |
| GET    | `/api/v1/categories/{id}`    | Get category by ID    | -             |
| POST   | `/api/v1/categories`         | Create category       | Admin         |
| PUT    | `/api/v1/categories/{id}`    | Update category       | Admin         |
| DELETE | `/api/v1/categories/{id}`    | Delete category       | Admin         |

---

### Publishers

| Method | Route                        | Description           | Auth Required |
|--------|------------------------------|-----------------------|---------------|
| GET    | `/api/v1/publishers`         | Get all publishers    | -             |
| GET    | `/api/v1/publishers/{id}`    | Get publisher by ID   | -             |
| POST   | `/api/v1/publishers`         | Create publisher      | Admin         |
| PUT    | `/api/v1/publishers/{id}`    | Update publisher      | Admin         |
| DELETE | `/api/v1/publishers/{id}`    | Delete publisher      | Admin         |

---

### Reviews

| Method | Route                              | Description                 | Auth Required |
|--------|------------------------------------|-----------------------------|---------------|
| GET    | `/api/v1/reviews`                  | Get all reviews             | Admin         |
| GET    | `/api/v1/reviews/by-book/{id}`     | Get reviews for a book      | User/Admin    |
| DELETE | `/api/v1/reviews/{id}`             | Delete own or any (admin)   | User/Admin    |


---

## Notes

- All paginated GET endpoints include `X-Pagination` metadata in response headers.

---
