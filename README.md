# 🚀 .NET Clean Architecture Setup

Bu repository, **.NET Web API projelerinde başlangıç noktası olarak kullanılabilecek**, sürdürülebilir, ölçeklenebilir ve güvenli bir **Clean Architecture starter template** sunmaktadır.

Proje; yaygın olarak kullanılan mimari yapıların, tasarım desenlerinin, authentication mekanizmasının ve temel kütüphanelerin hazır bir altyapı halinde sunulmasını amaçlamaktadır.

Yeni projelerde tekrar tekrar kurulması gereken temel yapıların hazır olarak kullanılabilmesi ve geliştirme sürecinin hızlandırılması hedeflenmiştir.

---

## 🏗️ Architecture

Projede **Clean Architecture** yaklaşımı kullanılmaktadır.

Temel prensipler:

* Separation of Concerns
* Maintainability
* Testability
* Scalability
* Loose Coupling
* Dependency Inversion

Uygulama; business logic, application logic, infrastructure ve presentation sorumluluklarını birbirinden ayıracak şekilde tasarlanmıştır.

---

## 🧩 Design Patterns

Projede aşağıdaki tasarım desenleri ve mimari yaklaşımlar kullanılmaktadır:

* **Result Pattern** – İşlem sonuçlarını standart ve kontrollü şekilde yönetmek için
* **Repository Pattern** – Veri erişim işlemlerini soyutlamak için
* **CQRS Pattern** – Command ve Query işlemlerini birbirinden ayırmak için
* **Unit of Work Pattern** – Birden fazla veri işlemini tek bir transaction kapsamında yönetmek için
* **Dependency Injection** – Servisler arasındaki bağımlılıkları yönetmek için

---

## 🔐 Authentication & Authorization

Projede temel kullanıcı yönetimi ve authentication altyapısı bulunmaktadır.

### ASP.NET Core Identity

Kullanıcı yönetimi için **ASP.NET Core Identity** kullanılmaktadır.

Identity altyapısı üzerinden:

* User oluşturma
* User login
* Password management
* User ID yönetimi
* Role management
* Authentication
* Authorization

işlemleri gerçekleştirilmektedir.

Kullanıcı entity'si Identity altyapısı ile genişletilerek uygulamaya özel alanların eklenmesine uygun hale getirilmiştir.

---

### 🔑 JWT Authentication

API authentication işlemleri **JWT (JSON Web Token)** tabanlı olarak gerçekleştirilmektedir.

Login işlemi sonrasında başarılı authentication sonucunda kullanıcı için JWT token oluşturulur.

Token içerisinde kullanıcıyı tanımlamak ve authorization işlemlerinde kullanmak amacıyla gerekli claim bilgileri bulunmaktadır.

Client tarafından gönderilen JWT token, API tarafından doğrulanarak korumalı endpoint'lere erişim sağlanmaktadır.

Genel authentication flow:

```text
Client
   ↓
Login
   ↓
User Validation
   ↓
ASP.NET Core Identity
   ↓
JWT Token Generation
   ↓
Client receives Token
   ↓
Authorization Header
   ↓
JWT Validation
   ↓
Protected API Endpoint
```

---

## 👤 User Management

Projede kullanıcıların authentication işlemlerinin yönetilebilmesi için user tabanlı bir yapı bulunmaktadır.

Temel işlemler:

* User Registration
* User Login
* Password Validation
* JWT Token Generation
* User Authentication
* Role-based Authorization

Kullanıcı bilgileri **Entity Framework Core + ASP.NET Core Identity** üzerinden veritabanında tutulmaktadır.

---

## 🛡️ Role-Based Authorization

Authentication yanında authorization mekanizması da desteklenmektedir.

Kullanıcıların rollerine göre belirli endpoint'lere erişimleri sınırlandırılabilir.

Örneğin:

```text
User
 ├── Customer
 ├── Employee
 └── Admin
```

Endpoint'ler gerekli role göre korunabilir.

```text
Authenticated User
        ↓
JWT Validation
        ↓
User Claims / Roles
        ↓
Authorization
        ↓
Protected Endpoint
```

Bu yapı sayesinde authentication ve authorization birbirinden ayrılarak daha kontrollü bir erişim mekanizması oluşturulmuştur.

---

## 🗂️ Architecture Overview

Proje, sorumlulukların birbirinden ayrıldığı katmanlı bir yapı üzerine kurulmuştur.

```text
                    Presentation
                         │
                         ▼
                    Application
                         │
                         ▼
                       Domain
                         ▲
                         │
                  Infrastructure
```

### Domain

Uygulamanın temel business modellerini, entity'lerini ve domain kurallarını içerir.

Domain katmanı mümkün olduğunca dış bağımlılıklardan bağımsız tutulmuştur.

---

### Application

Uygulamanın business işlemlerinin yönetildiği katmandır.

İçerisinde:

* Commands
* Queries
* Handlers
* DTOs
* Validators
* Application Services
* Authentication related operations

gibi uygulama seviyesindeki işlemler bulunmaktadır.

---

### Infrastructure

Dış sistemlerle, veritabanıyla ve framework bağımlılıklarıyla ilgili implementasyonları içerir.

Örneğin:

* Entity Framework Core
* ASP.NET Core Identity
* Repository
* Unit of Work
* Database Configuration
* JWT Provider
* External Services
* Persistence

gibi altyapı implementasyonları burada bulunmaktadır.

---

### Presentation

API'nin dış dünyaya açılan katmanıdır.

İçerisinde:

* API Endpoints
* HTTP Requests / Responses
* Authentication Configuration
* Authorization
* Swagger
* API Configuration

gibi işlemler bulunmaktadır.

---

## 🔄 Request Flow

Tipik bir authenticated API request'i aşağıdaki akışı takip eder:

```text
Client
   ↓
HTTP Request
   ↓
Presentation
   ↓
Authentication Middleware
   ↓
JWT Validation
   ↓
Authorization
   ↓
MediatR
   ↓
Command / Query Handler
   ↓
Application
   ↓
Repository / UnitOfWork
   ↓
Entity Framework Core
   ↓
Database
```

Login işlemi ise:

```text
Client
   ↓
Login Request
   ↓
Authentication Handler
   ↓
ASP.NET Core Identity
   ↓
User Validation
   ↓
JWT Provider
   ↓
JWT Token
   ↓
Client
```

---

## 🛠️ Technologies & Libraries

Projede kullanılan temel teknolojiler ve kütüphaneler:

| Technology / Library                         | Purpose                                     |
| -------------------------------------------- | ------------------------------------------- |
| **.NET 9**                                   | Application framework                       |
| **ASP.NET Core Web API**                     | API development                             |
| **ASP.NET Core Identity**                    | User and authentication management          |
| **JWT**                                      | Token-based authentication                  |
| **Entity Framework Core**                    | ORM and database operations                 |
| **SQL Server**                               | Relational database                         |
| **MediatR**                                  | CQRS ve request/handler yönetimi            |
| **TS.Result**                                | Standard Result response structure          |
| **Mapster**                                  | Object mapping                              |
| **FluentValidation**                         | Request ve DTO validation                   |
| **TS.EntityFrameworkCore.GenericRepository** | Generic repository infrastructure           |
| **OData**                                    | Dynamic filtering, sorting and querying     |
| **Scrutor**                                  | Automatic dependency injection registration |

---

## 🔑 Authentication Features

Authentication altyapısında aşağıdaki özellikler bulunmaktadır:

* ✅ User Registration
* ✅ User Login
* ✅ ASP.NET Core Identity
* ✅ JWT Authentication
* ✅ JWT Claims
* ✅ Role-Based Authorization
* ✅ Password Hashing
* ✅ Protected API Endpoints
* ✅ User ID based authentication
* ✅ Authentication Middleware
* ✅ Authorization Middleware

---

## 📦 Key Features

* ✅ Clean Architecture
* ✅ CQRS
* ✅ Repository Pattern
* ✅ Unit of Work Pattern
* ✅ Result Pattern
* ✅ Dependency Injection
* ✅ MediatR
* ✅ FluentValidation
* ✅ Mapster
* ✅ Entity Framework Core
* ✅ Generic Repository
* ✅ OData
* ✅ Scrutor
* ✅ ASP.NET Core Identity
* ✅ JWT Authentication
* ✅ User Management
* ✅ Role-Based Authorization
* ✅ Separation of Concerns

---

## 📁 Project Structure

Genel proje yapısı:

```text
src
│
├── Domain
│   ├── Entities
│   ├── Users
│   └── ...
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── Handlers
│   ├── DTOs
│   ├── Validators
│   └── Services
│
├── Infrastructure
│   ├── Context
│   ├── Repositories
│   ├── Identity
│   ├── Authentication
│   └── Services
│
└── Presentation
    ├── Endpoints
    ├── Middleware
    └── Configuration
```

---

## 🚀 Getting Started

Repository'yi klonlayın:

```bash
git clone <repository-url>
```

Proje dizinine geçin:

```bash
cd <project-directory>
```

NuGet paketlerini restore edin:

```bash
dotnet restore
```

Projeyi build edin:

```bash
dotnet build
```

Database migration'larını uygulayın:

```bash
dotnet ef database update
```

Projeyi çalıştırın:

```bash
dotnet run
```

---

## 🔐 Authentication Usage

Uygulama çalıştırıldıktan sonra kullanıcı kayıt ve login endpoint'leri üzerinden authentication işlemleri gerçekleştirilebilir.

Genel kullanım:

```text
Register
   ↓
Login
   ↓
JWT Token
   ↓
Authorization Header
   ↓
Protected Endpoint
```

Korumalı endpoint'lere erişirken JWT token aşağıdaki şekilde gönderilir:

```http
Authorization: Bearer <JWT_TOKEN>
```

---

## 🎯 Purpose

Bu repository'nin amacı, yeni bir **.NET Web API** projesine başlanırken tekrar tekrar oluşturulması gereken temel mimari ve teknik yapıların hazır bir template halinde sunulmasıdır.

Template içerisinde yalnızca Clean Architecture yapısı değil, aynı zamanda:

* Authentication
* Authorization
* User Management
* JWT
* Identity
* CQRS
* Repository
* Unit of Work
* Validation
* Mapping
* Database Access
* Dependency Injection

gibi gerçek projelerde sık kullanılan altyapılar da hazır olarak bulunmaktadır.

Bu sayede yeni projeler doğrudan bu altyapı üzerinden geliştirilebilir ve başlangıç aşamasındaki tekrar eden kurulum işlemleri azaltılabilir.

---

## 👨‍💻 Author

Developed as a reusable **.NET Clean Architecture Web API starter template** with authentication and authorization infrastructure.