using System.ComponentModel.DataAnnotations;

namespace gestion_dette_web.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string Libelle { get; set; }
    
    [Range(1, float.MaxValue, ErrorMessage = "Le prix doit être positif")]
    public float Prix { get; set; }

    // Relation many-to-many avec Dette via Detail
    public virtual ICollection<Detail>? Details { get; set; }
}
