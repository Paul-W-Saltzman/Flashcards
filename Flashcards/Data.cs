using System;
using System.Diagnostics.Metrics;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using Microsoft.IdentityModel.Protocols;
using static System.Net.Mime.MediaTypeNames;

namespace Flashcards
{
    internal class Data
    {
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
        private static readonly string connectionString = "" +
            "Data Source = (localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=master;" +
            "Integrated Security = True; Connect Timeout = 30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent = ReadWrite; " +
            "Multi Subnet Failover=False";

        internal static void CheckCreateDatabase()
        {

            string databaseName = "FlashCards";
            string projectPath = Directory.GetCurrentDirectory();

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
                        //Create the database


                        connection.Open();

                        string createDatabaseQuery = @$"BEGIN
                                                    CREATE DATABASE {databaseName} ON PRIMARY
                                                    (NAME = N'{databaseName}',
                                                    FILENAME = N'{projectPath}\\{databaseName}Data.mdf',
                                                    SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)
                                                    LOG ON (NAME = N'{databaseName}_Log',
                                                    FILENAME = N'{projectPath}\\{databaseName}Log.ldf',
                                                    SIZE = 1MB,
                                                    MAXSIZE = 5MB,
                                                    FILEGROWTH = 10%)
                                                    END";

                        using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();

                            Console.WriteLine("Database is created successfully.");
                            Console.ReadLine();

                            //output string
                        //    Console.WriteLine(createDatabaseQuery);
                        //    Console.ReadLine();
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

