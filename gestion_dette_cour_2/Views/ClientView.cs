using System.Collections;
using gestion_dette_cour_2.Entities;

namespace gestion_dette_cour_2.Views;

public class ClientView
{
    // Affiche tous les clients
        public void Index(IEnumerable<Client> clients)
        {
            // var clients = _clientRepository.GetAll();
            Console.WriteLine("Liste des clients:");
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.Id}, Nom: {client.Nom}, Email: {client.Email}");
            }
        }

        // Affiche les détails d'un client spécifique
        public void Details(Client client)
        {
            // var client = _clientRepository.GetById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable.");
                return;
            }
            Console.WriteLine($"Détails du client - ID: {client.Id}, Nom: {client.Nom}, Email: {client.Email}");
        }

        // Crée un nouveau client
        public Client Create()
        {
            var client = new Client();
            Console.Write("Entrez le nom du client: ");
            client.Nom = Console.ReadLine();
            Console.Write("Entrez l'email du client: ");
            client.Email = Console.ReadLine();

            // _clientRepository.Insert(client);
            Console.WriteLine("Client ajouté avec succès !");
            return client;
        }

        // Modifie un client existant
        public Client? Edit(Client client)
        {
            // var client = _clientRepository.GetById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable.");
                return null;
            }

            Console.WriteLine($"Modification du client - ID: {client.Id}, Nom actuel: {client.Nom}, Email actuel: {client.Email}");
            Console.Write("Entrez le nouveau nom (laisser vide pour ne pas changer): ");
            string newNom = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(newNom)) client.Nom = newNom;

            Console.Write("Entrez le nouvel email (laisser vide pour ne pas changer): ");
            string newEmail = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(newEmail)) client.Email = newEmail;

            // _clientRepository.Update(client);
            Console.WriteLine("Client mis à jour avec succès !");
            return client;
        }

        // Supprime un client
        public Client? Delete(Client client)
        {
            // var client = _clientRepository.GetById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable.");
                return null;
            }

            Console.WriteLine($"Confirmez la suppression du client - ID: {client.Id}, Nom: {client.Nom}");
            Console.Write("Tapez 'oui' pour confirmer: ");
            string confirmation = Console.ReadLine()!;
            if (confirmation.ToLower() == "oui")
            {
                // _clientRepository.Delete(id);
                Console.WriteLine("Client supprimé avec succès !");
                return client;
            }
            else
            {
                Console.WriteLine("Suppression annulée.");
                return null;
            }
        }
}