﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Labb2._2.Model.DAL
{
    public class ContactDAL : DALBase
    {


        public void DeleteContact(int contactId)
        {
            using(var conn = CreateConnection()){
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception)
                {                                  
                    throw new ArgumentException("Något fel inträffade då en kontakt skulle tas bort" );
                }
            }
        }

        public void InsertContact(Contact contact)
        {
            using(var conn = CreateConnection()){

                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspAddContact",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 50).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    contact.ContactID = (int)cmd.Parameters["@ContactID"].Value;

                    conn.Close();


                }
                catch(Exception)
                {
                    throw new ArgumentException("kanske ska ha en CustomValidator här istället..");
                }

            }
        }

        public void UpdateContact(Contact contact)
        {
            using(var conn = CreateConnection()){
                try{
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactID;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch(Exception)
                {
                    throw new ArgumentException("Något fel hände vid uppdatering av kontakten..");
                }
            }
        }
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            var contactListPageWise = new List<Contact>();
                             
            using (var conn = CreateConnection())
            {
                try {

                    SqlCommand sqlCommand = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = (startRowIndex/maximumRows) + 1;//Page index tänker att 1= sida1, 2 = sida2 osv! 
                                                                                        //MaximumRows är värdet vi satt att i vår datapager (PageSize=15)
                                                                                        //StartRowIndex är ALLA poster som finns i tabellen, från och med första sidan, tillochmed den sidan man står på
                                                                                        //Eftersom ALLA sidor har 15 kontakter, så borde det gå att få fram Vilken sida man bör vara på genom att ta
                                                                                        //StartRowIndex / MaximumRows = PageIndex.. <--problem = när denna exekveras så är värdet på StartRowIndex 0... 
                                                                                        //LÖSNING! +1 efteråt, om startRowIndex/maximumRows = Noll så blir det alltid + 1..
                                                                                        //Detta fungerar då MaximumRows är 0 på sida 1 och är 15 på sida 2
                    sqlCommand.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;


                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                    totalRowCount = (int)sqlCommand.Parameters["@RecordCount"].Value; // Denna krävs för att satsen ska fungera, dock vet jag inte riktigt varför.. bara typ..
                    using (var reader = sqlCommand.ExecuteReader())
                    {

                        var indexContactID = reader.GetOrdinal("ContactID");
                        var indexName = reader.GetOrdinal("FirstName");
                        var indexLastName = reader.GetOrdinal("LastName");
                        var indexEmail = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contactListPageWise.Add(new Contact
                            {
                                ContactID = reader.GetInt32(indexContactID),
                                LastName = reader.GetString(indexLastName),
                                FirstName = reader.GetString(indexName),
                                EmailAddress = reader.GetString(indexEmail)
                            });
                        }


                    }
                }
                catch
                {
                    throw new ArgumentException("något blev fel när data hämtades..");
                    // kanske ska ha en CustomValidator här?...
                }
            }
            
            return contactListPageWise;
        }

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

                    var getContactCommand = new SqlCommand("Person.uspGetContact",connectionObj);
                    getContactCommand.CommandType = CommandType.StoredProcedure;
                    getContactCommand.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;
                    


                    connectionObj.Open();

                    using(var reader = getContactCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Utan denna fungerar det tydligen inte, lärt mig det den hårda vägen.. 
                        {
                            var IEmailAddress = reader.GetOrdinal("EmailAddress"); // hämtar ner Index
                            var IFirstName = reader.GetOrdinal("FirstName");
                            var ILastName = reader.GetOrdinal("LastName");
                            var IContactID = reader.GetOrdinal("ContactID");

                            contact.FirstName = reader.GetString(IFirstName);
                            contact.LastName = reader.GetString(ILastName);
                            contact.ContactID = reader.GetInt32(IContactID);
                            contact.EmailAddress = reader.GetString(IEmailAddress); // använder Index för att hämta ner datan som sätts i kontakt objektet
                        }

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