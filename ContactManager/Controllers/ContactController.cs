using ContactManager.Models;
using ContactManager.Services;
using System.Net.Http;
using System.Web.Http;

namespace ContactManager.Controllers
{
    public class ContactController : ApiController
    {
        private IContactRepository contactRepository;

        public ContactController()
        {
            this.contactRepository = new ContactRepository();
        }

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public Contact[] Get()
        {
            return this.contactRepository.GetAllContacts();
        }

        public HttpResponseMessage Post(Contact contact)
        {
            this.contactRepository.SaveContact(contact);

            var response = Request.CreateResponse<Contact>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }

        public HttpResponseMessage Put(Contact contact)
        {
            if (this.contactRepository.UpdateContact(contact))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (this.contactRepository.DeleteContact(id))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        }
    }
}