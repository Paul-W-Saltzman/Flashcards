using System;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    internal class Program
    {
        private static readonly string connectionString = "Server=localhost;Integrated Security=SSPI;";

        private static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string createDatabaseQuery = "CREATE DATABASE MyDatabase ON PRIMARY " +
                                                "(NAME = MyDatabase_Data, " +
                                                "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                                                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                                                "LOG ON (NAME = MyDatabase_Log, " +
                                                "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                                                "SIZE = 1MB, " +
                                                "MAXSIZE = 5MB, " +
                                                "FILEGROWTH = 10%)";

                    using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Database is created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating database: " + ex.Message);
            }
        }
    }
}
