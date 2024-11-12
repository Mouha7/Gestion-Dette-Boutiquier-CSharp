using Microsoft.EntityFrameworkCore;
using gestion_dette_web.Data;
using gestion_dette_web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur.
builder.Services.AddControllersWithViews();

// Configuration de la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, 
        ServerVersion.AutoDetect(connectionString)));

// Enregistrement des repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();

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

app.Run();