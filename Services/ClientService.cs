using gestion_dette_web.Models;
using gestion_dette_web.Data;

namespace gestion_dette_web.services
{
    public class ClientService : ServiceEF<Client>, IClientService
    {
        public ClientService(ApplicationDbContext context) : base(context)
        {
        }
    }
}