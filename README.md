# 🧑‍💻 People REST API with Desktop Front-End

A simple full-stack project built with ASP.NET Core and C#.  
Implements a RESTful API with full CRUD operations and a desktop front-end that interacts with the API.

---

## 📦 Project Overview

### 🔙 Back-End (ASP.NET Core Web API)

- ✅ Full CRUD (`GET`, `POST`, `PUT`, `DELETE`) for People
- 🎯 3-Tier Architecture (Controller - Service - Data Layer)
- 🧾 DTOs (for cleaner and safer data exchange)
- 🛡 Validations using Data Annotations
- ❗ Proper error handling with `ActionResult` and meaningful messages
- 📌 Static Routing with `[Route("api/[controller]")]`
- 🔍 Swagger UI for interactive API documentation

### 🖥️ Front-End (C# Desktop App)

- Desktop application using Windows Forms or WPF (depending on your setup)
- Fetches and displays all People from the API
- Allows user to:
  - Add new person
  - Edit existing person
  - Delete person
  - View details
- Error feedback and data validation integrated with the UI

---

## 🧰 Technologies Used

- ASP.NET Core
- Entity Framework Core or ADO.NET
- Windows Forms or WPF (C# Desktop UI)
- Swagger (Swashbuckle)
- Visual Studio 2022+

---

## 🗂️ Project Structure
