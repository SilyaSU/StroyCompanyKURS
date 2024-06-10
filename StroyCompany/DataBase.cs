using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace StroyCompany
{
    class DataBase : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=Stroy;Integrated Security=True;TrustServerCertificate=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlDataReader ExecuteQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            return cmd.ExecuteReader();
        }

        public List<string> GetTables()
        {
            List<string> tables = new List<string>();
            DataTable schema = sqlConnection.GetSchema("Tables");

            foreach (DataRow row in schema.Rows)
            {
                tables.Add(row[2].ToString());
            }

            return tables;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                sqlConnection.Dispose();
            }
        }


    }
}
