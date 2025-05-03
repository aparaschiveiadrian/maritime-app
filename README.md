# Maritime Management App

- Supports managing ships, ports, voyages, and country-port relationships.
- The application uses **PostgreSQL 15**, running in a **Docker container** defined in `docker-compose.yml` for deployment. It maps the container’s default port **5432** to **2000** on the host machine for local access.
---

## Features Implemented

### Backend (ASP.NET Core 8.0.408, C#)
- RESTful API endpoints for:
  - `Country`, `Port`, `Ship`, `Voyage`
  - Full CRUD support
- Validation at ENtity and COntroller layer
- Following an onion architecture:
  - Controllers → Services → ApplicationDbCOntext
- DTOs and Mapper(for requests and responses) for all input/output models
- Added Swagger-UI for API documentation

---

## Unit Testing

- Unit tests implemented using:
  - `xUnit`
  - `Moq`
- 100% coverage for:
  - `CountryService`, `PortService`, `ShipService`, `VoyageService`
  - `CountryController`, `PortController`, `ShipController`, `VoyageController`
---


## Database Diagram (Lucidcharts)
![Diagram Preview](https://github.com/user-attachments/assets/509219bc-492b-4d20-9a2c-6e3dd955cc51)

---

