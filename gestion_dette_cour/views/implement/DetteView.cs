using Main.Data.Entities;
using Main.Data.Enums;
using Main.Services;

namespace Main.Views.Implement
{
    public class DetteView : ImpView<Dette>, IDetteView {
    private IDetteService detteService;

    public DetteView(IDetteService detteService) {
        this.detteService = detteService;
    }

    public override Dette saisir() {
        Dette dette = new Dette();
        dette.montantTotal= checkMontant("Entrez le montant total: ");
        dette.montantVerser = checkMontant("Entrez le montant verser: ");
        dette.status = true;
        dette.etat = EtatDette.ENCOURS;
        return dette;
    }

    public override Dette getObject(List<Dette> dettes) {
        Dette dette;
        String choix;
        int count = dettes.Count;
        this.display(dettes);
        do {
            dette = new Dette();
            Console.Write("Choisissez une dette par son id: ");
            choix = Console.ReadLine();
            if (isInteger(choix)) {
                dette.idDette = int.Parse(choix);
                dette = detteService.findBy(dettes, dette);
            } else {
                continue;
            }
            if (dette == null || int.Parse(choix) > count) {
                Console.WriteLine("Erreur, l'id est invalide.");
            }
        } while (dette == null);
        return dette;
    }
    
    private double checkMontant(String msg) {
        String montant;
        do {
            Console.Write(msg);
            montant = Console.ReadLine();
            if (!isDecimal(montant)) {
                Console.WriteLine("Format incorrect, le montant doit être un nombre.");
            }
        } while (!isDecimal(montant));
        return double.Parse(montant);
    }

    public void display(List<Dette> dettes) {
        motif("+");
        Console.WriteLine("Liste des dettes: ");
        motif("+");
        foreach (Dette dette in dettes) {
            displayDette(dette);
        }
    }

    public void displayDette(Dette dette) {
        Console.WriteLine("ID: " + dette.idDette);
        Console.WriteLine("Montant Total: " + dette.montantTotal);
        Console.WriteLine("Montant Verser: " + dette.montantVerser);
        Console.WriteLine("Montant Restant: " + dette.getMontantRestant());
        Console.WriteLine("Status: " + dette.status);
        Console.WriteLine("Etat: " + dette.etat);
        Console.WriteLine("Date de contraction: " + dette.dateCreation);
        Console.WriteLine("Client: " + dette.client.surname);
        Console.WriteLine("Demande de dette: " + (dette.demandeDette == null ? "N/A" : dette.demandeDette));
        motif("-");
        if (dette.paiements != null) {
            displayPay(dette);
        } else {
            Console.WriteLine("Pas de paiements pour cette dette.");
        }
        motif("-");
        if (dette.details != null) {
            displayDetail(dette);
        } else {
            Console.WriteLine("Pas d'articles pour cette dette.");
        }
        motif("+");
    }
    
    public void displayPay(Dette dette) {
        if (dette.paiements != null && dette.paiements.Count == 0) {
            Console.WriteLine("Pas de paiements pour cette dette.");
            return;
        }
        Console.WriteLine("---Paiements---");
        foreach (Paiement paiement in dette.paiements) {
            Console.WriteLine("  - Montant : " + paiement.montantPaye);
            Console.WriteLine("  - Date : " + paiement.datePaiement);
            motif("-");
        }
    }

    public void displayDetail(Dette dette) {
        if (dette.details != null && dette.details.Count == 0) {
            Console.WriteLine("Pas de détails pour cette dette.");
            return;
        }
        Console.WriteLine("---Articles---");
        foreach (Detail detail in dette.details) {
            Console.WriteLine("  - Article : " + detail.article.libelle);
            Console.WriteLine("  - Quantité : " + detail.qte);
            Console.WriteLine("  - Prix de vente : " + detail.prixVente);
            motif("-");
        }
    }
}
}