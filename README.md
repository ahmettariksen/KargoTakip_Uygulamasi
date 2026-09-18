# 🚚 Kargo Takip Sistemi

Bu proje, **.NET 9 Web API** kullanılarak geliştirilmiş, **Clean Architecture** prensiplerini temel alan bir **Kargo Takip Sistemi** API'sidir.

Projenin amacı; kargo kayıtlarının oluşturulması, takip edilmesi, güncellenmesi, silinmesi ve kargo durumlarının yönetilmesi ile kullanıcıların güvenli bir şekilde sisteme giriş yaparak yetkileri doğrultusunda API kaynaklarına erişebilmesini sağlayan sürdürülebilir ve ölçeklenebilir bir backend altyapısı oluşturmaktır.

Projede **Clean Architecture, CQRS, Repository, Unit of Work, ASP.NET Core Identity ve JWT Authentication** gibi gerçek dünya projelerinde kullanılan yapılar bir arada uygulanmıştır.

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

Uygulama; **Domain, Application, Infrastructure ve Presentation** katmanlarına ayrılmıştır.

Bu sayede business logic ile framework, database ve API gibi dış bağımlılıkların birbirinden ayrılması hedeflenmiştir.

---

## 📦 Domain

Projenin temel domain'i **Kargo Takip Sistemi** üzerine kuruludur.

Sistemde kargo operasyonlarının yönetilmesine yönelik temel entity'ler bulunmaktadır.

Örneğin:

* Kargo
* KargoInformation
* KargoTipi
* User

Kargo yapısı; gönderici, alıcı, teslimat adresi, kargo bilgileri ve kargo durumu gibi domain'e ait verilerin yönetilmesini sağlar.

---

## 👤 User Management

Projede kullanıcı yönetimi için **ASP.NET Core Identity** kullanılmaktadır.

Kullanıcı işlemleri:

* User Registration
* User Login
* Password Management
* User ID Management
* Role Management
* Authentication
* Authorization

Identity altyapısı sayesinde kullanıcı bilgileri güvenli bir şekilde yönetilmektedir.

Kullanıcı entity'si, uygulamanın ihtiyaçlarına göre **ASP.NET Core IdentityUser** üzerinden genişletilmiştir.

---

## 🔐 Authentication & Authorization

API authentication mekanizması **JWT (JSON Web Token)** kullanılarak oluşturulmuştur.

Kullanıcı başarılı bir şekilde login olduğunda JWT token oluşturulur.

Client, korumalı endpoint'lere erişirken token'ı `Authorization` header içerisinde gönderir.

```text
Client
   ↓
Login
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
   ↓
Bearer Token
   ↓
JWT Validation
   ↓
Protected API
```

JWT içerisinde kullanıcıyı tanımlamak için gerekli claim bilgileri bulunmaktadır.

---

## 🛡️ Authorization

Authentication sonrasında kullanıcıların API kaynaklarına erişimi authorization mekanizması ile kontrol edilmektedir.

Kullanıcı rollerine göre belirli endpoint'ler sınırlandırılabilir.

Örneğin:

```text
User
 ├── Customer
 ├── Employee
 └── Admin
```

Bu yapı sayesinde kullanıcıların yalnızca yetkili oldukları kaynaklara erişmesi sağlanabilir.

---

# 🧩 Design Patterns

Projede aşağıdaki tasarım desenleri ve mimari yaklaşımlar kullanılmaktadır:

### Result Pattern

API işlemlerinden dönen sonuçların standart bir yapı içerisinde yönetilmesini sağlar.

### Repository Pattern

Veri erişim işlemlerini business logic'ten ayırmak ve database işlemlerini soyutlamak için kullanılmaktadır.

### CQRS Pattern

Command ve Query işlemlerinin birbirinden ayrılmasını sağlar.

Örneğin:

```text
Command
 ├── KargoCreateCommand
 ├── KargoUpdateCommand
 ├── KargoDeleteCommand
 └── KargoStatusUpdateCommand

Query
 ├── KargoGetByIdQuery
 └── KargoGetAllQuery
```

Bu yapı sayesinde kargo üzerinde gerçekleştirilen veri değiştirme işlemleri ile veri okuma işlemleri birbirinden ayrılmıştır.

### Unit of Work Pattern

Birden fazla database işleminin tek bir çalışma birimi içerisinde yönetilmesini sağlar.

### Dependency Injection

Servisler arasındaki bağımlılıkların yönetilmesi için ASP.NET Core Dependency Injection altyapısı kullanılmaktadır.

---

## 🛠️ Technologies & Libraries

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
| **FluentValidation**                         | Request validation                          |
| **TS.EntityFrameworkCore.GenericRepository** | Generic repository infrastructure           |
| **OData**                                    | Dynamic filtering, sorting and querying     |
| **Scrutor**                                  | Automatic dependency injection registration |

---

# 🗂️ Architecture Overview

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

Kargo takip sisteminin temel business modellerini ve kurallarını içerir.

```text
Domain
├── Kargos
├── KargoInformations
├── KargoTipis
├── Users
└── ...
```

Domain katmanı database veya API gibi dış bağımlılıklardan bağımsız tutulmaya çalışılmıştır.

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

bulunmaktadır.

Kargo işlemleri MediatR üzerinden Command ve Query yapıları kullanılarak yönetilmektedir.

---

### Infrastructure

Database ve dış bağımlılıkların implementasyonlarını içerir.

Örneğin:

* Entity Framework Core
* ASP.NET Core Identity
* Repository
* Unit of Work
* JWT Provider
* Database Configuration
* External Services

---

### Presentation

API'nin dış dünyaya açılan katmanıdır.

İçerisinde:

* API Endpoints
* HTTP Requests / Responses
* Authentication
* Authorization
* Swagger
* API Configuration

gibi işlemler bulunmaktadır.

---

# 📦 Kargo Management

Sistemin temel business operasyonu **kargo yönetimidir**.

Kargo işlemleri CQRS yaklaşımı kullanılarak yönetilmektedir.

Kargo üzerinde gerçekleştirilebilen temel operasyonlar:

```text
Kargo
│
├── Create
├── Get All
├── Get By Id
├── Update
├── Delete
└── Update Status
```

Kargo bilgileri içerisinde kargonun türü, ağırlığı, gönderici, alıcı ve teslimat bilgileri gibi ilişkili domain verileri de yönetilebilmektedir.

---

## 🔄 Kargo Operations

### Create Kargo

Yeni bir kargo kaydı oluşturulmasını sağlar.

```text
KargoCreateCommand
        ↓
KargoCreateCommandHandler
        ↓
Validation
        ↓
Repository
        ↓
UnitOfWork
        ↓
Database
```

---

### Update Kargo

Mevcut kargo bilgilerinin güncellenmesini sağlar.

```text
KargoUpdateCommand
        ↓
KargoUpdateCommandHandler
        ↓
Validation
        ↓
Repository
        ↓
UnitOfWork
        ↓
Database
```

Kargo üzerinde güncellenebilen alanlar business gereksinimlerine göre yönetilmektedir.

---

### Delete Kargo

Mevcut bir kargo kaydının sistemden silinmesini sağlar.

```text
KargoDeleteCommand
        ↓
KargoDeleteCommandHandler
        ↓
Repository
        ↓
UnitOfWork
        ↓
Database
```

Delete işlemi CQRS içerisindeki ayrı bir **Command** olarak ele alınmaktadır.

---

### Update Kargo Status

Kargonun mevcut durumunun güncellenmesini sağlayan business operasyonudur.

Örneğin:

```text
Kargo Status

Created
   ↓
Accepted
   ↓
InTransit
   ↓
OutForDelivery
   ↓
Delivered
```

Kargo durumunun güncellenmesi, kargonun diğer bilgilerinin güncellenmesinden ayrı bir operation olarak ele alınmaktadır.

Örneğin:

```text
KargoStatusUpdateCommand
        ↓
KargoStatusUpdateCommandHandler
        ↓
Business Rules
        ↓
Repository
        ↓
UnitOfWork
        ↓
Database
```

Bu yapı sayesinde kargonun durum değişiklikleri ileride daha kapsamlı business kuralları ile genişletilebilir.

---

## 🔍 Kargo Query Operations

Kargo verilerinin okunması için Query yapıları kullanılmaktadır.

```text
Query
│
├── KargoGetAllQuery
└── KargoGetByIdQuery
```

### Get All

Kargo kayıtlarının listelenmesini sağlar.

OData kullanılarak dinamik sorgulama özellikleri desteklenmektedir.

Örneğin:

```text
GET /odata/kargo
```

OData sayesinde filtreleme, sıralama ve sorgulama işlemleri API seviyesinde gerçekleştirilebilir.

### Get By Id

Belirli bir kargonun detaylarını getirir.

```text
GET /kargo/{id}
```

Örnek request flow:

```text
GET /kargo/{id}
       ↓
KargoGetByIdQuery
       ↓
KargoGetByIdQueryHandler
       ↓
Repository
       ↓
Entity Framework Core
       ↓
SQL Server
       ↓
DTO
       ↓
Result
       ↓
HTTP Response
```

---

# 🔄 Request Flow

Tipik bir kargo API request'i aşağıdaki akışı takip eder:

```text
Client
   ↓
HTTP Request
   ↓
Presentation
   ↓
Authentication
   ↓
Authorization
   ↓
MediatR
   ↓
Command / Query
   ↓
Handler
   ↓
Application
   ↓
Repository / UnitOfWork
   ↓
Entity Framework Core
   ↓
SQL Server
```

Örneğin bir kargo güncelleme işlemi:

```text
PUT /kargo/{id}
       ↓
KargoUpdateCommand
       ↓
KargoUpdateCommandHandler
       ↓
Validation
       ↓
Repository
       ↓
UnitOfWork
       ↓
SQL Server
       ↓
Result
       ↓
HTTP Response
```

Kargo durum güncelleme işlemi:

```text
PATCH /kargo/{id}/status
       ↓
KargoStatusUpdateCommand
       ↓
KargoStatusUpdateCommandHandler
       ↓
Business Rules
       ↓
Repository
       ↓
UnitOfWork
       ↓
SQL Server
       ↓
Result
       ↓
HTTP Response
```

---

# 🔑 Authentication Flow

Login işlemi:

```text
POST /auth/login
       ↓
User Credentials
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

Korumalı endpoint'lere erişim:

```http
Authorization: Bearer <JWT_TOKEN>
```

Token doğrulandıktan sonra kullanıcının API kaynağına erişim yetkisi kontrol edilir.

---

# 📁 Project Structure

Genel proje yapısı:

```text
src
│
├── Domain
│   ├── Kargos
│   ├── KargoInformations
│   ├── KargoTipis
│   ├── Users
│   └── ...
│
├── Application
│   ├── Kargos
│   │   ├── Commands
│   │   │   ├── KargoCreate
│   │   │   ├── KargoUpdate
│   │   │   ├── KargoDelete
│   │   │   └── KargoStatusUpdate
│   │   │
│   │   ├── Queries
│   │   │   ├── KargoGetAll
│   │   │   └── KargoGetById
│   │   │
│   │   ├── DTOs
│   │   └── Validators
│   │
│   ├── Users
│   └── ...
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

# 🚀 Getting Started

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

# 📚 API Documentation

API endpoint'leri **Swagger / OpenAPI** üzerinden test edilebilir.

Uygulama çalıştırıldıktan sonra Swagger üzerinden:

* User Registration
* User Login
* JWT Authentication
* Kargo oluşturma
* Kargo listeleme
* Kargo detayını görüntüleme
* Kargo güncelleme
* Kargo silme
* Kargo durumunu güncelleme
* Kargo sorgulama

gibi API işlemleri gerçekleştirilebilir.

JWT authentication kullanıldığı için korumalı endpoint'leri test etmeden önce login işlemi gerçekleştirilerek alınan token'ın Swagger üzerinde authorize edilmesi gerekir.

---

# ✨ Key Features

* ✅ Clean Architecture
* ✅ CQRS
* ✅ MediatR
* ✅ Repository Pattern
* ✅ Generic Repository
* ✅ Unit of Work
* ✅ Result Pattern
* ✅ Dependency Injection
* ✅ ASP.NET Core Identity
* ✅ JWT Authentication
* ✅ Role-Based Authorization
* ✅ User Registration & Login
* ✅ Entity Framework Core
* ✅ SQL Server
* ✅ Mapster
* ✅ FluentValidation
* ✅ OData
* ✅ Scrutor
* ✅ Swagger / OpenAPI
* ✅ Kargo Management
* ✅ Kargo Create
* ✅ Kargo Update
* ✅ Kargo Delete
* ✅ Kargo Status Update
* ✅ Separation of Concerns

---

# 🎯 Project Purpose

Bu projenin amacı, gerçek bir **Kargo Takip Sistemi** senaryosu üzerinden modern **.NET Web API** geliştirme yaklaşımını ve Clean Architecture prensiplerini uygulamaktır.

Proje içerisinde yalnızca temel CRUD işlemleri değil; aynı zamanda gerçek projelerde kullanılan:

* Clean Architecture
* CQRS
* Repository
* Unit of Work
* Authentication
* Authorization
* Identity
* JWT
* Validation
* Mapping
* Dependency Injection
* ORM
* Database Management
* Business Rule Based Operations

gibi backend geliştirme konuları birlikte uygulanmaktadır.

Özellikle **Kargo Durum Güncelleme** işleminin ayrı bir Command ve Handler üzerinden ele alınması, kargo bilgilerinin basit CRUD işlemlerinden bağımsız olarak business operasyonlarının yönetilebilmesine olanak sağlamaktadır.

Bu yapı, ilerleyen aşamalarda kargo takip sistemine yeni business kurallarının ve özelliklerinin eklenebilmesine uygun şekilde tasarlanmıştır.

---

## 👨‍💻 Author

Developed as a **.NET 9 Clean Architecture Web API Kargo Takip Sistemi** project.

The project focuses on applying modern backend development practices, clean architecture principles, authentication and authorization mechanisms, CQRS and business-oriented API operations in a real-world domain scenario.