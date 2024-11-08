using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;

namespace Main.Core.Config.Security
{
    public class Connexion : IConnexion {
    private IUserService userService;

    public Connexion(IUserService userService) {
        this.userService = userService;
        User user = new User("admin@admin.sn", "a", "a", true, Role.ADMIN);
        userService.add(user);
        User client = new User("client@admin.sn", "c", "c", true, Role.CLIENT);
        userService.add(client);
        User boutiquier = new User("boutiquier@admin.sn", "b", "b", true, Role.BOUTIQUIER);
        userService.add(boutiquier);
    }
    
    public User connexion() {
        User user = null;
        do {
            Console.WriteLine("Bienvenue sur l'application de gestion de dette");
            Console.WriteLine("Veuillez-vous connecter");
            // Demander le login
            Console.Write("Login : ");
            String login = Console.ReadLine();
            // Demander le mot de passe
            Console.Write("Password : ");
            String password = Console.ReadLine();
            // Récupération de l'utilisateur par login et mot de passe
            user = userService.getByLogin(login, password);
            // Vérification des conditions de connexion
            if (user == null) {
                Console.WriteLine("Login ou Mot de passe incorrect.");
            } else if (!user.status) {
                Console.WriteLine("Votre compte est désactivé.");
                user = null; // Pour forcer la boucle à redemander les identifiants
            }
        } while (user == null); // Tant que l'utilisateur est null (mauvais login ou compte désactivé)
        // Retourne l'utilisateur authentifié
        return user;
    }
    
}
}