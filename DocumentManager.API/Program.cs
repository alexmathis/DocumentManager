using Document_Manager.API.Middleware;
using DocumentManager.Application.Behaviors;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Interfaces;
using DocumentManager.Infrastructure;
using DocumentManager.Infrastructure.Persistence.Repositories;
using DocumentManager.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Presentation assembly (where the controllers live)
var presentationAssembly = typeof(DocumentManager.Presentation.AssemblyReference).Assembly;
builder.Services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(presentationAssembly));


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var applicationAssembly = typeof(DocumentManager.Application.AssemblyReference).Assembly;

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DocumentManager.API", Version = "v1" });
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IUnitOfWork>(
    factory => factory.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

//builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IFileStorageService, MockAzureFileStorageService>();

builder.Services.AddScoped<IDbConnection>(provider =>
{
    var dbContext = provider.GetRequiredService<ApplicationDbContext>();
    return dbContext.Database.GetDbConnection();
});


builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentManager.API"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

app.Run();