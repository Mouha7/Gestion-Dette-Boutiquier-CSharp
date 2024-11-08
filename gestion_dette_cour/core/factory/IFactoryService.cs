using Main.Services;

namespace Main.Core.Factory
{
    public interface IFactoryService
    {
        IArticleService getInstanceArticleService();
        IClientService getInstanceClientService();
        IDemandeArticleService getInstanceDemandeArticleService();
        IDemandeDetteService getInstanceDemandeDetteService();
        IDetailService getInstanceDetailService();
        IDetteService getInstanceDetteService();
        IPaiementService getInstancePaiementService();
        IUserService getInstanceUserService();
    }
}