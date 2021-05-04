using ContactManager.Models;

namespace ContactManager.Services
{
    public interface IContactRepository
    {
        Contact[] GetAllContacts();

        bool SaveContact(Contact contact);

        bool UpdateContact(Contact contact);

        bool DeleteContact(int id);
    }
}
