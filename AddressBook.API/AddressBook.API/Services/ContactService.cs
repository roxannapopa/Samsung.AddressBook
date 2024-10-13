using AddressBook.API.Models;
using System.Text.Json;
using System.IO.Abstractions;

namespace AddressBook.API.Services
{
    public class ContactService : IContactService
    {
        private readonly string _jsonFilePath = "Data/contacts.json";
        private readonly ILogger<ContactService> _logger;
        private readonly IFileSystem _fileSystem;  

        public ContactService(ILogger<ContactService> logger, 
            IFileSystem fileSystem)
        {
            _logger = logger;
            _fileSystem = fileSystem;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            try
            {
                if (_fileSystem.File.Exists(_jsonFilePath))
                {
                    var jsonData = await _fileSystem.File.ReadAllTextAsync(_jsonFilePath);

                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        return JsonSerializer.Deserialize<List<Contact>>(jsonData)!;
                    }
                    else
                    {
                        _logger.LogWarning("Contact data file is empty.");
                    }
                }
                else
                {
                    _logger.LogWarning($"Contact data file not found at path: {_jsonFilePath}.");
                }
            }
            catch (JsonException jsonEx)
            {                
                _logger.LogError(jsonEx, "Error deserializing the contact data.");
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, $"An error occurred while accessing file: {_jsonFilePath}.");
            }

            return new List<Contact>();
        }
    }
}
