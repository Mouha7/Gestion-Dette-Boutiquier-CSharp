using Main.Data.Entities;

namespace Main.Services
{
    public interface IArticleService
    {
        void add(Article value);
        List<Article> findAll();
        void setQte(Article article, int newQte);
        List<Article> findAllAvailable();
        Article? findBy(Article article, List<Article> articles);
        int length();
        void update(Article article, int newQte);
    }
}