using Main.Data.Entities;
using Main.Services;
using Main.Views;

namespace Main.Views.Admin
{
    public interface IApplicationAdmin : IApplication
    {
        int role();
        int status();
        void msgStatus(bool state);
        void soldes(IDetteService detteService, IDetteView detteView);
        void updateQte(IArticleService articleService, IArticleView articleView);
        void listingArticleAvailable(IArticleService articleService, IArticleView articleView);
        void createArticle(IArticleService articleService, IArticleView articleView);
        void listingUserActifs(IUserService userService, IUserView userView, User userConnect);
        void activeDesactiveAccount(IUserService userService, IUserView userView, User userConnect);
        void createAccountCustomer(IClientService clientService, IClientView clientView, IUserService userService, IUserView userView);
    }
}