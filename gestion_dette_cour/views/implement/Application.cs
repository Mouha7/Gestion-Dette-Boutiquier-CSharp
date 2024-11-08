using Main.Data.Entities;

namespace Main.Views.Implement
{
    public abstract class Application : IApplication
    {
        protected static string MSG_CHOICE = "Choisissez une option : ";
        protected static string MSG_EXIT = "Merci d'avoir utiliser notre application, au revoir !";
        protected static string MSG_CLIENT = "Aucun client n'a été enregistré.";
        protected static string MSG_ACCOUNT = "Compte créer avec succès !";
        protected static string MSG_ERROR = "Erreur, choix invalide.";
        protected static string MSG_FILTER = "Filtrer par: ";

        public bool isEmpty(int size, string msg)
        {
            if (size == 0)
            {
                Console.WriteLine(msg);
                return true;
            }
            return false;
        }

        public void msgWelcome(User user)
        {
            Console.WriteLine("Bienvenue " + user.login + "!");
        }

        public void msgSuccess()
        {
            msgSuccess("Ajouté avec succès !");
        }

        public void msgSuccess(string msg)
        {
            Console.WriteLine(msg);
        }

        public void motif(char c)
        {
            motif(c.ToString(), 64);
        }

        public void motif(string letter, int nbr)
        {
            Console.WriteLine(new string(letter[0], nbr));
        }

        public bool isDigit(string number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(number, @"^\d$");
        }

        public bool isInteger(string number)
        {
            return int.TryParse(number, out _);
        }

        public bool isDecimal(string number)
        {
            return double.TryParse(number, out _);
        }

        // Déclare les méthodes requises par l'interface comme abstraites
        public abstract int menu();

        public abstract void run(User user);

        // Mettre en place les entrer pour continuer par exemple:
        // "Appuyez sur une touche pour continuer..."
    }
}