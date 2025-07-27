using Microsoft.OpenApi.Models;
using ReportsBackend.Api.Middleware;
using ReportsBackend.Application.Extensions;
using ReportsBackend.Infrastracture.Data.Context;
using ReportsBackend.Infrastracture.Extensions;
using ReportsBackend.Infrastracture.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configure Swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

if (builder.Environment.IsDevelopment())
{

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            policy =>
            {
                policy.WithOrigins("http://localhost:8080") // Allow React app
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // If using authentication
            });
    });


}

else
{

    builder.Services.AddCors(
        options =>
        {
            options.AddPolicy("AllowReactApp", builder =>
            builder.SetIsOriginAllowed(origin =>
            {

                try
                {
                    var uri = new Uri(origin);
                    return uri.Host == "localhost" || uri.Host.StartsWith("192.168.40");

                }
                catch
                {
                    return false;
                }
            }).AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
        });



}

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var applicationDbcontext = services.GetRequiredService<ApplicationDbContext>();
    applicationDbcontext.Database.EnsureCreated();
    ApplicationInitializer.Initializer(applicationDbcontext);
}

app.UseCors("AllowReactApp");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
