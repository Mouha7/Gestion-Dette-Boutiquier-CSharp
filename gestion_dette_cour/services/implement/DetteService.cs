using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class DetteService : IDetteService {
    private IDetteRepository detteRepository;

    public DetteService(IDetteRepository detteRepository) {
        this.detteRepository = detteRepository;
    }
    public void add(Dette value) {
        detteRepository.insert(value);
    }
    public List<Dette> findAll() {
        return detteRepository.selectAll();
    }
    public int length() {
        return detteRepository.size();
    }
    public Dette? findBy(Dette dette) {
        return detteRepository.selectBy(dette);
    }
    public Dette? findBy(List<Dette> dettes, Dette dette) {
        foreach (Dette d in dettes) {
            if (d.idDette == dette.idDette) {
                return d;
            }
        }
        return null;
    }
    public void setStatus(Dette dette, bool state) {
        detteRepository.changeStatus(dette, state);
    }
    public List<Dette> getAllSoldes() {
        return detteRepository.selectAllSoldes();
    }
    public List<Dette> getAllNonSoldes() {
        return detteRepository.selectAllNonSoldes();
    }
    public void update(List<Dette> dettes, Dette updatedDette) {
        for (int i = 0; i < dettes.Count; i++) {
            if (dettes[i].idDette == updatedDette.idDette) {
                dettes[i] = updatedDette;
                break; // Sortir de la boucle une fois que la mise à jour est effectuée
            }
        }
    }
}
}