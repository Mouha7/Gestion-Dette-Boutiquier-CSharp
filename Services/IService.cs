namespace gestion_dette_web.services
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        int Insert(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}