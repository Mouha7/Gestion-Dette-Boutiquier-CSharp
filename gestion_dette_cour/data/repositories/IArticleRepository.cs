using Main.Data.Entities;

namespace Main.Data.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        void updateQte(Article article, int newQte);
        List<Article> selectAllAvailable();
    }
}