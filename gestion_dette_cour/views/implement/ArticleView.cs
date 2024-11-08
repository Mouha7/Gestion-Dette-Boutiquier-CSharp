using Main.Data.Entities;
using Main.Services;

namespace Main.Views.Implement
{
    public class ArticleView : ImpView<Article>, IArticleView {
    private IArticleService articleService;

    public ArticleView(IArticleService articleService) {
        this.articleService = articleService;
    }
    
    public override Article saisir() {
        Article article = new Article();
        Console.WriteLine("Entrez le libelle de l'article : ");
        article.libelle = Console.ReadLine();
        article.prix = double.Parse(check("Entrez le prix de l'article : ", "le prix").ToString());
        article.qteStock = check("Entrez la quantité de l'article : ", "la quantité");
        return article;
    }
    
    public int check(string msg, string msgError) {
        string? value;
        string error = "Erreur, ";
        bool valid = false; // Pour contrôler la boucle
        int intValue = 0;
    
        do {
            Console.WriteLine(msg);
            value = Console.ReadLine();
    
            try {
                intValue = int.Parse(value); // Essaie de convertir la valeur en entier
                if (intValue == 0) {
                    Console.WriteLine("Erreur, la valeur ne peut pas être 0.");
                } else if (intValue < 0) {
                    Console.WriteLine(error + msgError + " ne peut être négatif.");
                } else if (!isDecimal(value) && msgError.Contains("prix")) {
                    Console.WriteLine(error + msgError + " doit être un nombre décimal.");
                } else if (!isInteger(value) && msgError.Contains("quantité")) {
                    Console.WriteLine(error + msgError + " doit être un nombre entier.");
                } else {
                    valid = true; // Tout est correct, sortir de la boucle
                }
            } catch (FormatException e) {
                Console.Error.WriteLine($"Erreur, veuillez entrer un nombre valide: {e.Message}");
            }
    
        } while (!valid);
    
        return intValue;
    }
    
    public override Article getObject(List<Article> articles) {
        Article article;
        string? choix;
        int count = articles.Count;
        this.afficher(articles);
        do {
            article = new Article();
            Console.WriteLine("Choisissez un article par son id: ");
            choix = Console.ReadLine();
            if (isInteger(choix)) {
                article.idArticle = int.Parse(choix);
                article = articleService.findBy(article, articles);
            } else {
                continue;
            }
            Console.WriteLine(article);
            if (article == null || int.Parse(choix) > count) {
                Console.WriteLine("Erreur, l'id de l'article est invalide.");
            }
        } while (article == null);
        return article;
    }
    }
}