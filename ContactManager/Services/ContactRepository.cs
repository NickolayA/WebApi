using ContactManager.Models;
using System;
using System.Linq;
using System.Web;

namespace ContactManager.Services
{
    public class ContactRepository : IContactRepository
    {
        private const string CacheKey = "ContactStore";

        public ContactRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[]
                    {
                        new Contact
                        {
                            Id = 1, Name = "Glenn Block"
                        },
                        new Contact
                        {
                            Id = 2, Name = "Dan Roth"
                        }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }

        public Contact[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Contact[])ctx.Cache[CacheKey];
            }

            return new Contact[]
            {
                new Contact
                {
                    Id = 0,
                    Name = "Placeholder"
                }
            };
        }

        public bool SaveContact(Contact contact)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = GetCurrentData(ctx).ToList();
                    currentData.Add(contact);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        public bool UpdateContact(Contact contact)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                var currentData = GetCurrentData(ctx);
                var filteredData = currentData.Where(d => d.Id != contact.Id).ToList();

                filteredData.Add(contact);

                try
                {
                    ctx.Cache[CacheKey] = filteredData.ToArray();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public bool DeleteContact(int id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                var currentData = GetCurrentData(ctx).ToList();

                try
                {
                    ctx.Cache[CacheKey] = currentData.Where(d => d.Id != id).ToArray();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        private Contact[] GetCurrentData(HttpContext ctx)
        {
            if (ctx != null)
            {
                try
                {
                    return (Contact[])ctx.Cache[CacheKey];
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

            return null;
        }
    }
}