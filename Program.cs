using Microsoft.EntityFrameworkCore;
using notesy_api_c_sharp.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173");
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var env = builder.Environment.EnvironmentName;
var dbPassword = builder.Configuration["DB_PASSWORD"];
var sslCaPath = builder.Configuration["SSL_CA_PATH"];
var prodConnection = $"Server=mysql-notesy-db-boyaga-37e8.f.aivencloud.com;" +
                     $"Port=28239;" +
                     $"Database=defaultdb;" +
                     $"User Id=avnadmin;" +
                     $"Password={dbPassword};" +
                     $"SslMode=VerifyCA;" +
                     $"SslCa={sslCaPath};";
var connectionString = env == "Production" ? prodConnection : builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.Now}));

app.MapControllers();

app.Run();
