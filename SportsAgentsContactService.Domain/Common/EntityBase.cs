using System.ComponentModel.DataAnnotations;

namespace SportsAgentsContactService.Domain.Common;

public abstract class EntityBase
{
    [Key] public int Id { get; set; }
}