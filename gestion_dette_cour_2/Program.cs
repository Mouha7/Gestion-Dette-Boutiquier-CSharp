using gestion_dette_cour_2.Repositories;
using gestion_dette_cour_2.Services;
using gestion_dette_cour_2.Views;

class Program
{
    static void Main(string[] args)
    {
        IClientRepository clientRepository = new ClientRepository();
        IClientService clientService = new ClientService(clientRepository);
        ClientView clientView = new ClientView();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Afficher tous les clients");
            Console.WriteLine("2. Afficher les détails d'un client");
            Console.WriteLine("3. Créer un nouveau client");
            Console.WriteLine("4. Modifier un client");
            Console.WriteLine("5. Supprimer un client");
            Console.WriteLine("0. Quitter");
            Console.Write("Sélectionnez une option : ");

            // Valider que l'entrée est un entier
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Veuillez entrer un nombre valide.");
                continue;
            }

            switch (option)
            {
                case 1:
                    clientView.Index(clientService.SelectAll());
                    break;
                case 2:
                    Console.Write("Entrez l'ID du client: ");
                    if (int.TryParse(Console.ReadLine(), out int idDetails))
                    {
                        clientView.Details(clientService.SelectById(idDetails));
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un ID valide.");
                    }
                    break;
                case 3:
                    clientService.Insert(clientView.Create());
                    break;
                case 4:
                    Console.Write("Entrez l'ID du client à modifier: ");
                    if (int.TryParse(Console.ReadLine(), out int idEdit))
                    {
                        clientService.Update(clientView.Edit(clientService.SelectById(idEdit))!);
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un ID valide.");
                    }
                    break;
                case 5:
                    Console.Write("Entrez l'ID du client à supprimer: ");
                    if (int.TryParse(Console.ReadLine(), out int idDelete))
                    {
                        clientView.Delete(clientService.SelectById(idDelete));
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un ID valide.");
                    }
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Option non valide.");
                    break;
            }
        }
    }
}
