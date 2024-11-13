using gestion_dette_cour_2.Entities;
using gestion_dette_cour_2.Repositories;

namespace gestion_dette_cour_2.Services
{
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Client> SelectAll()
        {
            return _clientRepository.GetAll();
        }

        public Client SelectById(int id)
        {
            return _clientRepository.GetById(id)!;
        }

        public void Insert(Client client)
        {
            _clientRepository.Insert(client);
        }

        public void Update(Client client)
        {
            _clientRepository.Update(client);
        }

        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }
    }
}
