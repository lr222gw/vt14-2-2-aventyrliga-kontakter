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

        public Contact GetContactById(int contactId)
        {
            Contact contact = new Contact(); // instansierar kontaktobjektet..

            using(var connectionObj = CreateConnection()){ // påbörjar en connection till databasen..

                try
                {

                    var getContactCommand = new SqlCommand();
                    getContactCommand.CommandType = CommandType.StoredProcedure;
                    getContactCommand.Parameters.Add("@ContactID", SqlDbType.Int, 23).Value = contactId;
                    getContactCommand.Connection = connectionObj;

                    
                    
                    using(var reader = getContactCommand.ExecuteReader())
                    {

                        var EmailAdress = reader.GetOrdinal("EmailAdress"); // hämtar ner ******
                        var FirstName = reader.GetOrdinal("FirstName");
                        var LastName = reader.GetOrdinal("LastName");

                        contact.EmailAdress = reader.GetString(EmailAdress); // använder ***** för att hämta ner datan som sätts i kontakt objektet
                        contact.FirstName = reader.GetString(FirstName);
                        contact.LastName = reader.GetString(LastName);                        
                    }
                    
                }
                catch
                {
                    throw new ArgumentException("Något gick fel vid hämtningen av Kontakten");
                }
                
            }

            return contact;
            throw new NotImplementedException();
        }
       
    }
}