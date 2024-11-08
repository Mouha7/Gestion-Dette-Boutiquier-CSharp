using Main.Data.Entities;

namespace Main.Services
{
    public interface IDetailService
    {
        void add(Detail value);
        List<Detail> findAll();
        int length();
    }
}