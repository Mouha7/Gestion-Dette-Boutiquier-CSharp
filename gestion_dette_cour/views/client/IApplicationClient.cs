using Main.Data.Entities;
using Main.Services;

namespace Main.Views.Clients
{
    public interface IApplicationClient : IApplication
    {
        void displayDette(IDetteService detteService, IDetteView detteView);
        void displayPaiement(Dette dette);
        void displayArticle(Dette dette);
        void subMenu(Dette dette);
        void saisirDette(IArticleService articleService, IClientService clientService, IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView, IDemandeArticleService demandeArticleService, User user);
        void displayDemandeDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView);
        void subMenuDemandeDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView);
        void relaunchDette(IDemandeDetteService demandeDetteService, IDemandeDetteView demandeDetteView);
    }
}