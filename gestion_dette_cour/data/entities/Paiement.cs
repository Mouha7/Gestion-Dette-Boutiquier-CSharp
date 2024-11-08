namespace Main.Data.Entities
{
    public class Paiement
    {
        public int idPaiement { get; set; }
        public DateTime datePaiement { get; set; }
        public Double montantPaye { get; set; }
        public static int nbr = 0;

        // Nav
        public Dette? dette { get; set; }

        public Paiement()
        {
            this.idPaiement = ++nbr;
            this.datePaiement = DateTime.Now;
        }
    }
}