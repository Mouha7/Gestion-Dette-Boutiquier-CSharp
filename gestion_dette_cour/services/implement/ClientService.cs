using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class ClientService : IClientService {
    private IClientRepository clientRepository;

    public ClientService(IClientRepository clientRepository) {
        this.clientRepository = clientRepository;
    }

    public void add(Client client) {
        clientRepository.insert(client);
    }

    public List<Client> findAll() {
        return clientRepository.selectAll();
    }

    public List<Client> findAllCustomerAvailable() {
        return clientRepository.selectAllCustomerAvailable();
    }

    public int length() {
        return clientRepository.size();
    }

    public Client? findBy(Client client) {
        foreach (Client cl in findAll()) {
            if(cl.user != null && cl.user.idUser == client.user.idUser) {
                return cl;
            }
            if (cl.tel != null && cl.tel == client.tel) {
                return cl;
            }
        }
        return null;
    }

    public Client? findBy(List<Client> clients, Client client) {
        foreach (Client cl in clients) {
            if (cl.idClient == client.idClient) {
                return cl;
            }
            if(client.user != null && cl.user.idUser == client.user.idUser) {
                return cl;
            }
            if (client.tel != null && cl.tel == client.tel) {
                return cl;
            }
            if (client.surname != null && cl.surname == client.surname) {
                return cl;
            }
            if (client.tel != null && cl.tel == client.tel) {
                return cl;
            }
        }
        return null;
    }

    public void setStatus(Client client, Boolean state) {
        clientRepository.changeStatus(client, state);
    }

    public List<Client> getAllActifs() {
        return clientRepository.selectAllActifs();
    }

    public void update(List<Client> clients, Client updateClient) {
        for (int i = 0; i < clients.Count; i++) {
            if (clients[i].idClient == updateClient.idClient) {
                clients[i] = updateClient;
                break; // Sortir de la boucle une fois que la mise à jour est effectuée
            }
        }
    }
}

}