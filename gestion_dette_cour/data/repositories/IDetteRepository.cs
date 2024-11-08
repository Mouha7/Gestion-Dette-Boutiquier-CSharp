using Main.Data.Entities;

namespace Main.Data.Repositories
{
    public interface IDetteRepository : IRepository<Dette>
    {
        void changeStatus(Dette dette, Boolean state);
        List<Dette> selectAllSoldes();
        List<Dette> selectAllNonSoldes();
    }
}