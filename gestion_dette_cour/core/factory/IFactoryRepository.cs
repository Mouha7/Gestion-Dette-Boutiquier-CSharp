using Main.Data.Repositories;

namespace Main.Core.Factory
{
    public interface IFactoryRepository
    {
        IArticleRepository getInstanceArticleRepository();
        IClientRepository getInstanceClientRepository();
        IDemandeArticleRepository getInstanceDemandeArticleRepository();
        IDemandeDetteRepository getInstanceDemandeDetteRepository();
        IDetailRepository getInstanceDetailRepository();
        IDetteRepository getInstanceDetteRepository();
        IPaiementRepository getInstancePaiementRepository();
        IUserRepository getInstanceUserRepository();
    }
}