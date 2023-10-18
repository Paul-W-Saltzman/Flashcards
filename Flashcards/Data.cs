using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;

namespace Flashcards
{
    internal class Data
    {
        private static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        internal static void CheckCreateDatabase()
        {
            string databaseName = "FlashCards";
            string projectPath = Directory.GetCurrentDirectory();
            bool databaseExists = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    databaseExists = CheckIfDatabaseExists(connection, databaseName); 
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking database: " + ex.Message);
            }


            if (databaseExists) 
            {
                Console.WriteLine("Database Exits.");
            }
            else if (!databaseExists)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = @$"BEGIN
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
                        command.ExecuteNonQuery();

                        connection.Close();


                        Console.WriteLine("Database created successfully.");


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating database: " + ex.Message);
                }
            }
        }

        private static bool CheckIfDatabaseExists(SqlConnection connection, string databaseName)
        {
            bool databaseExists = false;
            string query = "SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name = @DatabaseName";

            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DatabaseName", databaseName);
                    connection.Open();
                    int databaseCount = Convert.ToInt32(command.ExecuteScalar());
                    databaseExists = (databaseCount > 0);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return databaseExists;
        }
    }
}
