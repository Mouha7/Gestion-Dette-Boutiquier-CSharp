using System.Text.RegularExpressions;
using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;

namespace Main.Views.Implement
{
    public class UserView : ImpView<User>, IUserView
    {
        private IUserService userService;
        private static string MSG_EMAIL = "Entrez votre adresse email : ";
        private static string MSG_LOGIN = "Entrez votre login : ";
        private static string MSG_PASSWORD = "Entrez votre mot de passe : ";

        public UserView(IUserService userService)
        {
            this.userService = userService;
        }

        public User accountCustomer(IUserService userService, Client client)
        {
            User user = new User();
            user.email = checkEmail();
            // Vérification Email unique
            if (userService.findBy(userService.findAll(), user) != null)
            {
                Console.WriteLine("Erreur, l'email est déjà utilisé.");
                return null;
            }
            Console.Write(MSG_LOGIN);
            user.login = Console.ReadLine();
            // Vérification Login unique
            if (userService.findBy(userService.findAll(), user) != null)
            {
                Console.WriteLine("Erreur, le login est déjà utilisé.");
                return null;
            }
            Console.Write(MSG_PASSWORD);
            user.password = Console.ReadLine();
            user.status = true;
            user.role = Role.CLIENT;
            user.client = client;
            return user;
        }

        public User? saisir(IUserService userService)
        {
            User user = new User();
            user.email = checkEmail();

            // Vérification Email unique
            if (userService.findBy(userService.findAll(), user) != null)
            {
                Console.WriteLine("Erreur, l'email est déjà utilisé.");
                return null;
            }

            Console.Write("Veuillez entrer votre login : ");
            user.login = Console.ReadLine();

            // Vérification Login unique
            if (userService.findBy(userService.findAll(), user) != null)
            {
                Console.WriteLine("Erreur, le login est déjà utilisé.");
                return null;
            }
            Console.Write("Veuillez entrer votre mot de passe : ");
            user.password = Console.ReadLine();
            // Sélection du rôle
            Role[] roles = (Role[])Enum.GetValues(typeof(Role));
            int roleIndex = typeRole() - 1;

            if (roleIndex >= 0 && roleIndex < roles.Length)
            {
                user.role = roles[roleIndex];
            }
            else
            {
                Console.WriteLine("Erreur : rôle invalide.");
                return null;
            }

            user.status = true;
            return user;
        }


        private string checkEmail()
        {
            string email;
            // Regex pour un email valide
            string emailRegex = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

            do
            {
                Console.Write(MSG_EMAIL);
                email = Console.ReadLine();
                // Vérifie si l'email correspond au format attendu
                if (!Regex.IsMatch(email, emailRegex))
                {
                    Console.WriteLine("Format incorrect. Veuillez entrer un email valide (par exemple : exemple@domaine.com).");
                }
            } while (!Regex.IsMatch(email, emailRegex));

            return email;
        }


        public int typeRole()
        {
            string choix;
            do
            {
                Console.WriteLine("(1)- " + Role.ADMIN);
                Console.WriteLine("(2)- " + Role.BOUTIQUIER);
                Console.Write("Choisissez un type d'utilisateur : ");
                choix = Console.ReadLine();
                if (!choix.Equals("1") && !choix.Equals("2"))
                {
                    Console.WriteLine("Erreur, choix invalide. Veuillez entrer 1 ou 2.");
                }
            } while (!choix.Equals("1") && !choix.Equals("2"));
            return int.Parse(choix);
        }

        public override User getObject(List<User> users)
        {
            User user;
            string choix;
            int count = users.Count;
            this.afficher(users);
            do
            {
                user = new User();
                Console.Write("Choisissez une user par son id: ");
                choix = Console.ReadLine();
                if (isInteger(choix))
                {
                    user.idUser = int.Parse(choix);
                    user = userService.findBy(user);
                }
                else
                {
                    continue;
                }
                if (user == null || int.Parse(choix) > count)
                {
                    Console.WriteLine("Erreur, l'id est invalide.");
                }
            } while (user == null);
            return user;
        }

        public override User saisir()
        {
            throw new NotImplementedException();
        }
    }
}