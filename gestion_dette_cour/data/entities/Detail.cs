namespace Main.Data.Entities
{
    public class Detail
    {
        public int idDetteArticle { get; set; }
        public int qte { get; set; }
        public Double prixVente { get; set; }
        public static int nbr = 0;

        // Nav
        public Article? article { get; set; }
        public Dette? dette { get; set; }

        public Detail()
        {
            this.idDetteArticle = ++nbr;
            this.qte = 0;
            this.prixVente = 0.0;
        }
    }
}