using Main.Core.Config.Router;
using Main.Core.Factory;
using Main.Core.Factory.Implement;

namespace Main // Remplacez par le nom de votre namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuration initiale
            IFactory factory = Factory.getInstance();
            IRouter router = new Router(factory);

            // Démarrage de l'application
            router.navigate();
        }
    }
}