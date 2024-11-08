using Main.Views;

namespace Main.Core.Factory
{
    public interface IFactoryView
    {
        IArticleView getInstanceArticleView();
        IClientView getInstanceClientView();
        IDemandeDetteView getInstanceDemandeDetteView();
        IUserView getInstanceUserView();
        IPaiementView getInstancePaiementView();
        IDetteView getInstanceDetteView();
    }
}