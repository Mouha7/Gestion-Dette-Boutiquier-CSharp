using Main.Core.Repository.Implement;
using Main.Data.Entities;

namespace Main.Data.Repositories.Implement
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public List<Client> selectAllActifs()
        {
            return selectAll().Where(cl => cl.status).ToList();
        }

        public void changeStatus(Client client, Boolean state)
        {
            Client cl = selectBy(client);
            if (cl == null)
            {
                Console.Error.WriteLine($"Client non trouvé.");
            }
            else
            {
                cl.status = state;
            }
        }

        public List<Client> selectAllCustomerAvailable()
        {
            return selectAll().Where(cl => cl.user == null)
                    .ToList();
        }
    }
}