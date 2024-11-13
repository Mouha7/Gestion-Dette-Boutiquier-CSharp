using System;
using gestion_dette_web.Data;
using gestion_dette_web.Models;
using Microsoft.AspNetCore.Identity;

public class Fixture
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public Fixture(UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public void Load()
    {
        // Création de 50 clients aléatoires
        for (int i = 1; i <= 50; i++)
        {
            var client = new Client
            {
                Surnom = $"Surnom{i}",
                Telephone = $"77100101{i}",
                Adresse = $"Adresse{i}",
                Email = $"client{i}@example.com"
            };

            if (i % 2 == 0)
            {
                // Création d'un utilisateur et association avec le client
                var user = new User
                {
                    Nom = $"Nom{i}",
                    Prenom = $"Prénom{i}",
                    Login = $"login{i}",
                    Password = $"password{i}"
                };

                var result = _userManager.CreateAsync(user, $"password{i}").Result;
                if (result.Succeeded)
                {
                    client.User = user;
                }
            }

            // Création des dettes
            for (int j = 1; j <= 5; j++)
            {
                var dette = new Dette
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Montant = 150000 * j,
                    MontantVerser = i % 2 == 0? 150000 * j : 150000
                };
                client.Dettes!.Add(dette);
            }

            _dbContext.Clients.Add(client);
        }

        _dbContext.SaveChanges();
    }
}