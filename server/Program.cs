using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Services;
using server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Acontrollers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// app services and repos
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPortService, PortService>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IVoyageService, VoyageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") //angular frontend
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowFrontend");
// Middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();