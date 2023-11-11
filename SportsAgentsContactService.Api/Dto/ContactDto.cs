namespace SportsAgentsContactService.Api.Dto;

public class ContactDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null;
    public string Email { get; set; } = null;
    public string Message { get; set; } = null;
}