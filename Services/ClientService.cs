using gestion_dette_web.Models;
using gestion_dette_web.Data;

namespace gestion_dette_web.services
{
    public class ClientService : ServiceEF<Client>, IClientService
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Client> GetClients(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _dbContext.Clients
                .OrderBy(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalClients()
        {
            return _dbContext.Clients.Count();
        }
    }
}