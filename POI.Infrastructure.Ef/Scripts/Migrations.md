# Database Migrations

## Applying Migrations

### PoiDbContext Migrations
1. **Creating Migrations**

   ```bash
   dotnet ef migrations add InitialCreate --project .\POI.Infrastructure.Ef --startup-project .\POI.Api --context PoiDbContext -v
2. **Updating the Database**
    
   ```bash
    dotnet ef database update --project .\POI.Infrastructure.Ef --startup-project .\POI.Api --context PoiDbContext -v


### AgentDbContext Migrations
1. **Creating Migrations**

   ```bash
   dotnet ef migrations add InitialCreate --project .\POI.Infrastructure.Ef --startup-project .\POI.Api --context AgentDbContext -v
2. **Updating the Database**

   ```bash
    dotnet ef database update --project .\POI.Infrastructure.Ef --startup-project .\POI.Api --context AgentDbContext -v

