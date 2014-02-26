using Labb2._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labb2._2
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Contact> ListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.getContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void ListView_InsertItem(Contact contact)
        {
            if(ModelState.IsValid){
                Service.saveContact(contact);
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListView_UpdateItem(int ContactID)
        {

            try
            {
                var contact = Service.GetContactById(ContactID); //med id't så hämtar vi ut en kontakt mha getContactById...

                if(contact == null){
                    //Skriv kod för att visa error..
                }
                if(TryUpdateModel(contact)){ //om kontakten är giltig
                    Service.saveContact(contact); // spara ner det uppdaterade i kontakten..
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException(" " + ex);
                //Skriv kod för att visa error..? 
            }
            
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListView_DeleteItem(int ContactID)
        {
            Service.DeleteContact(ContactID);
        }


    }
}