🏠 HouseBrokerApp
🚀 House Broker Application (MVP) — built with ASP.NET Core, .NET 7, Entity Framework Core, JWT Authentication, Swagger, and Clean Architecture.

✅ Brokers can manage listings (create, update, delete)
✅ House Seekers can search and view listings
✅ Role-based authorization with ASP.NET Identity & JWT
✅ Fully documented API via Swagger

📌 Table of Contents
✨ Features

⚙️ Tech Stack

📂 Project Structure

🚀 Getting Started

🔐 Authentication Flow

🏠 API Endpoints

🧪 Unit Testing

🔗 Postman Testing

📜 File Documentation

✨ Features
✅ JWT Authentication & Role-based access (Broker vs HouseSeeker)
✅ CRUD for property listings (restricted to Brokers)
✅ Public property search & details
✅ Swagger UI with Authorize 🔒 for JWT
✅ Unit tested with xUnit, Moq, FluentAssertions
✅ Clean Architecture: separation of concerns

⚙️ Tech Stack
ASP.NET Core 7

Entity Framework Core (MSSQL)

ASP.NET Identity

JWT Bearer Authentication

Swagger / Swashbuckle

xUnit, Moq, FluentAssertions (Unit Testing)

📂 Project Structure

HouseBrokerApp.sln
 ├── HouseBrokerApp.Api
 │    ├── Program.cs
 │    ├── appsettings.json
 │    ├── Properties/launchSettings.json
 │    ├── Controllers/
 │    │    ├── AuthController.cs
 │    │    ├── PropertyController.cs
 ├── HouseBrokerApp.Infrastructure
 │    ├── Data/DbSeeder.cs
 ├── HouseBrokerApp.Application
 │    ├── Interfaces/IPropertyRepository.cs
 ├── HouseBrokerApp.Domain
 │    ├── Entities/Property.cs
 ├── HouseBrokerApp.Tests
 │    ├── PropertyControllerTests.cs

🚀 Getting Started
1️⃣ Clone & Restore
bash
Copy
Edit
git clone https://github.com/roaksey/HouseBrokerApp.git
cd HouseBrokerApp
dotnet restore


2️⃣ Database Setup
bash
dotnet ef database update

3️⃣ Trust Local HTTPS
bash
dotnet dev-certs https --trust

4️⃣ Run the API
bash
dotnet run --project HouseBrokerApp.Api

Visit 👉 https://localhost:5001/swagger

🔐 Authentication Flow
Register: /api/auth/register

Login: /api/auth/login → returns JWT token with roles

Use Swagger Authorize 🔒 → paste Bearer {your_token}

Call protected endpoints as Broker

Default seeded users:

Role	Username	Password
Broker	broker1	Password123!
HouseSeeker	seeker1	Password123!

🏠 API Endpoints
Method	Route	Role Required
GET	/api/property	Public
GET	/api/property/{id}	Public
GET	/api/property/search	Public
POST	/api/property	Broker only
PUT	/api/property/{id}	Broker only
DELETE	/api/property/{id}	Broker only

🧪 Unit Testing
✅ HouseBrokerApp.Tests
✅ Tests for PropertyController:

GetAll

Get

Search

Create

Run tests:

bash
Copy
Edit
dotnet test
Uses:

xUnit

Moq

FluentAssertions

🔗 Postman Testing
1️⃣ Login:

http
Copy
Edit
POST /api/auth/login
{
  "username": "broker1",
  "password": "Password123!"
}
2️⃣ Copy the JWT token.

3️⃣ Use Authorization: Bearer {your_token} in headers for protected calls:

bash
Copy
Edit
POST /api/property
PUT /api/property/{id}
DELETE /api/property/{id}


📜 File Documentation
⚙️ Program.cs
Adds EF Core, Identity, JWT auth, Swagger with Bearer

Uses UseAuthentication() + UseAuthorization()

Seeds roles/users on startup with DbSeeder

🔐 AuthController
/register: Register user with role (Broker or HouseSeeker)

/login: Issues JWT with role claims

🏠 PropertyController
CRUD for Property

[Authorize(Roles = "Broker")] protects Create, Update, Delete

Anyone can view/search

🔑 DbSeeder
Creates Broker & HouseSeeker roles

Seeds default users for testing

📜 appsettings.json
Connection string

Strong Jwt:Key for HS256

Issuer/Audience for validation

🌍 launchSettings.json
Runs HTTPS (https://localhost:5001)

Opens Swagger (launchUrl: "swagger")

