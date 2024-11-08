namespace Main.Data.Entities
{
    public class Client
    {
        public int idClient { get; set; }
        public String? surname { get; set; }
        public String? tel { get; set; }
        public String? address { get; set; }
        public Double cumulMontantDu { get; set; }
        public Boolean status { get; set; }
        public static int nbr = 0;

        // Navigabilité: revoir la pertinence de garder certain navigabilité 
        public User? user { get; set; }
        public List<DemandeDette> demandeDettes { get; set; } = new List<DemandeDette>();
        public List<Dette> dettes { get; set; } = new List<Dette>();

        public Client()
        {
            this.cumulMontantDu = 0.0;
            this.idClient = ++nbr;
        }

        public void addDemandeDette(DemandeDette demandeDette)
        {
            demandeDettes.Add(demandeDette);
        }

        public void addDetteClient(Dette dette)
        {
            dettes.Add(dette);
        }

        public Double getCumulMontantDu()
        {
            if (dettes != null && !(dettes.Count == 0))
            {
                foreach (Dette d in dettes)
                {
                    cumulMontantDu += d.getMontantRestant();
                }
                return cumulMontantDu;
            }
            return cumulMontantDu;
        }
    }
}