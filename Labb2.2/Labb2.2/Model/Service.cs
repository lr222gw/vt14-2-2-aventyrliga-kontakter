using Labb2._2.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            ICollection<ValidationResult> validationResults; // skapar Icollection lista för valideringsresultat..
            if(!contact.Validate(out validationResults)){ // om kontakten inte validerar..
                var ex = new ValidationException("Kontakten klarade inte valideringen.."); // Skriv ett meddelande som presenterar felet
                ex.Data.Add("ValidationResults", validationResults); // lägger till datan från outparametern så att 
                throw ex; // kastar undantaget
            }
                if (contact.ContactID == 0)
                {
                    ContactDAL.InsertContact(contact);
                }
                else
                {
                    ContactDAL.UpdateContact(contact);
                }                        

        }
        public void DeleteContact(int contactID)
        {
            ContactDAL.DeleteContact(contactID);
        }
        public IEnumerable<Contact> getContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }
        public Contact GetContactById(int ContactId)
        {
            return ContactDAL.GetContactById(ContactId);
        }

    }
}