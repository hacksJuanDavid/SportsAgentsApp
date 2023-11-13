using SportsAgentsContactService.Application.Interfaces;
using SportsAgentsContactService.Domain.Entities;
using SportsAgentsContactService.Domain.Exceptions;
using SportsAgentsContactService.Domain.Interfaces.Repositories;

namespace SportsAgentsContactService.Application.Services;

public class ContactService : IContactService
{
    // Vars
    private readonly IContactRepository _contactRepository;

    // Constructor
    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    // Methods
    // Add contact
    public async Task<Contact> AddContactAsync(Contact contact)
    {
        // Validation contact 
        if (contact == null)
        {
            throw new BadRequestException("Contact cannot be null");
        }

        // Validation contact Exists with Id
        var contactExists = await _contactRepository.GetByIdAsync(contact.Id);
        if (contactExists != null)
        {
            throw new BadRequestException($"Contact with Id={contact.Id} Already Exists");
        }

        // Add contact
        var contactCreated = await _contactRepository.AddAsync(contact);
        return contactCreated;
    }

    // Get contact by Id
    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        // Validation contact Exists with Id
        var contactExists = await _contactRepository.GetByIdAsync(id);
        if (contactExists == null)
        {
            throw new NotFoundException($"Contact with Id={id} Not found");
        }

        // Get contact
        var contact = await _contactRepository.GetByIdAsync(id);
        return contact;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        // Get all contacts
        var contacts = await _contactRepository.GetAllAsync();
        return contacts;
    }

    // Update contact
    public async Task<Contact?> UpdateContactAsync(Contact? contact)
    {
        // Validation contact 
        if (contact == null)
        {
            throw new BadRequestException("Contact cannot be null");
        }

        // Validation contact Exists with Id
        var contactExists = await _contactRepository.GetByIdAsync(contact.Id);
        if (contactExists == null)
        {
            throw new NotFoundException($"Contact with Id={contact.Id} Not found");
        }

        // Update contact
        var contactUpdated = await _contactRepository.UpdateAsync(contact);
        return contactUpdated;
    }

    // Delete contact
    public async Task<Contact?> DeleteContactAsync(int id)
    {
        // Validation contact Exists with Id
        var contactExists = await _contactRepository.GetByIdAsync(id);
        if (contactExists == null)
        {
            throw new NotFoundException($"Contact with Id={id} Not found");
        }

        // Delete contact
        var contactDeleted = await _contactRepository.DeleteAsync(contactExists);
        return contactDeleted;
    }
}