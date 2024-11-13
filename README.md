# Installer l'outil EF Core
dotnet tool install --global dotnet-ef

# Créer la migration initiale
dotnet ef migrations add InitialCreate

# Mettre à jour la base de données
dotnet ef database update

# Live Preview
dotnet watch run