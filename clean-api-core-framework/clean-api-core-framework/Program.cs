using APICoreFramework.Middlewares;
using Application.Common;
using Application.Interfaces;
using Application.Products.Commands;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Seed;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add secrets only in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Now you can access it like this:
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register ApplicationDbContext (Persistence Layer) with Application Interface (Application Layer)
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((provider, options) =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// Register common services
builder.Services.AddScoped<IFileProcessingHandler, FileProcessingHandler>();

// Register Services with relevant interfaces
builder.Services.AddScoped<IProductService, ProductService>();

// Register IProductRepository with ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IIdempotencyRepository, IdempotencyRepository>();

// Register other services, MediatR, and controllers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();

// Build the app after all services are registered.
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            dbContext.Database.OpenConnection();
            Console.WriteLine("Connection successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Connection failed:");
            Console.WriteLine(ex.Message);
            throw;
        }
        if (!dbContext.Database.CanConnect())
        {
            throw new Exception("Database connection failed.");
        }
    }
}


// Seed the database with initial data
//Migrate DB and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Apply migrations
    context.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        // Seed
        SeedData.Initialize(services);
    }
}


app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use the custom middleware for idempotency
app.UseMiddleware<IdempotencyMiddleware>();
// Use the custom middleware for error handling
//app.UseMiddleware<ErrorHandlingMiddleware>();
// Use the custom middleware for logging
//app.UseMiddleware<LoggingMiddleware>();
// Use the custom middleware for performance monitoring
//app.UseMiddleware<PerformanceMonitoringMiddleware>();
// Use the custom middleware for authentication
//app.UseMiddleware<AuthenticationMiddleware>();
// Use the custom middleware for authorization
//app.UseMiddleware<AuthorizationMiddleware>();
// Use the custom middleware for caching
//app.UseMiddleware<CachingMiddleware>();
// Use the custom middleware for rate limiting
//app.UseMiddleware<RateLimitingMiddleware>();
// Use the custom middleware for request validation
//app.UseMiddleware<RequestValidationMiddleware>();
// Use the custom middleware for response compression
//app.UseMiddleware<ResponseCompressionMiddleware>();
// Use the custom middleware for response caching
//app.UseMiddleware<ResponseCachingMiddleware>();
// Use the custom middleware for cross-origin resource sharing (CORS)
//app.UseMiddleware<CorsMiddleware>();
// Use the custom middleware for security headers
//app.UseMiddleware<SecurityHeadersMiddleware>();

app.MapControllers();

app.Run();