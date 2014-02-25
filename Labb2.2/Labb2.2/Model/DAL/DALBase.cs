using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Labb2._2.Model.DAL
{
    public abstract class DALBase
    {
        private readonly static string _connectionString;

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["connectstring"].ConnectionString;
        }

        protected SqlConnection CreateConnection(){
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString; //Skapat ett SqlConnection objekt med ConnectionSträngen som pekar mot rätt databas..
            return myConnection;
        }
    }
}