using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class PaiementService : IPaiementService {
    private IPaiementRepository paiementRepository;

    public PaiementService(IPaiementRepository paiementRepository) {
        this.paiementRepository = paiementRepository;
    }

    public void add(Paiement value) {
        paiementRepository.insert(value);
    }

    public List<Paiement> findAll() {
        return paiementRepository.selectAll();
    }

    public Paiement? findBy(List<Paiement> paiements, Paiement paiement) {
        foreach (Paiement pay in paiements) {
            if (pay.idPaiement == paiement.idPaiement) {
                return pay;
            }
        }
        return null;
    }

    public int length() {
        return paiementRepository.size();
    }
    
}

}