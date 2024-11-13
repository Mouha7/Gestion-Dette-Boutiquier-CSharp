namespace gestion_dette_cour_2.Core.Service;

public interface IService<T>
{
    IEnumerable<T> SelectAll();
    T SelectById(int id);
    void Insert(T value);
    void Update(T value);
    void Delete(int id);
}