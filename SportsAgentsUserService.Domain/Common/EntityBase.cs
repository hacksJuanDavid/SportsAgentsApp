using System.ComponentModel.DataAnnotations;

namespace SportsAgentsUserService.Domain.Common;

public abstract class EntityBase
{
    [Key] public int Id { get; set; }
}