using Main.Data.Enums;

namespace Main.Data.Entities
{
    public class Dette
    {
        public int idDette { get; set; }
        public Double montantTotal { get; set; }
        public Double montantVerser { get; set; }
        public Boolean status { get; set; }
        public EtatDette etat { get; set; }
        public DateTime dateCreation { get; set; }
        public static int nbr = 0;

        // Nav
        public Client? client { get; set; }
        public DemandeDette? demandeDette { get; set; }
        public List<Paiement> paiements { get; set; } = new List<Paiement>();
        public List<Detail> details { get; set; } = new List<Detail>();

        public Dette()
        {
            this.idDette = ++nbr;
            this.dateCreation = DateTime.Now;
            this.montantTotal = 0.0;
            this.montantVerser = 0.0;
        }

        public void addPaiement(Paiement paiement)
        {
            paiements.Add(paiement);
        }

        public void addDetail(Detail detail)
        {
            details.Add(detail);
        }

        public Double getMontantRestant()
        {
            return this.montantTotal - this.montantVerser;
        }
    }
}