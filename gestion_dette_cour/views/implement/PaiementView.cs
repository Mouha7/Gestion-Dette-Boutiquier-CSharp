using Main.Data.Entities;
using Main.Services;

namespace Main.Views.Implement
{
    public class PaiementView : ImpView<Paiement>, IPaiementView {
    private IPaiementService paiementService;

    public PaiementView(IPaiementService paiementService) {
        this.paiementService = paiementService;
    }

    public override Paiement saisir() {
        Paiement paiement = new Paiement();
        paiement.montantPaye = checkMontant("Entrer le montant du paiement: ");
        return paiement;
    }

    public override Paiement getObject(List<Paiement> list) {
        Paiement paiement;
        string choix;
        int count = list.Count;
        this.afficher(list);
        do {
            paiement = new Paiement();
            Console.Write("Choisissez une paiement par son id: ");
            choix = Console.ReadLine();
            if (isInteger(choix)) {
                paiement.idPaiement = int.Parse(choix);
                paiement = paiementService.findBy(paiementService.findAll(), paiement);
            } else {
                continue;
            }
            if (paiement == null || int.Parse(choix) > count) {
                Console.WriteLine("Erreur, l'id est invalide.");
            }

        } while (paiement == null);
        return paiement;
    }
    

    private double checkMontant(string msg) {
        string montant;
        do {
            Console.Write(msg);
            montant = Console.ReadLine();
            if (!isDecimal(montant)) {
                Console.WriteLine("Format incorrect, le montant doit être un nombre.");
            }
            if (double.Parse(montant) <= 0.0) {
                Console.WriteLine("Format incorrect, le montant doit positif.");
            }
        } while (!isDecimal(montant) && double.Parse(montant) <= 0.0);
        return double.Parse(montant);
    }
}
}