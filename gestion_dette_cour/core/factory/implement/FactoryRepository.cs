using Main.Data.Repositories;
using Main.Data.Repositories.Implement;

namespace Main.Core.Factory.Implement
{
    public class FactoryRepository : IFactoryRepository {
    private IArticleRepository articleRepository;
    private IClientRepository clientRepository;
    private IDemandeArticleRepository demandeArticleRepository;
    private IDemandeDetteRepository demandeDetteRepository;
    private IDetailRepository detailRepository;
    private IDetteRepository detteRepository;
    private IPaiementRepository paiementRepository;
    private IUserRepository userRepository;

    public IArticleRepository getInstanceArticleRepository() {
        return articleRepository == null ? new ArticleRepository() : articleRepository;
    }

    public IDemandeArticleRepository getInstanceDemandeArticleRepository() {
        return demandeArticleRepository == null? new DemandeArticleRepository() : demandeArticleRepository;
    }
    
    public IClientRepository getInstanceClientRepository() {
        return clientRepository == null ? new ClientRepository() : clientRepository;
    }

    public IDetailRepository getInstanceDetailRepository() {
        return detailRepository == null ? new DetailRepository() : detailRepository;
    }
    
    public IDemandeDetteRepository getInstanceDemandeDetteRepository() {
        return demandeDetteRepository == null ? new DemandeDetteRepository() : demandeDetteRepository;
    }

    public IDetteRepository getInstanceDetteRepository() {
        return detteRepository == null ? new DetteRepository() : detteRepository;
    }

    public IPaiementRepository getInstancePaiementRepository() {
        return paiementRepository == null ? new PaiementRepository() : paiementRepository;
    }

    public IUserRepository getInstanceUserRepository() {
        if (userRepository == null) {
            userRepository = new UserRepository();
        }
        return userRepository;
    }
}
}