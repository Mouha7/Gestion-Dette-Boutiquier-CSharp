using gestion_dette_web.Models;

namespace gestion_dette_web.services;

public interface IClientService : IService<Client>
{
    List<Client> GetClients(int page, int pageSize);
    int GetTotalClients();
}