using System.Text.RegularExpressions;
using Main.Data.Entities;
using Main.Services;

namespace Main.Views.Implement
{
    public class ClientView : ImpView<Client>, IClientView
    {
        private IClientService clientService;

        public ClientView(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public Client? saisir(IClientService clientService)
        {
            Client client = new Client();
            Console.WriteLine("Entrez le surnom: ");
            client.surname = Console.ReadLine();
            // Vérification Surname unique
            if (clientService.findBy(clientService.findAll(), client) != null)
            {
                Console.WriteLine("Erreur, le surname est déjà utilisé.");
                return null;
            }
            client.tel = checkTel();
            // Vérification Tel unique
            if (clientService.findBy(clientService.findAll(), client) != null)
            {
                Console.WriteLine("Erreur, le téléphone appartient déjà à un utilisateur.");
                return null;
            }
            Console.WriteLine("Entrez l'adresse: ");
            client.address = Console.ReadLine();
            client.status = true;
            return client;
        }

        private string checkTel()
        {
            string? tel;
            do
            {
                Console.WriteLine("Entrez le numéro de téléphone : ");
                tel = Console.ReadLine();

                // Vérifie si le numéro commence par 70, 77 ou 78, et contient 9 chiffres au total
                if (!Regex.IsMatch(tel, @"^(70|77|78)\d{7}$"))
                {
                    Console.WriteLine("Format incorrect. Le numéro doit commencer par 70, 77 ou 78 et contenir 9 chiffres au total (par exemple : 77 xxx xx xx).");
                }
            } while (!Regex.IsMatch(tel, @"^(70|77|78)\d{7}$"));

            return "+221" + tel;
        }


        public override Client getObject(List<Client> clients)
        {
            Client client;
            string? choix;
            int count = clients.Count;
            this.display(clients);
            do
            {
                client = new Client();
                Console.WriteLine("Choisissez un client par son id: ");
                choix = Console.ReadLine();
                if (isInteger(choix))
                {
                    client.idClient = int.Parse(choix);
                    client = clientService.findBy(clients, client);
                }
                else
                {
                    continue;
                }
                if (client == null || int.Parse(choix) > count)
                {
                    Console.WriteLine("Erreur, l'id est invalide.");
                }
            } while (client == null);
            return client;
        }

        public void display(List<Client> clients)
        {
            Console.WriteLine("Liste des clients: ");
            foreach (Client client in clients)
            {
                displayClient(client);
            }
        }

        public void displayClient(Client client)
        {
            if (client == null)
            {
                Console.WriteLine("Aucun client trouvé.");
                return;
            }
            motif("+");
            Console.WriteLine("ID : " + client.idClient);
            Console.WriteLine("Surname : " + client.surname);
            Console.WriteLine("Tel : " + client.tel);
            Console.WriteLine("Adresse : " + client.address);
            Console.WriteLine("Cumul Montant Dû : " + client.getCumulMontantDu());
            Console.WriteLine("Status : " + client.status);
            Console.WriteLine("User : " + (client.user == null ? "N/A" : client.user));
            if (client.demandeDettes != null)
            {
                motif("-");
                Console.WriteLine("Liste Demande de dette : ");
                foreach (DemandeDette dette in client.demandeDettes)
                {
                    Console.WriteLine("Montant Total: " + dette.montantTotal);
                    Console.WriteLine("Date: " + dette.dateDemande);
                    Console.WriteLine("Etat: " + dette.etat);
                }
            }
            else
            {
                Console.WriteLine("Liste des demandes de dette : N/A");
            }
            if (client.dettes != null)
            {
                motif("-");
                Console.WriteLine("Liste de dette : ");
                foreach (Dette dette in client.dettes)
                {
                    Console.WriteLine("Montant Total: " + dette.montantTotal);
                    Console.WriteLine("Montant Verser: " + dette.montantVerser);
                    Console.WriteLine("Montant Restant: " + dette.getMontantRestant());
                    Console.WriteLine("Status: " + dette.status);
                    Console.WriteLine("État: " + dette.etat);
                    Console.WriteLine("Date: " + dette.dateCreation);
                }
            }
            else
            {
                Console.WriteLine("Liste des dettes : N/A");
            }
            motif("+");
        }

        public override Client saisir()
        {
            throw new NotImplementedException();
        }
    }
}