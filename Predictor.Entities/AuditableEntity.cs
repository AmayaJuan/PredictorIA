using System.Data;

namespace Predictor.Entities;

// Base class with the primary key and audit fields shared by all entities
public abstract class AuditableEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
}
