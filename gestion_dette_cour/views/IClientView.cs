using Main.Data.Entities;
using Main.Services;

namespace Main.Views
{
    public interface IClientView : IView<Client>
    {
        void display(List<Client> clients);
        void displayClient(Client client);
        Client? saisir(IClientService clientService);
    }
}