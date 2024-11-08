using Main.Data.Enums;

namespace Main.Data.Entities
{
    public class DemandeDette
    {
        public int idDemandeDette { get; set; }
        public DateTime dateDemande { get; set; }
        public Double montantTotal { get; set; }
        public EtatDemandeDette etat { get; set; }
        public static int nbr = 0;

        // Nav
        public List<DemandeArticle> demandeArticles { get; set; } = new List<DemandeArticle>();
        public Dette? dette { get; set; }
        public Client? client { get; set; }

        public void addDemandeArticle(DemandeArticle demandeArticle)
        {
            demandeArticles.Add(demandeArticle);
        }

        public DemandeDette()
        {
            this.idDemandeDette = ++nbr;
            this.dateDemande = DateTime.Now;
            this.montantTotal = 0.0;
        }
    }
}