using Main.Data.Entities;

namespace Main.Services
{
    public interface IClientService
    {
        void add(Client value);
        List<Client> findAll();
        int length();
        Client? findBy(Client client);
        Client? findBy(List<Client> clients, Client client);
        void setStatus(Client client, Boolean state);
        List<Client> getAllActifs();
        List<Client> findAllCustomerAvailable();
        void update(List<Client> clients, Client updateClient);
    }
}