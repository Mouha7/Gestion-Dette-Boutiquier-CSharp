using gestion_dette_cour_2.Entities;

namespace gestion_dette_cour_2.Services;

public interface IClientService
{
    IEnumerable<Client> SelectAll();
    Client SelectById(int id);
    void Insert(Client client);
    void Update(Client client);
    void Delete(int id);
}