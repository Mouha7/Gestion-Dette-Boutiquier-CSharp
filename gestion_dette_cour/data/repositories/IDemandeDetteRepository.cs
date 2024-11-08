using Main.Data.Entities;

namespace Main.Data.Repositories
{
    public interface IDemandeDetteRepository : IRepository<DemandeDette>
    {
        void selectUpdate(DemandeDette demandeDette);
    }
}