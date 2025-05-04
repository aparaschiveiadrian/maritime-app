
# Maritime Management App

A full-stack maritime logistics management system with REST APIs and a user-friendly Angular interface.

---
##  UI Preview

| Home | Countries | Ships |
|------|-----------|--------|
| ![home](https://github.com/user-attachments/assets/a9350641-5081-48a7-8373-f25032d5d316) | ![countries](https://github.com/user-attachments/assets/b39c4cf7-cf8b-46d1-a5c8-a183ad47dbcd) | ![ships](https://github.com/user-attachments/assets/4f6822d9-5ab4-4721-809b-fbe148de991f) |

| Ports | Voyages |
|-------|---------|
| ![ports](https://github.com/user-attachments/assets/ce53c635-f7cd-42fa-b835-fc5fbaa540a0) | ![voyages](https://github.com/user-attachments/assets/35b1aa08-f5c0-42a7-bec9-b56d0c3987b8) |


## Technologies Used

- **Backend**: ASP.NET Core 8.0  
- **Frontend**: Angular 19.2.10  
- **Database**: PostgreSQL 15 (via Docker)  
- **CI**: GitHub Actions (Tests + Build pipeline)

---

## Docker Setup

The PostgreSQL instance is containerized using **Docker** and defined in `docker-compose.yml`.

- **Host Port**: `2000`  
- **Container Port**: `5432`

---

## CI Workflow

- GitHub Actions run on **every push and pull request**
- **Unit tests must pass** to allow merges into the `main` branch

---

## Backend Features

- Full **CRUD API** for:
  - `Countries`, `Ports`, `Ships`, and `Voyages`
- Layered architecture:
  - **Controller → Service → EF DbContext**
- **DTOs** and **Mappers** for clean request/response handling
- **Validation** at both entity and controller levels
- Integrated **Swagger UI** for API testing

---

## Frontend Features

- Responsive **Angular UI**
- Reusable, modular components
- Separate pages for:
  - `Home`, `Country`, `Ship`, `Port`, `Voyage`
- All features support:
  - **Create**, **Read**, **Update**, and **Delete**

---

## Unit Testing

Tested with:
- **xUnit** for test framework  
- **Moq** for mocking services and dependencies  

Coverage includes:
- Services: `Country`, `Port`, `Ship`, `Voyage`  
- Controllers: All API endpoints tested

---

## Database Diagram

Created with Lucidchart:

![Database Diagram](https://github.com/user-attachments/assets/509219bc-492b-4d20-9a2c-6e3dd955cc51)

---
