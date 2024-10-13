using AddressBook.API.Models;

namespace AddressBook.API.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
    }
}
