# Database Schema

## Overview

The database schema for the e-commerce platform is designed to manage products, users, orders, payments, and other related data. The schema is organized into various tables representing core entities and their relationships.

## Entities

### 1. **User**
- **Fields**:
  - `Id`: Unique identifier for the user (Primary Key).
  - `Email`: User's email address (Unique).
  - `PasswordHash`: Hashed password for security.
  - `Role`: User's role (e.g., Customer, Admin).
  - `CreatedDate`: Date the user account was created.
  
- **Relationships**:
  - One-to-Many with **Order** (A user can place many orders).

### 2. **Product**
- **Fields**:
  - `Id`: Unique identifier for the product (Primary Key).
  - `Name`: The product's name.
  - `Description`: Detailed description of the product.
  - `Price`: The product's price.
  - `CategoryId`: Foreign Key to **Category**.
  
- **Relationships**:
  - Many-to-One with **Category** (A product belongs to one category).
  - One-to-Many with **ProductReview** (A product can have many reviews).

### 3. **Order**
- **Fields**:
  - `Id`: Unique identifier for the order (Primary Key).
  - `UserId`: Foreign Key to **User**.
  - `TotalAmount`: Total cost of the order.
  - `OrderDate`: Date the order was placed.
  
- **Relationships**:
  - One-to-Many with **OrderItem** (An order contains multiple items).
  - Many-to-One with **User** (An order is placed by one user).

### 4. **Category**
- **Fields**:
  - `Id`: Unique identifier for the category (Primary Key).
  - `Name`: The name of the category.
  - `Description`: Description of the category.
  
- **Relationships**:
  - One-to-Many with **Product** (A category can have many products).

### 5. **OrderItem**
- **Fields**:
  - `Id`: Unique identifier for the order item (Primary Key).
  - `OrderId`: Foreign Key to **Order**.
  - `ProductId`: Foreign Key to **Product**.
  - `Quantity`: Quantity of the product ordered.
  - `Price`: Price of the product at the time of the order.
  
- **Relationships**:
  - Many-to-One with **Order** (An order item belongs to one order).
  - Many-to-One with **Product** (An order item refers to one product).

### 6. **ProductReview**
- **Fields**:
  - `Id`: Unique identifier for the review (Primary Key).
  - `ProductId`: Foreign Key to **Product**.
  - `UserId`: Foreign Key to **User**.
  - `Rating`: Rating given by the user.
  - `Comment`: User's review comment.
  - `ReviewDate`: Date the review was submitted.
  
- **Relationships**:
  - Many-to-One with **Product** (A review is associated with one product).
  - Many-to-One with **User** (A review is written by one user).

## ER Diagram

Below is the Entity-Relationship (ER) diagram of the database schema. The diagram visually represents the tables, fields, and relationships.

![Database Diagram](database-diagram.png)

## Conclusion

The database schema is designed to support the core functionality of the e-commerce platform. The relationships between entities ensure data integrity and consistency. The schema can be extended as needed to accommodate additional features or changes in requirements.
