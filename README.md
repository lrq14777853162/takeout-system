# Takeout System

A .NET 8 based MVP scaffold for a takeout platform with merchant, rider, admin, and user roles.

## Tech Stack
- ASP.NET Core 8 Web API
- Entity Framework Core
- MySQL
- Redis (reserved for future use)
- Swagger / OpenAPI
- Docker Compose

## Solution Structure
- `src/Takeout.Api` - API entry point and controllers
- `src/Takeout.Application` - service contracts and DTOs
- `src/Takeout.Domain` - core entities and enums
- `src/Takeout.Infrastructure` - EF Core DbContext and implementations
- `sql/init.sql` - database bootstrap script

## MVP Modules
- Authentication placeholder with JWT-ready configuration
- Merchant management
- Product category and product management
- Order creation
- Order status flow
- Rider order taking

## Run
1. Install .NET 8 SDK and Docker.
2. Start infrastructure with `docker compose up -d`.
3. Open the solution and run `src/Takeout.Api`.
4. Open Swagger at `/swagger`.

## Next Steps
- Add JWT authentication
- Add EF Core migrations
- Add Redis caching and RabbitMQ integration
- Add payment integration
- Add unit and integration tests