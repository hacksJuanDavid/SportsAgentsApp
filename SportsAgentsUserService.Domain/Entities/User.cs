using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportsAgentsUserService.Domain.Common;

namespace SportsAgentsUserService.Domain.Entities;

[Table("users")]
public class User : EntityBase
{
    [Required(ErrorMessage = "The username is required")]
    [StringLength(50, ErrorMessage = "The username must be less than 50 characters")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "The email is required")]
    [StringLength(50, ErrorMessage = "The email must be less than 50 characters")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "The password is required")]
    [StringLength(20, ErrorMessage = "The password must be less than 20 characters")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "The role is required")]
    [StringLength(20, ErrorMessage = "The role must be less than 20 characters")]
    public string Role { get; set; } = null!;
}