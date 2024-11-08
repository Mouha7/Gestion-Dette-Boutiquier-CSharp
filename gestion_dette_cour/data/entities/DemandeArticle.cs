namespace Main.Data.Entities
{
    public class DemandeArticle
    {
        public int idDemandeArticle { get; set; }
        public int qteArticle { get; set; }
        public static int nbr = 0;

        // Nav
        public Article? article { get; set; }
        public DemandeDette? demandeDette { get; set; }

        public DemandeArticle()
        {
            this.idDemandeArticle = ++nbr;
        }
    }
}