using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Petpals.DBConnection
{
    using System;
    using Microsoft.Data.SqlClient;

    namespace Petpals.Data
    {
        public class DbConnection
        {
            private readonly string _connectionString;

            public DbConnection(string connectionString)
            {
                _connectionString = connectionString;
            }

            public SqlConnection OpenConnection()
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }

            public void CloseConnection(SqlConnection connection)
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }

}
