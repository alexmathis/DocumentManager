# Simplified Multi-Tenant Collaboration Platform

This project is a simplified version of a multi-tenant SaaS collaboration platform that allows users from different organizations to store and retrieve documents. The platform supports core functionalities such as **organization management**, **user management**, **document storage**, and **multi-tenancy**.

## Table of Contents

1. [Overview](#overview)
2. [Project Architecture](#project-architecture)
   - [Core Features](#core-features)
   - [Use of Entity Framework for Command Handling](#use-of-entity-framework-for-command-handling)
   - [Key Components](#key-components)
3. [API Endpoints](#api-endpoints)
4. [Multi-Tenancy Implementation](#multi-tenancy-implementation)
   - [Database Schema Design](#database-schema-design)
   - [Data Isolation](#data-isolation)
5. [Deployment Instructions](#deployment-instructions)
   - [Local Development (Using Docker and Azurite)](#local-development-using-docker-and-azurite)
   - [Cloud Deployment (Azure)](#cloud-deployment-azure)
6. [Design Decisions](#design-decisions)
7. [Trade-offs and Future Enhancements](#trade-offs-and-future-enhancements)
8. [Conclusion](#conclusion)

## Overview

The **Simplified Multi-Tenant Collaboration Platform** is designed to allow multiple organizations (tenants) to manage their users and documents while ensuring data isolation between organizations. This platform provides basic features like CRUD operations for organizations, user management, document storage, and audit logging. It is built with **C#**, **ASP.NET Core**, and **Entity Framework Core** for backend development, with **SQL Server** for relational data storage and **Azure Blob Storage** (or Azurite for local development) for file storage.

## Project Architecture

### Core Features

1. **Organization Management**:

   - Each organization has an ID and a name.
   - Supports CRUD operations for creating, reading, updating, and deleting organizations.
   - Organizations can have multiple users, but users belong to only one organization.

2. **User Management**:

   - Each user has an ID and an email address.
   - Users are associated with one organization at a time, and they can perform actions within the scope of their organization.

3. **Document Storage**:

   - Each document has an ID, name, size, and storage path.
   - Users can create, read, update, and delete documents owned by their organization.
   - Document metadata is stored in a SQL database, and files are stored in Azure Blob Storage (or simulated via Azurite for local development).

4. **Multi-Tenancy**:
   - Multi-tenancy is implemented by associating each user and document with an organization.
   - Data isolation is ensured by scoping queries and actions based on the user's organization.
5. **Audit Logging**:
   - Logs who created, updated, or deleted documents and when those actions occurred.

### Use of Entity Framework for Command Handling

For all incoming commands (such as creating, updating, or deleting organizations, users, and documents), **Entity Framework Core** is used to manage database interactions. Entity Framework simplifies database operations by abstracting away raw SQL queries and allowing us to work directly with C# objects. It ensures that all changes to the database, such as the addition or modification of entities, are tracked and persisted in a transaction-safe manner.

Entity Framework is responsible for:

- Handling changes to entities such as organizations, users, and documents.
- Tracking and applying these changes to the underlying SQL Server database.
- Managing relationships between entities (e.g., associating users with organizations and documents with users).

This ensures that all operations follow **ACID** principles (Atomicity, Consistency, Isolation, Durability) and that any changes made via commands are persisted in a reliable and consistent manner.

The query side, which involves fetching data (e.g., getting documents by user or organization), primarily leverages **Dapper** for optimized, lightweight querying where necessary, while complex commands and operations are handled via Entity Framework.

### Key Components:

- **OrganizationController**: Handles CRUD operations for organizations.
- **UserController**: Manages user creation and association with organizations.
- **DocumentController**: Supports file uploads, document management, and audit logging.
- **IFileStorageService**: Abstraction for handling file storage, allowing flexibility for cloud or local file storage.

## API Endpoints

### Organization Management

- **POST /api/organizations**: Create a new organization.
- **GET /api/organizations**: Get all organizations.
- **PUT /api/organizations/{id}**: Update an organization's details.
- **DELETE /api/organizations/{id}**: Delete an organization.

### User Management

- **POST /api/users**: Create a new user and associate with an organization.
- **GET /api/users/{id}**: Get a user by ID.
- **PUT /api/users/{id}**: Update a user's email and organization.
- **DELETE /api/users/{id}**: Delete a user.

### Document Management

- **POST /api/documents**: Upload a document, store its metadata, and track its audit.
- **GET /api/documents/{id}**: Retrieve document metadata by ID.
- **GET /api/documents**: Get all documents for the user's organization.
- **PUT /api/documents/{id}**: Update document metadata and upload a new file if provided.
- **DELETE /api/documents/{id}**: Delete a document and log the action.

## Multi-Tenancy Implementation

Multi-tenancy is enforced by associating every user and document with an organization. Each request made by a user is scoped by their organization, ensuring that:

- Users can only see and manage documents belonging to their organization.
- CRUD operations are restricted to data associated with the user's organization.

### Database Schema Design:

- **Organizations** Table: Stores organization information (ID, Name).
- **Users** Table: Stores user information (ID, Email) and references the organization.
- **Documents** Table: Stores document metadata (ID, Name, Size, StoragePath) and references the organization and owner (user).

### Data Isolation:

All database queries are scoped by the organization ID associated with the user, ensuring that data is isolated between organizations.

## Deployment Instructions

### Local Development (Using Docker and Azurite)

1. **Requirements**:
   - Docker and Docker Compose installed.
2. **Steps**:

   - Clone the repository from GitHub.
   - Build and start the services using Docker Compose:

     ```bash
     docker-compose up --build
     ```

   - The API will be accessible at `http://localhost:5000`, and you can interact with SQL Server and Azurite through their respective ports.

3. **Azurite (Local Blob Storage)**:

   - Azurite simulates Azure Blob Storage locally. The file storage service uses the Azurite Blob URL (`http://azurite:10000/devstoreaccount1`) for file storage.
   - Uploaded documents are stored in the local Azurite storage, but the file structure mimics Azure Blob Storage.

4. **SQL Server**:
   - SQL Server is running locally on port `1433`, and the connection string is set to use SQL Server for relational data storage.

### Cloud Deployment (Azure)

1. **Azure Resources Needed**:

   - **Azure SQL Database** for relational data storage.
   - **Azure Blob Storage** for document storage.
   - **Azure App Service** for API deployment.

2. **Steps**:
   - Deploy the API to Azure App Service using GitHub Actions or Azure Pipelines.
   - Update the connection strings for Azure SQL Database and Blob Storage in the Azure App Service configuration.
   - Run database migrations to create the necessary tables for organizations, users, and documents.

## Design Decisions

1. **CQRS (Command Query Responsibility Segregation)**:

   - The system uses CQRS to separate read and write concerns. This makes the codebase modular and scalable, allowing us to independently handle queries and commands for each entity (organizations, users, documents).

2. **Azure Blob Storage for File Management**:

   - Azure Blob Storage is used for document storage due to its scalability, ease of access, and integration with cloud services. Locally, Azurite is used to simulate Blob Storage for development and testing.

3. **Dapper for Data Access**:

   - Dapper was chosen for its lightweight nature and performance for data access. Given the simple data structure, Dapper provides fast and efficient querying without the overhead of a full ORM.

4. **Multi-Tenancy via Organization ID**:

   - Multi-tenancy is achieved by associating all users and documents with an organization ID, ensuring that all operations are scoped to the tenant's organization.

5. **Audit Logging**:
   - A basic audit logging mechanism is included to track the creation, updating, and deletion of documents. Logs capture the user who performed the action and when it occurred.

## Trade-offs and Future Enhancements

1. **Authentication**:
   - Authentication is
