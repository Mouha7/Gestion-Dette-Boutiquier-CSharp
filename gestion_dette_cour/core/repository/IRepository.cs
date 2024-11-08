public interface IRepository<T> {
    List<T> selectAll();
    void insert(T item);
    T selectBy(T value);
    int size();
}