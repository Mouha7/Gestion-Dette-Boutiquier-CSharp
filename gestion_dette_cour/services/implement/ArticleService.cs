using Main.Data.Entities;
using Main.Data.Repositories;

namespace Main.Services.Implement
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public void add(Article value)
        {
            articleRepository.insert(value);
        }

        public List<Article> findAll()
        {
            return articleRepository.selectAll();
        }

        public void setQte(Article article, int newQte)
        {
            articleRepository.updateQte(article, newQte);
        }

        public List<Article> findAllAvailable()
        {
            return articleRepository.selectAllAvailable();
        }

        public int length()
        {
            return articleRepository.size();
        }

        public Article? findBy(Article article, List<Article> articles)
        {
            foreach (Article value in articles)
            {
                if (value.idArticle == article.idArticle)
                {
                    return value;
                }
                if (article.libelle != null && value.libelle == article.libelle)
                {
                    return value;
                }
            }
            return null;
        }


        public void update(Article article, int newQte)
        {
            articleRepository.updateQte(article, newQte);
        }
    }
}