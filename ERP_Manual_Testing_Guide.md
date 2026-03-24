# ERP System - Manual Testing Guide

This guide provides a step-by-step workflow to manually test the ERP System API using Swagger UI.

## 🚀 Prerequisites

1.  **SQL Server**: Ensure your local SQL Server instance is running.
2.  **Database**: The `ERP_System` database should be created via migrations.
    *   Command: `dotnet ef database update --project ERP_System.Infrastructure --startup-project ERP_System.API`
3.  **Run the API**: Start the application.
    *   Command: `dotnet run --project ERP_System.API`
4.  **Open Swagger**: Navigate to `https://localhost:5001/swagger` (or the port shown in your terminal).

---

## 🛠 Step-by-Step Testing Workflow

Follow this order to ensure data dependencies are met.

### 1. Categories (The Foundation)
You cannot create a product without a category.
- **Action**: `POST /api/categories`
- **Payload**:
  ```json
  {
    "categoryName": "Electronics",
    "description": "Gadgets and devices"
  }
  ```
- **Verify**: Use `GET /api/categories` to see your new category and its `id`.

### 2. Products (The Inventory)
- **Action**: `POST /api/products/create`
- **Payload**:
  ```json
  {
    "productName": "Wireless Mouse",
    "sku": "MSE-001",
    "price": 25.99,
    "costPrice": 15.00,
    "categoryId": 1,
    "description": "High-precision optical mouse"
  }
  ```
- **Verify**: Use `GET /api/products/{id}`. Note the `productId`.

### 3. Customers (The Buyers)
- **Action**: `POST /api/customers`
- **Payload**:
  ```json
  {
    "customerName": "John Doe",
    "email": "john.doe@example.com",
    "phoneNumber": "123-456-7890",
    "address": "123 Main St"
  }
  ```
- **Verify**: Use `GET /api/customers/{id}`. Note the `customerId`.

### 4. Warehouse & Stock (The Storage)
- **Action**: `POST /api/warehouse`
- **Payload**:
  ```json
  {
    "name": "Main Distribution Center",
    "location": "North Sector"
  }
  ```
- **Action**: Add Stock via `POST /api/stock/add`
- **Payload**:
  ```json
  {
    "productId": 1,
    "warehouseId": 1,
    "quantity": 100
  }
  ```

### 5. Orders (The Transaction)
- **Action**: `POST /api/orders`
- **Payload**:
  ```json
  {
    "customerId": 1,
    "notes": "Fast delivery requested",
    "items": [
      {
        "productId": 1,
        "quantity": 2
      }
    ]
  }
  ```
- **Verification Logic**:
  - Check `GET /api/stock` -> The quantity for Product 1 should have decreased from 100 to 98.
  - Check `GET /api/orders/{id}` -> Should show the order with "John Doe" and "Wireless Mouse".

---

## 🏗 How Everything is Connected (Architecture)

The system follows **Clean Architecture**. Here is how a single request (e.g., "Create Product") moves through the code:

1.  **API Layer (`ProductsController`)**: Receives the HTTP POST request. It doesn't contain logic; it just "packs" the data into a `CreateProductCommand` and sends it to **MediatR**.
2.  **Application Layer (`CreateProductCommandHandler`)**: This is the "brain".
    *   It uses **FluentValidation** to check if the price is valid.
    *   It calls the **Repository** to check if the SKU already exists.
    *   It uses **AutoMapper** to convert the Command into a **Domain Entity**.
3.  **Domain Layer (`Product` Entity)**: Contains the core business rules (e.g., "Price cannot be negative").
4.  **Infrastructure Layer (`ProductRepository`)**:
    *   Uses **EF Core** to save the product to SQL Server.
    *   Uses **Dapper** (in Queries) for high-performance data retrieval.
5.  **Persistence**: The data is committed to the SQL database via the **Unit of Work** (ensuring all-or-nothing transactions).

---

## 🗺 Full Endpoint Directory

### 📦 Products & Categories
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/categories` | Create a new product category (electronics, clothing, etc). |
| `GET` | `/api/categories` | List all available categories. |
| `POST` | `/api/products/create` | Register a new product. Requires a valid `categoryId`. |
| `GET` | `/api/products` | Get all active products (uses Dapper for speed). |
| `GET` | `/api/products/{id}` | Get detailed info for one product. |

### 👥 Customers
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/customers` | Add a new customer to the database. |
| `GET` | `/api/customers` | List all customers. |

### 🏠 Warehouse & Stock
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/warehouse` | Create a storage location. |
| `POST` | `/api/stock/add` | Increase stock quantity for a product in a warehouse. |
| `GET` | `/api/stock` | View current inventory levels across all warehouses. |

### 🛒 Orders
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `POST` | `/api/orders` | **The Master Action**: Creates an order, validates customer/product status, and **automatically deducts stock**. |
| `GET` | `/api/orders/{id}` | View order details including items and total price. |

---

## 🔄 End-to-End Business Workflow

Follow this "Success Path" to see the full system in action:

1.  **Identity**: Create a **Category** (ID: 1) -> Create a **Product** (ID: 1) linked to Category 1.
2.  **Logistics**: Create a **Warehouse** (ID: 1) -> Add 100 units of **Stock** for Product 1 in Warehouse 1.
3.  **CRM**: Create a **Customer** (ID: 1).
4.  **Sales**: Create an **Order** for Customer 1 with 10 units of Product 1.
5.  **Validation**:
    *   Check `/api/stock`: Verify quantity is now **90**.
    *   Check `/api/orders`: Verify the order exists and the price matches `Product.Price * 10`.

---

## ⚡ Troubleshooting & Common Gotchas

| Issue | Cause | Solution |
| :--- | :--- | :--- |
| **Route not found** | `CreatedAtAction` suffix mismatch | Use `CreatedAtRoute` with explicit names like `GetProductById`. |
| **FK Constraint Error** | EF Core trying to update Category | In Repository, set `Entry(product.Category).State = Unchanged`. |
| **IsActive is always False** | Dapper Column Ordering | Ensure `p.IsActive` comes BEFORE `p.CategoryId` in the SELECT list. |
| **Validation Failed** | Product is Inactive | Check DB or use `GET /api/products` to verify `IsActive: true`. |
