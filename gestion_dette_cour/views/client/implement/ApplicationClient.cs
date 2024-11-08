using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;
using Main.Views;
using Main.Views.Clients;
using Main.Views.Implement;

namespace Main.Views.Clients.Implement
{
    public class ApplicationClient : Application, IApplicationClient {
    private IArticleService articleService;
    private IClientService clientService;
    private IDemandeDetteService demandeDetteService;
    private IDemandeDetteView demandeDetteView;
    private IDemandeArticleService demandeArticleService;
    private IDetteService detteService;
    private IDetteView detteView;

    public ApplicationClient(IArticleService articleService, IClientService clientService, IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView, IDemandeArticleService demandeArticleService, IDetteService detteService, IDetteView detteView) {
        this.articleService = articleService;
        this.demandeDetteService = demandeDetteService;
        this.demandeDetteView = demandeDetteView;
        this.demandeArticleService = demandeArticleService;
        this.detteService = detteService;
        this.detteView = detteView;
        this.clientService = clientService;
    }

    public override int menu() {
        string choice;
        string[] validValues = { "1", "2", "3", "4", "5" };
        do {
            Console.WriteLine("1- Lister des dettes non soldées");
            Console.WriteLine("2- Faire une demande de dette");
            Console.WriteLine("3- Lister des demandes de dette");
            Console.WriteLine("4- Relancer une demande de dette annuler");
            Console.WriteLine("5- Déconnexion");
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
                    displayDette(detteService, detteView);
                    break;
                case 2:
                    saisirDette(articleService, clientService, demandeDetteService, demandeDetteView, demandeArticleService, user);
                    break;
                case 3:
                    displayDemandeDette(demandeDetteService, demandeDetteView);
                    break;
                case 4:
                    relaunchDette(demandeDetteService, demandeDetteView);
                    break;
                default:
                    Console.WriteLine(MSG_EXIT);
                    break;
            }
        } while (choix != 5);
    }

    public void displayDette(IDetteService detteService, IDetteView detteView) {
        if (isEmpty(detteService.length(), "Aucun dette n'a été enregistré.")) {
            return;
        }
        Console.WriteLine("Choisissez l'id pour plus de detail");
        motif('+');
        detteView.afficher(detteService.findAll());
        Dette dette = detteView.getObject(detteService.findAll());
        motif('+');
        subMenu(dette);
    }

    public void displayPaiement(Dette dette) {
        dette.paiements.ForEach(item => Console.WriteLine(item?.ToString()));
    }

    public void displayArticle(Dette dette) {
        foreach (Detail detail in dette.details) {
            Console.WriteLine(detail.article);
        }
    }

    public void subMenu(Dette dette) {
        string choice;
        Console.WriteLine("1- Voir les articles");
        Console.WriteLine("2- Voir les paiements");
        do {
            Console.Write(MSG_CHOICE);
            choice = Console.ReadLine();
            if (choice.Equals("1")) {
                displayArticle(dette);
            } else if (choice.Equals("2")) {
                displayPaiement(dette);
            } else {
                Console.WriteLine("Erreur, choix invalide.");
            }
        } while (!choice.Equals("1") || !choice.Equals("2"));
    }

    public void saisirDette(IArticleService articleService, IClientService clientService,IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView, IDemandeArticleService demandeArticleService, User user) {
        DemandeDette dette = demandeDetteView.saisir(clientService, articleService, demandeArticleService, user);
        if (dette == null) {
            return;
        }
        demandeDetteService.add(dette);
        msgSuccess("Demande de dette ajoutée avec succès.");
    }

    public void displayDemandeDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView) {
        if (isEmpty(demandeDetteService.length(), "Aucune demande de dette n'a été enregistrée.")) {
            return;
        }
        demandeDetteView.afficher(demandeDetteService.findAll());
        Console.Write("Voulez-vous filtrer les demandes de dette(O/N): ");
        char choix = Console.ReadLine()[0];
        if (choix == 'O' || choix == 'o') {
            subMenuDemandeDette(demandeDetteService, demandeDetteView);
        }
        motif('+');
    }

    public void subMenuDemandeDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView) {
        string choice;
        Console.WriteLine("Filtrer par: ");
        Console.WriteLine("1- En cour la demande");
        Console.WriteLine("2- Annuler la demande");
        Console.Write(MSG_CHOICE);
        choice = Console.ReadLine();
        if (choice.Equals("1")) {
            List<DemandeDette> demandeDettes = demandeDetteService.findAll().Where(dette => dette.etat.CompareTo("ENCOURS") == 0).ToList();
            demandeDetteView.afficher(demandeDettes);
        } else if (choice.Equals("2")) {
            List<DemandeDette> demandeDettes = demandeDetteService.findAll().Where(dette => dette.etat.CompareTo("ANNULE") == 0).ToList();
            demandeDetteView.afficher(demandeDettes);
        } else {
            Console.WriteLine("Erreur, choix invalide.");
        }
    }

    public void relaunchDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView) {
        List<DemandeDette> demandeDettes = demandeDetteService.findAll().Where(dette => dette.etat.CompareTo("ANNULE") == 0).ToList();
        if (isEmpty(demandeDettes.Count, "Aucune demande de dette annulée n'a été trouvée.")) {
            return;
        }
        DemandeDette demandeDette = demandeDetteView.getObject(demandeDettes, demandeDetteService);
        demandeDette.etat = EtatDemandeDette.ENCOURS;
        demandeDetteService.update(demandeDettes, demandeDette);
        msgSuccess("Relancement de la demande de dette avec succès.");
    }
}
}