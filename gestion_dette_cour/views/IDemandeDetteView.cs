using Main.Data.Entities;
using Main.Services;

namespace Main.Views
{
    public interface IDemandeDetteView : IView<DemandeDette>
    {
        DemandeDette? saisir(IClientService clientService, IArticleService articleService, IDemandeArticleService demandeArticleService, User user);
        DemandeDette getObject(List<DemandeDette> list, IDemandeDetteService demandeDetteService);
        void afficherDemandeDette(DemandeDette demandeDette);
    }
}