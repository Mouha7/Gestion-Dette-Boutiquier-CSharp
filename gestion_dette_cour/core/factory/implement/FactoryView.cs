using Main.Views;
using Main.Views.Implement;

namespace Main.Core.Factory.Implement
{
    public class FactoryView : IFactoryView {
    private IArticleView articleView;
    private IClientView clientView;
    private IDemandeDetteView demandeDetteView;
    private IDetteView detteView;
    private IPaiementView paiementView;
    private IUserView userView;
    private IFactoryService factoryService;

    public FactoryView(IFactoryService factoryService) {
        this.factoryService = factoryService;
    }

    public IArticleView getInstanceArticleView() {
        return articleView == null ? new ArticleView(factoryService.getInstanceArticleService()) : articleView;
    }

    public IClientView getInstanceClientView() {
        return clientView == null ? new ClientView(factoryService.getInstanceClientService()) : clientView;
    }

    public IDemandeDetteView getInstanceDemandeDetteView() {
        return demandeDetteView == null ? new DemandeDetteView(factoryService.getInstanceDemandeDetteService()) : demandeDetteView;
    }

    public IDetteView getInstanceDetteView() {
        return detteView == null ? new DetteView(factoryService.getInstanceDetteService()) : detteView;
    }

    public IPaiementView getInstancePaiementView() {
        return paiementView ?? new PaiementView(factoryService.getInstancePaiementService());
    }

    public IUserView getInstanceUserView() {
        if (userView == null) {
            userView = new UserView(factoryService.getInstanceUserService());
        }
        return userView;
    }
}
}