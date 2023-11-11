using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsAgentsContactService.Api.Dto;
using SportsAgentsContactService.Application.Interfaces;
using SportsAgentsContactService.Domain.Entities;

namespace SportsAgentsContactService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    // Vars
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    // Constructor
    public ContactController(IContactService contactService, IMapper mapper)
    {
        _contactService = contactService;
        _mapper = mapper;
    }

    // GET: api/contact
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        var contactsDto = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contacts);
        return Ok(contactsDto);
    }

    // GET: api/contact/3
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        var contactDto = _mapper.Map<Contact, ContactDto>(contact!);
        return Ok(contactDto);
    }

    // POST: api/contact
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactDto contactDto)
    {
        var contact = _mapper.Map<ContactDto, Contact>(contactDto);
        await _contactService.AddContactAsync(contact);
        return Ok();
    }
    
    // PUT: api/contact/3
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ContactDto contactDto)
    {
        var contact = _mapper.Map<ContactDto, Contact>(contactDto);
        contact.Id = id;
        var contactUpdated = await _contactService.UpdateContactAsync(contact);
        var contactUpdatedDto = _mapper.Map<Contact, ContactDto>(contactUpdated!);
        return Ok(contactUpdatedDto);
    }
    
    // DELETE: api/contact/3
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteContactAsync(id);
        return Ok();
    }
}