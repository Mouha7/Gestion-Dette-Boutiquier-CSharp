using Main.Data.Entities;

namespace Main.Services
{

    public interface IDemandeDetteService
    {
        void add(DemandeDette value);
        List<DemandeDette> findAll();
        DemandeDette? findBy(DemandeDette demandeDette);
        int length();
        void update(List<DemandeDette> demandeDettes, DemandeDette updateDemande);
        DemandeDette? findBy(List<DemandeDette> demandeDettes, DemandeDette demandeDette);
    }
}