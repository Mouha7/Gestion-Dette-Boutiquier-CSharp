using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class DetailService : IDetailService {
    private IDetailRepository detailRepository;

    public DetailService(IDetailRepository detailRepository) {
        this.detailRepository = detailRepository;
    }

    public void add(Detail value) {
        detailRepository.insert(value);
    }

    public List<Detail> findAll() {
        return detailRepository.selectAll();
    }

    public int length() {
        return detailRepository.size();
    }
}
}