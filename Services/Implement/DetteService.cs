using gestion_dette_web.Models;
using gestion_dette_web.Data;
using gestion_dette_web.core.service;
using gestion_dette_web.services;

namespace gestion_dette_web.implement.services
{
    public class DetteService : ServiceEF<Dette>, IDetteService
    {
        private readonly ApplicationDbContext _dbContext;
        public DetteService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Dette> GetDettes(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            try
            {
                return _dbContext.Dettes
                    .OrderBy(c => c.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Dette>();  // Retourne une liste vide en cas d'erreur
            }
        }

        public int GetTotalDettes()
        {
            try
            {
                return _context.Dettes.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}