using Main.Core.Repository.Implement;
using Main.Data.Entities;

namespace Main.Data.Repositories.Implement
{
    public class DemandeDetteRepository : Repository<DemandeDette>, IDemandeDetteRepository
    {
        public void selectUpdate(DemandeDette demandeDette)
        {
            List<DemandeDette> allDemandes = selectAll();
            for (int i = 0; i < allDemandes.Count; i++)
            {
                DemandeDette currentDemandeDette = allDemandes[i];
                if (currentDemandeDette.Equals(demandeDette))
                {
                    currentDemandeDette = demandeDette;
                    break;
                }
            }
        }
    }
}