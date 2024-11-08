using Main.Core.Repository;

namespace Main.Core.Repository.Implement
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly List<T> list = new List<T>();

        public List<T> selectAll()
        {
            return list;
        }

        public void insert(T item)
        {
            list.Add(item);
        }

        public T selectBy(T value)
        {
            return selectAll()
                    .FirstOrDefault(cl => cl.Equals(value));
        }

        public int size()
        {
            return list.Count;
        }
    }
}