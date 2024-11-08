using Main.Data.Entities;

namespace Main.Services
{

    public interface IPaiementService
    {
        void add(Paiement value);
        List<Paiement> findAll();
        Paiement? findBy(List<Paiement> paiements, Paiement paiement);
        int length();
    }
}