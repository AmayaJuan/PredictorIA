namespace Predictor.Entities;

// Probabilities calculated for a match by a specific model version
public class Prediction : AuditableEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public string ModelVersion { get; set; } = string.Empty;

    public decimal ProbHome { get; set; }
    public decimal? ProbDraw { get; set; }
    public decimal? ProbAway { get; set; }
}