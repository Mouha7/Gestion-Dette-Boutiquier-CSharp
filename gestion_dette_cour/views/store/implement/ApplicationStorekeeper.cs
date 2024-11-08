using System.Text.RegularExpressions;
using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;
using Main.Views.Implement;

namespace Main.Views.Store.Implement
{
    public class ApplicationStorekeeper : Application, IApplicationStorekeeper {
    private IArticleService articleService;
    private IClientService clientService;
    private IClientView clientView;
    private IDemandeDetteService demandeDetteService;
    private IDemandeDetteView demandeDetteView;
    private IDetailService detailService;
    private IDetteService detteService;
    private IDetteView detteView;
    private IPaiementService paiementService;
    private IPaiementView paiementView;
    private IUserService userService;
    private IUserView userView;

    public ApplicationStorekeeper(IArticleService articleService,
            IClientService clientService, IClientView clientView, IDemandeDetteService demandeDetteService,
            IDemandeDetteView demandeDetteView, IDetailService detailService,
            IDetteService detteService, IDetteView detteView, IPaiementService paiementService,
            IPaiementView paiementView, IUserService userService, IUserView userView) {
        this.articleService = articleService;
        this.clientService = clientService;
        this.clientView = clientView;
        this.demandeDetteService = demandeDetteService;
        this.demandeDetteView = demandeDetteView;
        this.detailService = detailService;
        this.detteService = detteService;
        this.detteView = detteView;
        this.paiementService = paiementService;
        this.paiementView = paiementView;
        this.userService = userService;
        this.userView = userView;
    }

    public override int menu() {
        string choice;
        string[] validValues = { "1", "2", "3", "4", "5", "6", "7", "8" };
        do {
            Console.WriteLine("1- Créer un client");
            Console.WriteLine("2- Lister les clients");
            Console.WriteLine("3- Rechercher un client");
            Console.WriteLine("4- Créer une dette");
            Console.WriteLine("5- Créer une paiement");
            Console.WriteLine("6- Lister les dettes non soldées");
            Console.WriteLine("7- Lister les demandes de dette en cours");
            Console.WriteLine("8- Déconnexion");
            Console.Write(MSG_CHOICE);
            choice = Console.ReadLine();
            if (!validValues.Contains(choice)) {
                Console.WriteLine("Erreur, choix de l'index du menu invalide.");
            }
        } while (!validValues.Contains(choice));
        return int.Parse(choice);
    }

    public override void run(User user) {
        int choix;
        msgWelcome(user);
        do {
            choix = menu();
            switch (choix) {
                case 1:
                    saisirClient(clientService, clientView, userService, userView);
                    break;
                case 2:
                    displayClient(clientService, clientView);
                    break;
                case 3:
                    searchClientByTel(clientService, clientView);
                    break;
                case 4:
                    saisirDette(articleService, clientService, clientView, detteService, detailService, paiementView);
                    break;
                case 5:
                    saisirPaiement(paiementService, paiementView, detteService, detteView);
                    break;
                case 6:
                    displayDetteNonSolde(clientService, clientView, detteService, detteView);
                    break;
                case 7:
                    displayDemandeDette(articleService, detteService, demandeDetteService, demandeDetteView,
                            detailService);
                    break;
                default:
                    break;
            }
        } while (choix != 8);
    }

    public void saisirClient(IClientService clientService, IClientView clientView, IUserService userService,
            IUserView userView) {
        Client client = clientView.saisir(clientService);
        clientService.add(client);
        Console.Write("Voulez-vous enregistrer un compte utilisateur(O/N): ");
        string choix = Console.ReadLine();
        if (choix.Equals("O") || choix .Equals("o")) {
            User user = userView.accountCustomer(userService, client);
            client.user= user;
            userService.add(user);
        }
        msgSuccess(MSG_ACCOUNT);
    }

    public void displayClient(IClientService clientService, IClientView clientView) {
        if (isEmpty(clientService.length(), MSG_CLIENT)) {
            return;
        }
        motif('+');
        clientView.display(clientService.findAll());
        Console.Write("Voulez-vous filtrer les clients avec compte ou sans compte(O/N): ");
        char choix = Console.ReadLine()[0];
        if (choix == 'O' || choix == 'o') {
            subMenuClient(clientService, clientView);
        } else if (!(choix == 'N' || choix == 'n')) {
            Console.WriteLine(MSG_ERROR);
        }
    }

    public void subMenuClient(IClientService clientService, IClientView clientView) {
        string choice;
        Console.WriteLine(MSG_FILTER);
        Console.WriteLine("1- Un compte");
        Console.WriteLine("2- Pas de compte");
        Console.Write(MSG_CHOICE);
        choice = Console.ReadLine();
        if (choice.Equals("1")) {
            List<Client> clients = clientService.findAll()
                    .Where(cl => cl.user != null)
                    .ToList();
            if (isEmpty(clients.Count, MSG_CLIENT)) {
                return;
            }
            motif('+');
            clientView.display(clients);
        } else if (choice.Equals("2")) {
            List<Client> clients = clientService.findAll()
                    .Where(cl => cl.user == null)
                    .ToList();
            if (isEmpty(clients.Count, MSG_CLIENT)) {
                return;
            }
            motif('+');
            clientView.display(clients);
        } else {
            Console.WriteLine(MSG_ERROR);
        }
    }

    public void searchClientByTel(IClientService clientService, IClientView clientView) {
        if (isEmpty(clientService.length(), MSG_CLIENT)) {
            return;
        }
        Client clientSearch = new Client();
        clientView.display(clientService.findAll());
        Console.Write("Entrer le tel du client à rechercher: ");
        clientSearch.tel = Console.ReadLine();
        Client client = clientService.findBy(clientService.findAll(), clientSearch);
        clientView.displayClient(client);
        motif('+');
    }

    public void saisirDette(IArticleService articleService, IClientService clientService,
            IClientView clientView, IDetteService detteService, IDetailService detailService, IPaiementView paiementView) {
        List<Article> articleAvailable = articleService.findAllAvailable();
        List<Client> clients = clientService.findAll();
        if (articleAvailable.Count == 0) {
            Console.WriteLine("Aucun article n'a été enregistré.");
            return;
        }
        if (clients.Count == 0) {
            Console.WriteLine("Aucun client n'a été enregistré.");
            return;
        }

        Client client = clientView.getObject(clientService.findAll());
        Dette dette = new Dette();
        Dette testDette;
        dette.client = client;
        string choice;
        do {
            displayAvailableArticles(articleAvailable);
            choice = getUserChoice();
            if (!choice.Equals("0")) {
                testDette = processArticleChoice(articleService, choice, articleAvailable, dette, detailService);
                if (testDette != null) {
                    dette = testDette;
                }
            }
        } while (!choice.Equals("0"));
        Console.Write("Voulez-vous enregistrer un(des) paiement(s) (O/N): ");
        char choicePay = Console.ReadLine()[0];
        if (choicePay == 'O' || choicePay == 'o') {
            Object[] result = getPaiementClient(paiementView, dette);
            Paiement paiement = (Paiement) result[0];
            dette = (Dette) result[1];
            dette.addPaiement(paiement);
        }
        // Transaction
        client.addDetteClient(dette);
        detteService.add(dette);
        msgSuccess("Dette effectué avec succès!");
    }

    private Object[] getPaiementClient(IPaiementView paiementView, Dette dette) {
        Paiement paiement;
        do {
            paiement = paiementView.saisir();
            if (paiement.montantPaye > dette.montantTotal) {
                Console.WriteLine("Erreur, le montant payé dépasse le montant total de la dette.");
            }
        } while (paiement.montantPaye > dette.montantTotal);
        paiement.dette = dette;
        dette.montantVerser = dette.montantVerser + paiement.montantPaye;
        return new Object[] { paiement, dette };
    }

    private void displayAvailableArticles(List<Article> articleAvailable) {
        articleAvailable.ForEach(item => Console.WriteLine(item?.ToString()));
    }

    private string getUserChoice() {
        Console.Write("Entrez l'id de l'article(0 pour terminer): ");
        return Console.ReadLine();
    }

    private Dette processArticleChoice(IArticleService articleService, string choice, List<Article> articleAvailable,
            Dette dette, IDetailService detailService) {
        int quantity = getValidQuantity();
        if (quantity <= -1)
            return null;

        Article article = findArticle(choice, articleAvailable);
        if (article == null)
            return null;

        if (!checkStock(article, quantity))
            return null;

        updateArticleStock(articleService, article, quantity);
        return addDemandeArticle(article, quantity, dette, detailService);
    }

    private int getValidQuantity() {
        Console.Write("Entrez la quantité: ");
        string qte = Console.ReadLine();

        if (!Regex.IsMatch(qte, "\\d+")) {
            Console.WriteLine("Erreur, la quantité est incorrecte.");
            return -1;
        }

        return int.Parse(qte);
    }

    private Article findArticle(string id, List<Article> articleAvailable) {
        Article article = new Article();
        if (Regex.IsMatch(id, "\\d")) {
            article.idArticle = int.Parse(id);
        }
        Article foundArticle = articleService.findBy(article, articleAvailable);

        if (foundArticle == null) {
            Console.WriteLine("Article non trouvé.");
        }

        return foundArticle;
    }

    private bool checkStock(Article article, int quantity) {
        if (article.qteStock < quantity) {
            Console.WriteLine("Quantité saisit supérieur à celui en stock.");
            return false;
        }
        return true;
    }

    private void updateArticleStock(IArticleService articleService, Article article, int quantity) {
        foreach (Article ar in articleService.findAllAvailable()) {
            if (ar.idArticle == article.idArticle) {
                ar.qteStock = ar.qteStock - quantity;
                return;
            }
        }
    }

    private Dette addDemandeArticle(Article article, int quantity, Dette dette, IDetailService detailService) {
        // Ajouter une dette
        dette.montantTotal = (double) (dette.montantTotal + (quantity * article.prix));
        dette.status = true;
        dette.etat = EtatDette.ENCOURS;
        // Ajouter un détail entre article et une dette
        Detail detail = new Detail();
        detail.qte = quantity;
        detail.prixVente = (double)article.prix;
        detail.article = article;
        detail.dette = dette;
        // Transaction
        detailService.add(detail);
        dette.addDetail(detail);
        return dette;
    }

    public void saisirPaiement(IPaiementService paiementService, IPaiementView paiementView, IDetteService detteService,
            IDetteView detteView) {
        List<Dette> dettes = detteService.findAll();
        if (dettes.Count == 0) {
            Console.WriteLine("Aucune dette n'a été enregistré.");
            return;
        }

        Dette dette = detteView.getObject(dettes);
        Object[] result = getPaiementClient(paiementView, dette);
        Paiement paiement = (Paiement) result[0];
        // Update + Add paiement
        dette = (Dette) result[1];
        dette.addPaiement(paiement);
        detteService.update(dettes, dette);
        paiementService.add(paiement);
        msgSuccess("Paiement effectué avec succès!");
    }

    public void displayDetteNonSolde(IClientService clientService, IClientView clientView, IDetteService detteService,
            IDetteView detteView) {
        List<Dette> dettes = detteService.getAllNonSoldes();
        if (dettes.Count == 0) {
            Console.WriteLine("Aucune dette non soldé n'a été enregistrée.");
            return;
        }
        Client client = clientView.getObject(clientService.findAll());
        subMenu(detteView, client);
    }

    private void subMenu(IDetteView detteView, Client client) {
        string choice;
        Console.WriteLine("1- Voir les articles");
        Console.WriteLine("2- Voir les paiements");
        Console.Write(MSG_CHOICE);
        choice = Console.ReadLine();
        if (choice.Equals("1")) {
            foreach (Dette dette in client.dettes) {
                detteView.displayDetail(dette);
            }
        } else if (choice.Equals("2")) {
            foreach (Dette dette in client.dettes) {
                detteView.displayPay(dette);
            }
        } else {
            Console.WriteLine("Erreur, choix invalide.");
        }
    }

    public void displayDemandeDette(IArticleService articleService, IDetteService detteService,
            IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView,
            IDetailService detailService) {
        List<DemandeDette> demandeDettesEnCours = demandeDetteService.findAll()
                .Where(dette => dette.etat.CompareTo("ENCOURS") == 0).ToList();
        if (isEmpty(demandeDettesEnCours.Count, "Aucune demande de dette n'a été enregistrée.")) {
            return;
        }
        Console.WriteLine("1- filtrer les demandes de dette");
        Console.WriteLine("2- Voir une demande de dette");
        Console.Write("Voulez-vous : ");
        string choice = Console.ReadLine();
        if (choice.Equals("1")) {
            subMenuDemandeDette(demandeDetteService, demandeDetteView);
        } else if (choice.Equals("2")) {
            DemandeDette demandeDette = demandeDetteView.getObject(demandeDettesEnCours);
            demandeDetteView.afficherDemandeDette(demandeDette);
            motif('+');
            askDemandeDette(articleService, detteService, demandeDetteService, demandeDette, detailService, clientService);
        } else {
            Console.WriteLine(MSG_ERROR);
        }
    }

    private void subMenuDemandeDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView) {
        string choice;
        Console.WriteLine(MSG_FILTER);
        Console.WriteLine("1- En cour la demande");
        Console.WriteLine("2- Annuler la demande");
        Console.Write(MSG_CHOICE);
        choice = Console.ReadLine();
        if (choice.Equals("1")) {
            List<DemandeDette> demandeDettes = demandeDetteService.findAll()
                    .Where(dette => dette.etat.CompareTo("ENCOURS") == 0).ToList();
            if (isEmpty(demandeDetteService.length(), "Aucune demande de dette (En cour) n'a été enregistrée.")) {
                return;
            }
            demandeDetteView.afficher(demandeDettes);

        } else if (choice.Equals("2")) {
            List<DemandeDette> demandeDettes = demandeDetteService.findAll()
                    .Where(dette => dette.etat.CompareTo("ANNULE") == 0).ToList();
            if (isEmpty(demandeDetteService.length(), "Aucune demande de dette (Annulée) n'a été enregistrée.")) {
                return;
            }
            demandeDetteView.afficher(demandeDettes);
        } else {
            Console.WriteLine("Erreur, choix invalide.");
        }
    }

    private void askDemandeDette(IArticleService articleService, IDetteService detteService, IDemandeDetteService demandeDetteService,
            DemandeDette demandeDette, IDetailService detailService, IClientService clientService) {
        Console.Write("Voulez-vous Annuler ou Accepter la demande de dette(O/N): ");
        char ask = Console.ReadLine()[0];
        if (ask == 'O' || ask == 'o') {
            // Créé une dette
            Dette dette = new Dette();
            dette.montantTotal = demandeDette.montantTotal;
            dette.status = true;
            dette.etat = EtatDette.ENCOURS;
            dette.client = demandeDette.client;
            dette.demandeDette = demandeDette;
            // Créé une détail
            foreach (DemandeArticle a in demandeDette.demandeArticles) {
                // Update Article
                Article article = articleService.findBy(a.article, articleService.findAllAvailable());
                article.qteStock = article.qteStock - a.qteArticle;
                articleService.update(article, (int)article.qteStock);
                // Création détail
                Detail detail = new Detail();
                detail.qte = a.qteArticle;
                detail.prixVente = (double)article.prix;
                detail.article = article;
                detail.dette = dette;
                // Ajout détail à la dette
                dette.addDetail(detail);
                // Transaction
                detailService.add(detail);
            }
            demandeDette.etat = EtatDemandeDette.VALIDE;
            demandeDetteService.update(demandeDetteService.findAll(), demandeDette);
            detteService.add(dette);
            // Update dette client
            Client client = demandeDette.client;
            client.addDetteClient(dette);
            clientService.update(clientService.findAll(), client);
            msgSuccess("Demande de dette accepter avec succès.");
        } else if (ask == 'n' || ask == 'N') {
            demandeDette.etat = EtatDemandeDette.ANNULE;
            demandeDetteService.update(demandeDetteService.findAll(), demandeDette);
            msgSuccess("Demande de dette refuser avec succès.");
        }
        else {
            Console.WriteLine(MSG_ERROR);
        }
    }
}
}