using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySqlConsole
{
    /// <summary>
    /// The MySqlConnectionTest class provides a method to test the connection from this C# project to the local MySQL Server.
    /// </summary>
    public static class MySqlConnectionTest
    {
        /// <summary>
        /// Test the connection from this C# project to the local MySQL Server.
        /// </summary>
        public static void ConnectionTest()
        {
            string connectionString = @"Server = localhost; User Id = DinoStark; Password = non-feeling; Database = Startup;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            string query = @"Select * From Transactions;";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    Console.WriteLine(reader[0] + " " + reader[1]);

                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error Code:" + ex.ErrorCode);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}