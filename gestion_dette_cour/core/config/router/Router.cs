using Main.Core.Config.Security;
using Main.Core.Factory;
using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;
using Main.Views;
using Main.Views.Admin;
using Main.Views.Admin.Implement;
using Main.Views.Clients;
using Main.Views.Clients.Implement;
using Main.Views.Store;
using Main.Views.Store.Implement;

namespace Main.Core.Config.Router
{
    public class Router : IRouter {
    private IArticleService articleService;
    private IArticleView articleView;
    private IClientService clientService;
    private IClientView clientView;
    private IDemandeDetteService demandeDetteService;
    private IDemandeDetteView demandeDetteView;
    private IDemandeArticleService demandeArticleService;
    private IDetailService detailService;
    private IDetteService detteService;
    private IDetteView detteView;
    private IPaiementService paiementService;
    private IPaiementView paiementView;
    private IUserService userService;
    private IUserView userView;

    private IApplicationAdmin appAdmin;
    private IApplicationClient appClient;
    private IApplicationStorekeeper appStorekeeper;
    private IConnexion conn;

    public Router(IFactory factory) {
        this.articleService = factory.getFactoryService().getInstanceArticleService();
        this.articleView = factory.getFactoryView().getInstanceArticleView();
        this.clientService = factory.getFactoryService().getInstanceClientService();
        this.clientView = factory.getFactoryView().getInstanceClientView();
        this.demandeDetteService = factory.getFactoryService().getInstanceDemandeDetteService();
        this.demandeDetteView = factory.getFactoryView().getInstanceDemandeDetteView();
        this.demandeArticleService = factory.getFactoryService().getInstanceDemandeArticleService();
        this.detailService = factory.getFactoryService().getInstanceDetailService();
        this.detteService = factory.getFactoryService().getInstanceDetteService();
        this.detteView = factory.getFactoryView().getInstanceDetteView();
        this.paiementService = factory.getFactoryService().getInstancePaiementService();
        this.paiementView = factory.getFactoryView().getInstancePaiementView();
        this.userService = factory.getFactoryService().getInstanceUserService();
        this.userView = factory.getFactoryView().getInstanceUserView();

        this.appAdmin = new ApplicationAdmin(this.articleService, this.articleView, this.clientService, this.clientView, this.detteService, this.detteView, this.userService, this.userView);
        this.appClient = new ApplicationClient(this.articleService, clientService, this.demandeDetteService, this.demandeDetteView, this.demandeArticleService, this.detteService, this.detteView);
        this.appStorekeeper = new ApplicationStorekeeper(this.articleService, this.clientService, this.clientView, this.demandeDetteService, this.demandeDetteView, this.detailService, this.detteService, this.detteView, this.paiementService, this.paiementView, userService, userView);
        this.conn = new Connexion(this.userService);
    }

    public void navigate() {
        User user;
        do {
            user = conn.connexion();
            switch (user.role) {
                case Role.ADMIN:
                    appAdmin.run(user); 
                    break;
                case Role.CLIENT:
                    appClient.run(user);
                    break;
                case Role.BOUTIQUIER:
                    appStorekeeper.run(user);
                    break;
                default:
                    break;
            }
        } while (user != null);
    }
}
}