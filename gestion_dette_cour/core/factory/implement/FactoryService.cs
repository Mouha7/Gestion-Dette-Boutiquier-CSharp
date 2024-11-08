using Main.Services;
using Main.Services.Implement;

namespace Main.Core.Factory.Implement
{
    public class FactoryService : IFactoryService {
    private IArticleService articleService;
    private IClientService clientService;
    private IDemandeArticleService demandeArticleService;
    private IDemandeDetteService demandeDetteService;
    private IDetailService detailService;
    private IDetteService detteService;
    private IPaiementService paiementService;
    private IUserService userService;
    private IFactoryRepository factoryRepository;

    public FactoryService(IFactoryRepository factoryRepository) {
        this.factoryRepository = factoryRepository;
    }

    public IArticleService getInstanceArticleService() {
        return articleService == null ? new ArticleService(factoryRepository.getInstanceArticleRepository()) : articleService;
    }

    public IClientService getInstanceClientService() {
        return clientService == null ? new ClientService(factoryRepository.getInstanceClientRepository()) : clientService;
    }

    public IDemandeArticleService getInstanceDemandeArticleService() {
        return demandeArticleService == null ? new DemandeArticleService(factoryRepository.getInstanceDemandeArticleRepository()) : demandeArticleService;
    }

    public IDemandeDetteService getInstanceDemandeDetteService() {
        return demandeDetteService == null ? new DemandeDetteService(factoryRepository.getInstanceDemandeDetteRepository()) : demandeDetteService;
    }

    public IDetailService getInstanceDetailService() {
        return detailService == null ? new DetailService(factoryRepository.getInstanceDetailRepository()) : detailService;
    }

    public IDetteService getInstanceDetteService() {
        return detteService == null ? new DetteService(factoryRepository.getInstanceDetteRepository()) : detteService;
    }

    public IPaiementService getInstancePaiementService() {
        return paiementService == null? new PaiementService(factoryRepository.getInstancePaiementRepository()) : paiementService;
    }

    public IUserService getInstanceUserService() {
        return userService == null ? new UserService(factoryRepository.getInstanceUserRepository()) : userService;
    }
}
}