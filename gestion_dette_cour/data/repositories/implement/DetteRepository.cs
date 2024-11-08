using Main.Core.Repository.Implement;
using Main.Data.Entities;

namespace Main.Data.Repositories.Implement
{
    public class DetteRepository : Repository<Dette>, IDetteRepository
    {
        public void changeStatus(Dette dette, Boolean state)
        {
            Dette du = selectBy(dette);
            if (du != null)
            {
                du.status = state;
            }
            else
            {
                Console.Error.WriteLine($"Dette non trouvé.");
            }
        }

        public List<Dette> selectAllSoldes()
        {
            return selectAll().Where(dette => dette.getMontantRestant() == 0)
                    .ToList();
        }

        public List<Dette> selectAllNonSoldes()
        {
            return selectAll().Where(dette => dette.getMontantRestant() != 0)
                    .ToList();
        }
    }
}