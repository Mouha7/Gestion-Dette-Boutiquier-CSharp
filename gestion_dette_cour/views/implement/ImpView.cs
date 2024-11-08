namespace Main.Views.Implement
{
    public abstract class ImpView<T> : IView<T>
    {

        public void afficher(List<T> list)
        {
            list.ForEach(item => Console.WriteLine(item?.ToString()));
        }

        protected bool isInteger(string number)
        {
            try
            {
                int.Parse(number);
                return true;
            }
            catch (FormatException e)
            {
                Console.Error.WriteLine($"Erreur lors de la conversion en entier: {e.Message}");
                return false;
            }
        }

        protected bool isDecimal(string number)
        {
            try
            {
                double.Parse(number);
                return true;
            }
            catch (FormatException e)
            {
                Console.Error.WriteLine($"Erreur lors de la conversion en décimal: {e.Message}");
                return false;
            }
        }

        public void motif(string letter)
        {
            motif(letter, 64);
        }

        public void motif(string letter, int nbr)
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat(letter, nbr)));
        }

        public abstract T saisir();
        public abstract T getObject(List<T> list);
    }
}