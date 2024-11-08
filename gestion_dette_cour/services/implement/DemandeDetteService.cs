using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class DemandeDetteService : IDemandeDetteService {
    private IDemandeDetteRepository demandeDetteRepository;

    public DemandeDetteService(IDemandeDetteRepository demandeDetteRepository) {
        this.demandeDetteRepository = demandeDetteRepository;
    }

    public void add(DemandeDette value) {
        demandeDetteRepository.insert(value);
    }

    public List<DemandeDette> findAll() {
        return demandeDetteRepository.selectAll();
    }

    public DemandeDette? findBy(DemandeDette demandeDette) {
        foreach (DemandeDette dette in demandeDetteRepository.selectAll()) {
            if (dette.idDemandeDette == demandeDette.idDemandeDette) {
                return dette;
            }
        }
        return null;
    }

    public DemandeDette? findBy(List<DemandeDette> demandeDettes,DemandeDette demandeDette) {
        foreach (DemandeDette dette in demandeDettes) {
            if (dette.idDemandeDette == demandeDette.idDemandeDette) {
                return dette;
            }
        }
        return null;
    }

    public int length() {
        return demandeDetteRepository.size();
    }

    public void update(List<DemandeDette> demandeDettes, DemandeDette updateDemande) {
        for (int i = 0; i < demandeDettes.Count; i++) {
            if (demandeDettes[i].idDemandeDette == updateDemande.idDemandeDette) {
                demandeDettes[i] = updateDemande;
                break; // Sortir de la boucle une fois que la mise à jour est effectuée
            }
        }
    }
}
}