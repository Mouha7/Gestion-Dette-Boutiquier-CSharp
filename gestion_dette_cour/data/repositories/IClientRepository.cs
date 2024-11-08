using Main.Data.Entities;

namespace Main.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        List<Client> selectAllActifs();
        void changeStatus(Client client, Boolean state);
        List<Client> selectAllCustomerAvailable();
    }
}