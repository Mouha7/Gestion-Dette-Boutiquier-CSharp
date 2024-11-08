using Main.Data.Entities;

namespace Main.Services
{
    public interface IDetteService
    {
        void add(Dette value);
        List<Dette> findAll();
        int length();
        Dette? findBy(Dette dette);
        Dette? findBy(List<Dette> dettes, Dette dette);
        void setStatus(Dette dette, Boolean state);
        List<Dette> getAllSoldes();
        List<Dette> getAllNonSoldes();
        void update(List<Dette> dettes, Dette dette);
    }
}