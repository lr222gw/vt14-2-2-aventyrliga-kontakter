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

        public Contact GetContact(int ContactId)
        {
            return ContactDAL.GetContactById(ContactId);
        }

    }
}