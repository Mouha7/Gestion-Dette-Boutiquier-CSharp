namespace Main.Core.Repository;
public interface IRepositoryDB<T>
{
    IEnumerable<T> selectAll();
    T? selectBy(int id);
    int insert(T entity);
    int Update(T entity);
    int Delete(int id);
}