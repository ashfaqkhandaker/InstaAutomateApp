using Microsoft.Data.SqlClient;
using System.Configuration;

namespace InstaAutomateApp.Database
{
    public class DbHelper
    {
        private string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}