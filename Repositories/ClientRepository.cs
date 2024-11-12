using gestion_dette_web.Models;
using gestion_dette_web.Data;

namespace gestion_dette_web.Repositories
{
    public class ClientRepository : RepositoryEF<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}