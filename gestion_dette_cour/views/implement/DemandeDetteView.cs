using System.Text.RegularExpressions;
using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;

namespace Main.Views.Implement
{
    public class DemandeDetteView : ImpView<DemandeDette>, IDemandeDetteView
    {
        private IDemandeDetteService demandeDetteService;

        public DemandeDetteView(IDemandeDetteService demandeDetteService)
        {
            this.demandeDetteService = demandeDetteService;
        }

        public DemandeDette? saisir(IClientService clientService, IArticleService articleService, IDemandeArticleService demandeArticleService, User user)
        {
            List<Article> articleAvailable = articleService.findAllAvailable();
            if (articleAvailable.Count == 0)
            {
                Console.WriteLine("Aucun article n'a été enregistré.");
                return null;
            }
            DemandeDette demandeDette = initializeDemandeDette(clientService, user);
            string choice;
            do
            {
                displayAvailableArticles(articleAvailable);
                choice = getUserChoice();
                if (!choice.Equals("0"))
                {
                    processArticleChoice(choice, articleAvailable, articleService, demandeDette, demandeArticleService);
                }
            } while (!choice.Equals("0"));

            // Add demande de dette à un client
            Client client = clientService.findBy(clientService.findAll(), demandeDette.client);
            client.addDemandeDette(demandeDette);
            // Transaction
            clientService.update(clientService.findAll(), client);
            return demandeDette;
        }

        public void afficher(List<DemandeDette> list)
        {
            foreach (DemandeDette demandeDette in list)
            {
                afficherDemandeDette(demandeDette);
            }
        }

        public void afficherDemandeDette(DemandeDette demandeDette)
        {
            Console.WriteLine("ID: " + demandeDette.idDemandeDette);
            Console.WriteLine("Date: " + demandeDette.dateDemande);
            Console.WriteLine("Montant total: " + demandeDette.montantTotal + " Franc CFA");
            Console.WriteLine("État: " + demandeDette.etat);
            Console.WriteLine("Client: " + (demandeDette.client != null ? demandeDette.client.user.login : "N/A"));
            Console.WriteLine("---Articles demandés---");
            foreach (DemandeArticle da in demandeDette.demandeArticles)
            {
                Console.WriteLine("  - Libelle: " + da.article.libelle + " (Quantité: " + da.qteArticle + ")");
            }
        }

        private DemandeDette initializeDemandeDette(IClientService clientService, User user)
        {
            DemandeDette demandeDette = new DemandeDette();
            Client client = new Client();
            client.user = user;
            if (clientService.findBy(client) == null)
            {
                client.user = user;
                demandeDette.client = client;
            }
            else
            {
                demandeDette.client = clientService.findBy(client);
            }
            clientService.update(clientService.findAll(), client);
            demandeDette.etat = EtatDemandeDette.ENCOURS;
            return demandeDette;
        }

        private void displayAvailableArticles(List<Article> articleAvailable)
        {
            articleAvailable.ForEach(item => Console.WriteLine(item?.ToString())); ;
        }

        private string? getUserChoice()
        {
            Console.Write("Entrez l'id de l'article de la demande de dette(0 pour terminer): ");
            return Console.ReadLine();
        }

        private void processArticleChoice(string choice, List<Article> articleAvailable, IArticleService articleService, DemandeDette demandeDette, IDemandeArticleService demandeArticleService)
        {
            int quantity = getValidQuantity();
            if (quantity <= -1)
                return;

            Article article = findArticle(choice, articleAvailable, articleService);
            if (article == null)
                return;

            if (!checkStock(article, quantity))
                return;

            updateArticleStock(article, quantity);
            addDemandeArticle(article, quantity, demandeDette, demandeArticleService);
        }

        private int getValidQuantity()
        {
            Console.Write("Entrez la quantité: ");
            string? qte = Console.ReadLine();

            if (!Regex.IsMatch(qte, "\\d+"))
            {
                Console.WriteLine("Erreur, la quantité est incorrecte.");
                return -1;
            }

            return int.Parse(qte);
        }

        private Article? findArticle(string id, List<Article> articleAvailable, IArticleService articleService)
        {
            Article article = new Article();
            if (Regex.IsMatch(id, "\\d"))
            {
                article.idArticle = int.Parse(id);
            }
            Article foundArticle = articleService.findBy(article, articleAvailable);

            if (foundArticle == null)
            {
                Console.WriteLine("Article non trouvé.");
            }

            return foundArticle;
        }

        private bool checkStock(Article article, int quantity)
        {
            if (article.qteStock < quantity)
            {
                Console.WriteLine("Quantité insuffisante en stock.");
                return false;
            }
            return true;
        }

        private void updateArticleStock(Article article, int quantity)
        {
            article.qteStock = article.qteStock - quantity;
        }

        private void addDemandeArticle(Article article, int quantity, DemandeDette demandeDette, IDemandeArticleService demandeArticleService)
        {
            // créer une demande d'article
            DemandeArticle demandeArticle = new DemandeArticle();
            demandeArticle.qteArticle = quantity;
            demandeArticle.article = article;
            demandeArticle.demandeDette = demandeDette;

            demandeDette.montantTotal = (double)(article.prix * quantity);
            demandeArticle.demandeDette = demandeDette;
            demandeDette.addDemandeArticle(demandeArticle);
            // Transaction
            demandeArticleService.add(demandeArticle);
        }

        public override DemandeDette getObject(List<DemandeDette> list)
        {
            DemandeDette demandeDette;
            string choix;
            int count = list.Count;
            this.afficher(list);
            do
            {
                demandeDette = new DemandeDette();
                Console.Write("Choisissez une demande de dette par son id: ");
                choix = Console.ReadLine();
                if (isInteger(choix))
                {
                    demandeDette.idDemandeDette = int.Parse(choix);
                    demandeDette = demandeDetteService.findBy(list, demandeDette);
                }
                else
                {
                    continue;
                }
                if (demandeDette == null || int.Parse(choix) > count)
                {
                    Console.WriteLine("Erreur, l'id est invalide.");
                }

            } while (demandeDette == null);
            return demandeDette;
        }

        public DemandeDette getObject(List<DemandeDette> list, IDemandeDetteService demandeDetteService)
        {
            DemandeDette demandeDette;
            string choix;
            int count = list.Count;
            this.afficher(list);
            do
            {
                demandeDette = new DemandeDette();
                Console.Write("Choisissez une demande de dette par son id: ");
                choix = Console.ReadLine();
                if (Regex.IsMatch(choix, "\\d"))
                {
                    demandeDette.idDemandeDette = int.Parse(choix);
                    demandeDette = demandeDetteService.findBy(list, demandeDette);
                }
                if (demandeDette == null || int.Parse(choix) > count)
                {
                    Console.WriteLine("Erreur, l'id est invalide.");
                }

            } while (demandeDette == null || int.Parse(choix) > count);
            return demandeDette;
        }

        public override DemandeDette saisir()
        {
            throw new NotImplementedException();
        }
    }
}