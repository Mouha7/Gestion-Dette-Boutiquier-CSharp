using System;
using gestion_dette_web.Data;
using gestion_dette_web.Models;
using Microsoft.AspNetCore.Identity;

namespace gestion_dette_web.DataFixture;

public class Fixture
{
    private readonly ApplicationDbContext _dbContext;

    public Fixture(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Load()
    {
        if (!_dbContext.Clients.Any())
        {
            _dbContext.Clients.AddRange(
                new Client
                {
                    Surnom = "John Doe",
                    Email = "email@gmail.com",
                    Telephone = "771234567",
                    Adresse = "123 Rue des Bleus",
                    Dettes = [
                    new Dette { Montant = 1000, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)), MontantVerser = 1500},
                    new Dette { Montant = 2000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), MontantVerser = 1500 , Paiements = [
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Montant = 500 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Montant = 1000 }
                    ]},
                    new Dette { Montant = 3000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)) ,  Paiements = [
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Montant = 500 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Montant = 1000 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Montant = 1500 }
                    ]},
                    new Dette { Montant = 3000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), MontantVerser = 3000 },
                ]
                },
                new Client
                {
                    Surnom = "Jane Smith",
                    Email = "email@gmail.com",
                    Telephone = "789654321",
                    Adresse = "456 Rue des Verts",
                    Dettes = [
                    new Dette { Montant = 8000, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-10))},
                    new Dette { Montant = 6000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), MontantVerser = 4500,  Paiements = [
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Montant = 500 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), Montant = 1000 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Montant = 1500 },
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Montant = 500 }
                    ] },
                    new Dette { Montant = 1000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)) },
                    new Dette { Montant = 5000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),MontantVerser = 5000 },
                ]
                },
                new Client
                {
                    Surnom = "Alice Johnson",
                    Email = "email@gmail.com",
                    Telephone = "761472583",
                    Adresse = "789 Rue des Gris",
                    Dettes = [
                    new Dette { Montant = 1000, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3))},
                    new Dette { Montant = 2000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-8)), MontantVerser = 2000 },
                    new Dette { Montant = 3000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)) },
                    new Dette { Montant = 3000, Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), MontantVerser = 500,   Paiements = [
                        new Paiement { Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Montant = 500 },
                    ] },
                ]
                }
            );
            _dbContext.SaveChanges();
        }

        if (!_dbContext.Articles.Any()) {
            _dbContext.Articles.AddRange(
                new Article { Libelle = "Article 1", Prix = 100 },
                new Article { Libelle = "Article 2", Prix = 200 },
                new Article { Libelle = "Article 3", Prix = 300 }
            );
            _dbContext.SaveChanges();
        }
    }
}