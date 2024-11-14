using gestion_dette_web.Models;
using gestion_dette_web.Data;
using gestion_dette_web.core.service;
using gestion_dette_web.services;

namespace gestion_dette_web.implement.services
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
            try
            {
                int skip = (page - 1) * pageSize;
                return _dbContext.Clients
                    .OrderBy(c => c.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Client>();  // Retourne une liste vide en cas d'erreur
            }
        }

        public int GetTotalClients()
        {
            try
            {
                return _dbContext.Clients.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}