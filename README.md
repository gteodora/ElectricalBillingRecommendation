### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- PostgreSQL
- Visual Studio / Rider / VS Code

### Clone the Repository

```bash
git clone https://github.com/gteodora/electrical-billing-recommendation.git
cd electrical-billing-recommendation
```

### Modify appsettings.json with your PostgreSQL connection string.

### Clone the Repository
```bash
dotnet ef database update
```

### Run the Application
```bash
dotnet run
```
Navigate to:
```bash
https://localhost:44364/index.html
```

### Authentication & Roles
Default admin user is seeded:

Email: admin@admin.com
Password: Admin!123

Use the "Authorize" button in Swagger and provide a valid JWT Bearer token to access protected endpoints.


Default consumer user is seeded:

Email: user@user.com
Password: User!123

Use the "Authorize" button in Swagger and provide a valid JWT Bearer token to access protected endpoints.
