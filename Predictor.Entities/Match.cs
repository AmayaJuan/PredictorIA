using Predictor.Entities.Enums;

namespace Predictor.Entities;

// A match between two teams; DrawOdds is null in basketball
public class Match : AuditableEntity
{
    public SportType Sport { get; set; }

    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public DateTime Date { get; set; }
    public MatchStatus Status { get; set; }

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }

    public decimal? HomeOdds { get; set; }
    public decimal? DrawOdds { get; set; }
    public decimal? AwayOdds { get; set; }
}