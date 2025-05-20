โปรเจกต์ **CRUD Product API** โดยใช้:

✅ C# 12
✅ Clean Architecture
✅ Dapper + MSSQL
✅ Global Middleware (Exception + Response Wrapper)

#

## 📦 ProductAPI (.NET 8 + C# 12)

ระบบ WebAPI จัดการข้อมูลสินค้า (CRUD Product) พัฒนาโดยใช้แนวทาง **Clean Architecture**, **Dapper**, และ **Middleware แบบ Global**

---

### 🧱 Project Structure

```
ProductAPI/
├── Api/                  → API Layer (Controllers)
│   ├── Controllers/
├── Middleware/
│       ├── ApiResponseWrapperMiddleware.cs
│       └── ExceptionHandlingMiddleware.cs
│
├── Application/         → Application Layer (DTOs, Interfaces, Services)
│   ├── Interfaces/
│   ├── Services/
├── Common/
│   ├── ApiResponse<T>
│
├── Domain/              → Domain Layer (Entities)
│   └── Entities/Product.cs
│
├── Infrastructure/      → Data Access (Dapper, SQL Connection)
│   └── Repositories/ProductRepository.cs
```

#

### 🚀 Features

* ✅ Clean Architecture พร้อมแยก Layer ชัดเจน
* ✅ ใช้ Dapper สำหรับ ORM
* ✅ ใช้ `record`, `with expression`, และ `primary constructor` จาก **C# 12**
* ✅ รองรับ Global Middleware:

  * Global Exception Handling
  * Response Wrapper (`ApiResponse<T>`)
* ✅ ตัวอย่างการ Simulate Exception
 
#

### 🔧 Tech Stack

| Item       | Description                                   |
| ---------- | --------------------------------------------- |
| .NET       | .NET 8 SDK                                    |
| Language   | C# 12                                         |
| ORM        | Dapper                                        |
| DB         | MSSQL Local / Azure SQL                       |
| Arch       | Clean Architecture                            |
| Middleware | Custom Middleware for Exception + ApiResponse |
| Tooling    | Postman, Swagger, Serilog (optional)          |

#

### 🧪 Sample Product Data

```json
{
  "id": 0,
  "name": "Mechanical Keyboard",
  "description": "RGB Mechanical Keyboard with blue switches",
  "price": 1890.00,
  "stock": 25
}
```

#

### 📥 Run Locally

#### ✅ Clone

```bash
git clone https://github.com/your-org/ProductAPI.git
cd ProductAPI
```

#### ✅ Update Connection String (in `appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductDb;Trusted_Connection=True;"
  }
}
```

#### ✅ Create DB Table

```sql
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18,2),
    Stock INT
);
```

#### ✅ Run

```bash
dotnet run --project ProductAPI.Api
```

#

### 📘 API Endpoints

| Method | URL                               | Description                   |
| ------ | --------------------------------- | ----------------------------- |
| GET    | `/api/product`                    | Get all products              |
| GET    | `/api/product/{id}`               | Get product by ID             |
| POST   | `/api/product`                    | Create product                |
| PUT    | `/api/product/{id}`               | Update product                |
| DELETE | `/api/product/{id}`               | Delete product                |
| GET    | `/api/product/simulate-exception` | Simulate Exception (for test) |

#

### 🔁 Response Format

```json
{
  "statusCode": 200,
  "success": true,
  "message": "Product retrieved",
  "data": { ... },
  "error": null
}
```

#

### 💥 Exception Example

```json
{
  "statusCode": 500,
  "success": false,
  "message": "Internal Server Error",
  "data": null,
  "error": {
    "exception": "NullReferenceException",
    "details": "Product not found!"
  }
}
```

#

### 📌 Future Enhancements

* ✅ Add Serilog Logging
* ✅ Add FluentValidation
* ✅ Add Unit & Integration Tests
* ⏳ Add Swagger Security / Authentication

#

### 👨‍💻 Author

* \[Nuchit Atjanawat]
* \[nuchit2019]

#
