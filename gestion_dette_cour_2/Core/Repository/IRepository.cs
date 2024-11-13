namespace gestion_dette_cour_2.Core.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        int Insert(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}