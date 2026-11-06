using Predictor.Entities.Enums;

namespace Predictor.Entities;

// A football or basketball team
public class Team : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public SportType Sport { get; set; }
}