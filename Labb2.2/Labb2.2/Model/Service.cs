using Labb2._2.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb2._2.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        public void saveContact(Contact contact){

            if(contact.ContactID==0){
                ContactDAL.InsertContact(contact);
            }
            else
            {

            }

        }

        public IEnumerable<Contact> getContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }
        public Contact GetContact(int ContactId)
        {
            return ContactDAL.GetContactById(ContactId);
        }

    }
}