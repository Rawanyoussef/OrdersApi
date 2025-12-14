# ORDER
# Orders API - Simple Orders API with Redis Caching

## Overview

This project is a simple Application Programming Interface (API) built using ASP.NET Core for managing customer orders. A key feature of this project is the implementation of a Caching pattern using Redis to enhance the performance of order retrieval.

## Key Features

*   **Order Management (CRUD):** A complete API for creating, retrieving, and deleting orders.
*   **Redis Caching:** Utilizes Redis to temporarily store individual orders with a Time-To-Live (TTL) of 5 minutes, reducing database load.
*   **Database:** Uses Entity Framework Core for database interaction, with migrations already set up.
*   **Code Quality:** Adheres to clean architecture principles and design patterns such as the Repository Pattern and Service Layer, utilizing Dependency Injection (DI) and Async/Await for robust and maintainable code.

## Technologies Used

*   **ASP.NET Core**
*   **Entity Framework Core**
*   **Redis** (via the StackExchange.Redis library)
*   **AutoMapper** (for mapping Entities to DTOs)

## Project Structure

The project is divided into logical layers to ensure a clear separation of concerns:

| Project | Description |
| :--- | :--- |
| `OrdersApi` | Presentation Layer - Contains the Controllers and the application entry point. |
| `Orders.Services` | Service Layer - Holds the business logic, coordinates operations, and implements the caching logic. |
| `Orders.Repository` | Repository Layer - Provides an abstraction interface for data access operations. |
| `Orders.Data` | Data Layer - Contains the Entity models and the database context (DbContext). |

## Order Model

| Field | Type | Description |
| :--- | :--- | :--- |
| `Id` | `Guid` | The unique identifier for the order. |
| `CustomerName` | `string` | The name of the customer. |
| `Product` | `string` | The name of the product ordered. |
| `Amount` | `decimal` | The monetary value of the order. |
| `CreatedAt` | `DateTime` | The date and time the order was created. |

## API Endpoints

| Operation | Expected Path (RESTful) | Current Path in Code | Description |
| :--- | :--- | :--- | :--- |
| **Create** | `POST /orders` | `POST /api/Order/CreateOrder` | Creates a new order. |
| **Get All** | `GET /orders` | `GET /api/Order/GetAllOrder` | Retrieves a list of all orders. |
| **Get By ID** | `GET /orders/{id}` | `GET /api/Order/GetById/{id}` | Retrieves a specific order (uses Redis Cache). |
| **Delete** | `DELETE /orders/{id}` | `DELETE /api/Order/DeleteOrder/{id}` | Deletes an order from the database and Redis Cache. |

---
**Note on Routing:** The current paths in the code are functional, but they can be easily modified to fully align with the standard RESTful pattern (i.e., using the Expected Paths).
