using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Labb2._2.Model.DAL
{
    public class ContactDAL : DALBase
    {




        public IEnumerable<Contact> GetContacts()
        {
            List<Contact> ListOfContacts = new List<Contact>();

            using(var conn = CreateConnection()){

                SqlCommand myCommand = new SqlCommand("Person.uspGetContacts", conn);
                myCommand.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using(var reader = myCommand.ExecuteReader())
                {
                    var IContactID = reader.GetOrdinal("ContactID"); // Hämtar ner index..
                    var IFirstName = reader.GetOrdinal("FirstName");
                    var ILastName = reader.GetOrdinal("LastName");
                    var IEmailAddress = reader.GetOrdinal("EmailAddress");

                    while (reader.Read()) // för varje gång Reader läser ett nytt objekt, implementera(?) det..
                    {
                        ListOfContacts.Add(new Contact
                        {
                            FirstName = reader.GetString(IFirstName),
                            LastName = reader.GetString(ILastName),
                            EmailAddress = reader.GetString(IEmailAddress),
                            ContactID = reader.GetInt32(IContactID)
                        });
                            
                            
                    }
                       
                }

            }
            return ListOfContacts;
            throw new NotImplementedException();
        }

        public Contact GetContactById(int contactId)
        {
            Contact contact = new Contact(); // instansierar kontaktobjektet..

            using(var connectionObj = CreateConnection()){ // påbörjar en connection till databasen..

                try
                {

                    var getContactCommand = new SqlCommand("Person.uspGetContact");
                    getContactCommand.CommandType = CommandType.StoredProcedure;
                    getContactCommand.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;
                    getContactCommand.Connection = connectionObj;


                    connectionObj.Open();
                    
                    using(var reader = getContactCommand.ExecuteReader())
                    {

                        var EmailAddress = reader.GetOrdinal("EmailAddress"); // hämtar ner Index
                        var FirstName = reader.GetOrdinal("FirstName");
                        var LastName = reader.GetOrdinal("LastName");

                        contact.EmailAddress = reader.GetString(EmailAddress); // använder Index för att hämta ner datan som sätts i kontakt objektet
                        contact.FirstName = reader.GetString(FirstName);
                        contact.LastName = reader.GetString(LastName);                        
                    }
                    connectionObj.Close();
                    
                }
                catch(Exception ex)
                {
                    throw new ArgumentException("Något gick fel vid hämtningen av Kontakten" + ex);
                }
                
            }

            return contact;
        }
       
    }
}