using SportsAgentsContactService.Domain.Entities;

namespace SportsAgentsContactService.Application.Interfaces;

public interface IContactService
{
    // Add contact
    public Task<Contact> AddContactAsync(Contact contact);
    
    // Get by id
    public Task<Contact?> GetContactByIdAsync(int id);
    
    // Get all contacts
    public Task<IEnumerable<Contact>> GetAllContactsAsync();
    
    // Update contact
    public Task<Contact?> UpdateContactAsync(Contact? contact);
    
    // Delete contact
    public Task<Contact?> DeleteContactAsync(int id);
}