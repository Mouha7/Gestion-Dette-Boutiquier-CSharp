using Main.Data.Entities;
using Main.Services;
using Main.Views.Implement;

namespace Main.Views.Admin.Implement
{
    public class ApplicationAdmin : Application, IApplicationAdmin
    {
        private IArticleService articleService;
        private IArticleView articleView;
        private IClientService clientService;
        private IClientView clientView;
        private IDetteService detteService;
        private IDetteView detteView;
        private IUserService userService;
        private IUserView userView;

        public ApplicationAdmin(IArticleService articleService, IArticleView articleView, IClientService clientService,
                IClientView clientView, IDetteService detteService, IDetteView detteView, IUserService userService,
                IUserView userView)
        {
            this.articleService = articleService;
            this.articleView = articleView;
            this.clientService = clientService;
            this.clientView = clientView;
            this.detteService = detteService;
            this.detteView = detteView;
            this.userService = userService;
            this.userView = userView;
        }

        public override void run(User user)
        {
            int choix;
            msgWelcome(user);
            do
            {
                choix = menu();
                switch (choix)
                {
                    case 1:
                        createAccountCustomer(clientService, clientView, userService, userView);
                        break;
                    case 2:
                        createAccountUser(userService, userView);
                        break;
                    case 3:
                        activeDesactiveAccount(userService, userView, user);
                        break;
                    case 4:
                        listingUserActifs(userService, userView, user);
                        break;
                    case 5:
                        createArticle(articleService, articleView);
                        break;
                    case 6:
                        listingArticleAvailable(articleService, articleView);
                        break;
                    case 7:
                        updateQte(articleService, articleView);
                        break;
                    case 8:
                        soldes(detteService, detteView);
                        break;
                    default:
                        Console.WriteLine(MSG_EXIT);
                        break;
                }
            } while (choix != 9);
        }

        public override int menu()
        {
            string[] validValues = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string choice;
            do
            {
                Console.WriteLine("1- Créer un compte à un client n'ayant pas de compte");
                Console.WriteLine("2- Créer un compte utilisateur (Admin ou Boutiquier)");
                Console.WriteLine("3- Activer/Désactiver un compte utilisateur");
                Console.WriteLine("4- Lister les comptes utilisateurs actif par rôle");
                Console.WriteLine("5- Créer un article");
                Console.WriteLine("6- Lister les articles disponibles");
                Console.WriteLine("7- Mettre à jour la quantité en stock d'un article");
                Console.WriteLine("8- Archiver les dettes soldées");
                Console.WriteLine("9- Déconnexion");
                Console.Write(MSG_CHOICE);
                choice = Console.ReadLine();

                // Vérification si le choix est valide
                if (!validValues.Contains(choice))
                {
                    Console.WriteLine("Erreur, choix de l'index du menu invalide.");
                }
            } while (!validValues.Contains(choice));

            return int.Parse(choice);
        }

        public int status()
        {
            string choix;
            do
            {
                Console.WriteLine("1- Compte utilisateur (Client)");
                Console.WriteLine("2- Compte utilisateur (Admin ou Boutiquier)");
                Console.Write(MSG_CHOICE);
                choix = Console.ReadLine();
                if (!choix.Equals("1") && !choix.Equals("2"))
                {
                    Console.WriteLine("Erreur, choix invalide. Veuillez entrer 1 ou 2.");
                }
            } while (!choix.Equals("1") && !choix.Equals("2"));

            return int.Parse(choix);
        }

        public void createAccountUser(IUserService userService, IUserView userView)
        {
            User user = userView.saisir(userService);
            if (user != null)
            {
                userService.add(user);
                msgSuccess(MSG_ACCOUNT);
            }
        }

        public void msgStatus(bool state)
        {
            if (state)
            {
                Console.WriteLine("Activer avec succès");
            }
            else
            {
                Console.WriteLine("Désactiver avec succès");
            }
        }

        public int role()
        {
            string choix;
            do
            {
                Console.WriteLine("1- Admin");
                Console.WriteLine("2- Boutiquier");
                Console.WriteLine("3- Client");
                Console.Write(MSG_CHOICE);
                choix = Console.ReadLine();
                if (!choix.Equals("1") && !choix.Equals("2") && !choix.Equals("3"))
                {
                    Console.WriteLine("Erreur, choix invalide. Veuillez entrer 1 ou 2 ou 3.");
                }
            } while (!choix.Equals("1") && !choix.Equals("2") && !choix.Equals("3"));

            return int.Parse(choix);
        }

        public void soldes(IDetteService detteService, IDetteView detteView)
        {
            if (isEmpty(detteService.length(), "Aucun compte soldé n'a été enregistré."))
            {
                return;
            }
            Dette dette = detteView.getObject(detteService.getAllSoldes());
            bool state = !dette.status;
            detteService.setStatus(dette, state);
            msgStatus(state);
        }

        public void updateQte(IArticleService articleService, IArticleView articleView)
        {
            if (isEmpty(articleService.length(), "Aucun article n'a été enregistré."))
            {
                return;
            }
            Article article = articleView.getObject(articleService.findAll());
            int newQte = int.Parse(articleView.check("Entrez la nouvelle quantité de l'article : ", "la quantité").ToString());
            articleService.setQte(article, newQte);
            msgSuccess("Modifiée avec succès !");
        }

        public void listingArticleAvailable(IArticleService articleService, IArticleView articleView)
        {
            if (isEmpty(articleService.length(), "Aucun article n'a été enregistré."))
            {
                return;
            }
            articleView.afficher(articleService.findAllAvailable());
        }

        public void createArticle(IArticleService articleService, IArticleView articleView)
        {
            articleService.add(articleView.saisir());
            msgSuccess();
        }

        public void listingUserActifs(IUserService userService, IUserView userView, User userConnect)
        {
            int choixRole = role();
            switch (choixRole)
            {
                case 1:
                    if (isEmpty(userService.length(), "Aucun admin n'a été enregistré."))
                    {
                        break;
                    }
                    if (isEmpty(userService.getAllActifs(0, userConnect).Count, "Aucun admin n'est actif."))
                    {
                        break;
                    }
                    userView.afficher(userService.getAllActifs(0, userConnect));
                    break;
                case 2:
                    if (isEmpty(userService.length(), "Aucun boutiquier n'a été enregistré."))
                    {
                        break;
                    }
                    if (isEmpty(userService.getAllActifs(1, userConnect).Count, "Aucun boutiquier n'est actif."))
                    {
                        break;
                    }
                    userView.afficher(userService.getAllActifs(1, userConnect));
                    break;
                case 3:
                    if (isEmpty(userService.length(), MSG_CLIENT))
                    {
                        break;
                    }
                    if (isEmpty(userService.getAllActifs(2, userConnect).Count, "Aucun client n'est actif."))
                    {
                        break;
                    }
                    userView.afficher(userService.getAllActifs(2, userConnect));
                    break;
                default:
                    break;
            }
        }

        public void activeDesactiveAccount(IUserService userService, IUserView userView, User userConnect)
        {
            List<User> users = userService.findAll()
                                .Where(us => us.idUser != userConnect.idUser)
                                .ToList();
            if (isEmpty(users.Count, "Aucun compte admin ou boutiquier ou client n'a été enregistré."))
            {
                return;
            }
            User user = userView.getObject(users);
            bool state = !user.status;
            userService.setStatus(user, state);
            msgStatus(state);
        }

        public void createAccountCustomer(IClientService clientService, IClientView clientView, IUserService userService,
                IUserView userView)
        {
            List<Client> clients = clientService.findAllCustomerAvailable();
            if (isEmpty(clients.Count, MSG_CLIENT))
            {
                return;
            }
            Client client = clientView.getObject(clientService.findAllCustomerAvailable());
            User user = userView.accountCustomer(userService, client);
            if (user != null)
            {
                client.user = user;
                clientService.update(clients, client);
                userService.add(user);
                msgSuccess(MSG_ACCOUNT);
            }
        }

    }
}