using System.ComponentModel.DataAnnotations;

namespace gestion_dette_web.Models;

public class Paiement
{
    public int Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Range(1, float.MaxValue, ErrorMessage = "Seul un montant positif est autorisé")]
    public float Montant { get; set; }
    // Relation
    public Dette Dette { get; set; }
    public int DetteId { get; set; }
}