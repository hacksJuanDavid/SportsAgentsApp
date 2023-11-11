using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportsAgentsContactService.Domain.Common;

namespace SportsAgentsContactService.Domain.Entities;

[Table("contacts")]
public class Contact : EntityBase
{
    [Required(ErrorMessage = "The username is required")]
    [StringLength(20, ErrorMessage = "The username must be less than 20 characters")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "The email is required")]
    [StringLength(50, ErrorMessage = "The email must be less than 50 characters")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "The message is required")]
    [StringLength(500, ErrorMessage = "The message must be less than 500 characters")]
    public string Message { get; set; } = null!;
}