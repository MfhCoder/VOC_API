Initialize VOC Web API
Set up a new .NET 8 Web API solution "VOC" with API, Core, and Infrastructure projects using a layered architecture. Implement domain entities, DTOs, and utility classes. Add authentication, JWT, and PostgreSQL support. Introduce Specification and Unit of Work patterns for flexible querying and transaction management. Seed initial data and roles. Configure global exception handling, CORS, Swagger, and database seeding. Add EF Core migrations for initial schema, including Identity and domain tables.

Add MerchantsController with endpoints for retrieving merchants, industries, and locations

- Implemented GetMerchants to fetch merchants with optional filtering
- Added GetIndustries to retrieve a list of industries
- Added GetLocations to retrieve a list of locations
- Integrated authorization for all endpoints
