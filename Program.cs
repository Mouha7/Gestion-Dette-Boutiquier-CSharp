using Microsoft.EntityFrameworkCore;
using gestion_dette_web.Data;
using gestion_dette_web.services;
using gestion_dette_web.DataFixture;
using gestion_dette_web.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur.
builder.Services.AddControllersWithViews();

// Configuration de la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString)))
                ;

// Register the UserManager service
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Récupération de l'instance du service Fixture
// var fixture = builder.Services.BuildServiceProvider().GetRequiredService<Fixture>();
// fixture.Load();

// Enregistrement des repositories
builder.Services.AddScoped<IClientService, ClientService>();
// Ajoutez l'enregistrement du service Fixture
builder.Services.AddScoped<Fixture, Fixture>();

var app = builder.Build();

// Configure le pipeline de requête HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<Fixture>();
    seeder.Load();
}

app.Run();