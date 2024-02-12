# Entity Framework Core Documentation

## Using Visual C#, WEB API and MSSQL

Published on December 9, 2023 by Dario Krieger

Entity Framework (EF) is a powerful ORM (Object-Relational Mapping) framework used with Visual C#. It streamlines database interactions by allowing developers to work with database entities as .NET objects, reducing the need for direct SQL queries. EF simplifies database operations like querying, inserting, updating, and deleting data, abstracting away much of the underlying complexity. With its code-first approach, developers can define the database schema using C# classes, letting EF generate the necessary database structure automatically, fostering a more streamlined development process.

## Code First Approach

1. Create .NET Project
2. Install NuGet Packages: EntityFrameworkCore.Design, EntityFrameworkCore.Tools, EntityFrameworkCore.SqlServer
3. Clean Solution
4. Define your objects as classes:

    ```csharp
    public class Customer
    {
        public Guid Uid { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public List<Orders> Orders { get; set; }
    }
    ```

5. Create context class which inherits DbContext and save it to the "Models" folder with the objects from above:

    ```csharp
    public partial class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
    ```

6. Create a context extension class:

    ```csharp
    public partial class ShopContext : IShopContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
    ```

7. Define your connection string in appsettings.json:

    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "ConnectionString": "[YOUR_CONNECTION_STRING]"
      }
    }
    ```

8. Add these two lines of code in Program.cs main(string[] args) method:

    ```csharp
    var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    builder.Services.AddDbContext<TicketShopContext>(_ => _.UseSqlServer(connectionString));
    ```

## Database First Approach

1. Create database for example with SQL Server Management Studio
2. Create .NET Project
3. Install NuGet Packages: EntityFrameworkCore.Design, EntityFrameworkCore.Tools, EntityFrameworkCore.SqlServer
4. Clean Solution
5. Open Developer Powershell and execute the following command:

    ```bash
    dotnet ef dbcontext scaffold "Server=[Server];Database=[DBName];Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -p [Project(.csproj file)] -f
    ```

© 2024 Dario Krieger
