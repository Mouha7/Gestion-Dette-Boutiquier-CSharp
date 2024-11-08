using System;
using System.Collections.Generic;

namespace Main.Data.Entities
{
    public class Article
    {
        private static int nbr = 0;
        public int idArticle { get; set; }
        public string? libelle { get; set; }
        public double? prix { get; set; }
        public int? qteStock { get; set; }

        // Listes de navigation
        public List<Detail> details { get; set; } = new List<Detail>();
        public List<DemandeArticle> demandeArticles { get; set; } = new List<DemandeArticle>();

        public Article()
        {
            idArticle = ++nbr;
            details = new List<Detail>();
            demandeArticles = new List<DemandeArticle>();
        }

        public void AddDetail(Detail detail)
        {
            details.Add(detail);
        }

        public void AddDemandeArticle(DemandeArticle demandeArticle)
        {
            demandeArticles.Add(demandeArticle);
        }

        public override string ToString()
        {
            return $"Article [IdArticle={idArticle}, Libelle={libelle}, Prix={prix}, QteStock={qteStock}]";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Article article)
            {
                return idArticle == article.idArticle;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return idArticle.GetHashCode();
        }
    }
}
