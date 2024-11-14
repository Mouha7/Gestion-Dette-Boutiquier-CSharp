using System.ComponentModel.DataAnnotations;

namespace gestion_dette_web.Models;

public class Detail
{
    public int DetteId { get; set; }
    public Dette Dette { get; set; }

    public int ArticleId { get; set; }
    public Article Article { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être au moins de 1")]
    public int Qte { get; set; }

    public float Montant { get; set; }

    public float PrixTotal => Article.Prix * Qte;
}
