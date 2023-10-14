using System;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Flashcards
{
    internal class Data
    {
        private static readonly string connectionString = "" +
            "Data Source=localhost;" +
            "Integrated Security = True; Connect Timeout = 30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent = ReadWrite; " +
            "Multi Subnet Failover=False";

        internal static void CheckCreateDatabase()
        {

            string databaseName = "FlashCards";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Check if the database already exists
                    bool databaseExists = CheckIfDatabaseExists(connection, databaseName);

                    if (databaseExists)
                    {
                        Console.WriteLine("Database already exists.");
                        Console.ReadLine();
                    }
                    else if(!databaseExists)
                    {
                        // Create the database
                        

                        string createDatabaseQuery = $"CREATE DATABASE {databaseName} ON PRIMARY " +
                                                    $"(NAME = {databaseName}_Data, " +
                                                    $"FILENAME = 'C:\\{databaseName}Data.mdf', " +
                                                    "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                                                    $"LOG ON (NAME = {databaseName}_Log, " +
                                                    $"FILENAME = 'C:\\{databaseName}Log.ldf', " +
                                                    "SIZE = 1MB, " +
                                                    "MAXSIZE = 5MB, " +
                                                    "FILEGROWTH = 10%)";

                        using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                        {
                            //connection.Open();
                            //command.ExecuteNonQuery();
                            Console.WriteLine(createDatabaseQuery);
                            Console.ReadLine();
                            //Console.WriteLine("Database is created successfully.");
                            //Console.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating or checking database: " + ex.Message);
                
            }
        }




        private static bool CheckIfDatabaseExists(SqlConnection connection, string databaseName)
        {
            string query = $"SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name = '{databaseName}'";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                int databaseCount = Convert.ToInt32(command.ExecuteScalar());
                return databaseCount > 0;
            }
        }
    }
}

