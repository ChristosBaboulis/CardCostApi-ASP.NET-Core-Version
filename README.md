# Card Cost API (.NET 8)

This is an ASP.NET Core application that manages a clearing cost matrix per country and provides an endpoint to calculate the cost of clearing a given card number using the external Binlist API.

## Features

* CRUD API for managing clearing costs per country (SQL Server backend)
* External API call to [https://lookup.binlist.net](https://lookup.binlist.net) for country lookup based on card number
* Business logic endpoint to return the clearing cost of any given card
* DTO mapping with AutoMapper
* JWT authentication with RSA keys (stateless auth, role-based)
* Clean separation of domain, repository, service, and controller layers
* Dockerized application and SQL Server for portability

---

## API Endpoints

### Clearing Cost Management (Admin CRUD)

* `GET /api/clearing-costs` – List all clearing costs
* `GET /api/clearing-costs/{id}` – Get clearing cost by ID
* `POST /api/clearing-costs` – Create a new clearing cost
* `PUT /api/clearing-costs/{id}` – Update a clearing cost
* `DELETE /api/clearing-costs/{id}` – Delete a clearing cost

### Business Logic Endpoint

* `POST /api/given-card-related-cost`

    * Request Body: `{ "cardNumber": "45717360" }`
    * Response: `{ "country": "US", "cost": 5 }`

---

## How It Works

1. The card number is posted to `/api/given-card-related-cost`
2. The app queries `https://lookup.binlist.net/{cardNumber}` to get the country code
3. It looks up the country in the internal clearing cost matrix (SQL Server DB)
4. If not found, it falls back to country code "OTHERS"
5. Returns the final country and cost as JSON

---

## Technologies Used

* .NET 8 / ASP.NET Core Web API
* Entity Framework Core with SQL Server
* AutoMapper for DTO mapping
* JWT authentication with RSA keys (`System.IdentityModel.Tokens.Jwt`)
* Swagger (Swashbuckle) for API documentation and testing
* Docker & Docker Compose for application + SQL Server portability
* RESTful Design principles

---

## SwaggerUI

This project uses **SwaggerUI** instead of Postman collections for API testing and exploration.

When you run the app (via Visual Studio or `dotnet run`), SwaggerUI is available at:

```
https://localhost:{port}/swagger
```

From there you can:

1. Authenticate using `/auth/login` (with `admin1` / `adminPass` as default credentials)
2. Copy the returned JWT token
3. Click the **Authorize** button in SwaggerUI and paste the token as:

   ```
   Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9...
   ```

4. Once authorized, you can call all protected endpoints directly from the SwaggerUI interface.

This eliminates the need for an external Postman collection.

---

## Docker Deployment

This application and its SQL Server database are fully containerized.

### Start SQL Server via Docker Compose

```bash
docker compose up -d
```

This will start a SQL Server instance with persistent storage.

### Run the Application

```bash
dotnet build
dotnet run --project CardCostApplication
```

Or, to build a Docker image of the application itself:

```bash
docker build -t card_cost_api_dotnet .
docker run -p 8080:8080 card_cost_api_dotnet
```

---

## Authentication Flow (JWT)

All endpoints (except `/auth/login`) are protected and require a valid JWT token.

### How to Authenticate:

1. Send a POST request to the login endpoint:

    ```
    POST /auth/login
    ```

   #### Body (JSON):
    ```json
    {
      "username": "admin1",
      "password": "adminPass"
    }
    ```

2. Copy the `token` from the response:
    ```json
    {
      "token": "eyJhbGciOiJSUzI1NiJ9..."
    }
    ```

3. Use the token as a Bearer token in the `Authorization` header for all subsequent requests:
    ```
    Authorization: Bearer eyJhbGciOiJSUzI1NiJ9...
    ```

---

## Extendability

The application follows modular architecture with the following layers:

- `Domain`: Entities (mapped with EF Core)
- `Infrastructure`: Repositories and Persistence (DbContext, configs)
- `Application`: Business logic services
- `Contracts`: DTOs and API representations
- `Controllers`: REST endpoints

The `ClearingCostService` and external lookup logic can be easily extended or swapped via interfaces. Future support for additional card info providers (e.g., RapidAPI) can be added via strategy pattern.

---

## High Availability (HA) Ready

This application is stateless and can be deployed in multiple replicas (e.g., containers) behind a load balancer. No session or in-memory state is required between requests.

---

## License

This project is licensed under the MIT License.

---

Author
----------
Christos Bampoulis  
GitHub: [@ChristosBaboulis](https://github.com/ChristosBaboulis)
