using Main.Data.Entities;
using Main.Services;

namespace Main.Views
{
    public interface IUserView : IView<User>
    {
        User accountCustomer(IUserService userService, Client client);
        int typeRole();
        User saisir(IUserService userService);
    }
}