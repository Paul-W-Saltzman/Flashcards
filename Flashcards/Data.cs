using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using static System.Collections.Specialized.BitVector32;

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
        public static void CreateTables()
        {
            CreatStacksTable();
            CreateCardsTable();
            CreateSudySessionsTable();
            
        }

        private static void CreateSudySessionsTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"USE FlashCards;
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StudySessions')
                    BEGIN
                    CREATE TABLE dbo.StudySessions (
                    StudySessionID INT IDENTITY(1,1) PRIMARY KEY, 
                    StackID INT,
                    Date DATE,
                    StackName VARCHAR(50),
                    Correct INT,
                    Total INT,
                    Score INT
                    CONSTRAINT fk_study_stack_id
                        FOREIGN KEY (StackID)
                        REFERENCES Stacks (StackID)
                        ON DELETE CASCADE)

                    END";
                try
                {
                    int rowsAffected = tableCmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Study Sessions Table");
                    Console.WriteLine(exception);
                    Console.ReadLine();
                }
                connection.Close();

            }

        }

        private static void CreateCardsTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"USE FlashCards;
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cards')
                    BEGIN
                    CREATE TABLE dbo.Cards (
                    CardID INT IDENTITY(1,1) PRIMARY KEY,
                    StackID INT,
                    NoInStack INT,
                    Front VARCHAR(50),
                    Back VARCHAR(50)
                    CONSTRAINT fk_card_stack_id
                        FOREIGN KEY (StackID)
                        REFERENCES Stacks (StackID)
                        ON DELETE CASCADE)
                    END";
                try
                {
                    int rowsAffected = tableCmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Cards Table");
                    Console.WriteLine(exception);
                    Console.ReadLine();
                }
                connection.Close();
            }
        }
     

        private static void CreatStacksTable() 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"USE FlashCards;
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Stacks')
                    BEGIN
                    CREATE TABLE dbo.Stacks (
                    StackID INT IDENTITY(1,1) PRIMARY KEY,
                    StackName VARCHAR(50)); 
                    END";
                try
                {
                    int rowsAffected = tableCmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Stacks Table");
                    Console.WriteLine(exception);
                    Console.ReadLine();
                }
                connection.Close();
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

        internal static bool DoesStackExist(string Name)
        {
            bool doesStackExist = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"USEING FlashCards SELECT COUNT(*) FROM ";
                try
                {
                    int rowsAffected = tableCmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Does Stack Exist");
                    Console.WriteLine(exception);
                    Console.ReadLine();
                }
                connection.Close();
            }

            return doesStackExist;
        }
        internal static int LoadStack(string stackName)
        {
            int stackID = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $@"USE FlashCards;
                    INSERT INTO Stacks (StackName) VALUES('{stackName}');";
                try
                {
                    int rowsAffected = tableCmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("LoadStack1");
                    Console.WriteLine(exception);
                    Console.ReadLine();
                }
                tableCmd.CommandText = 
                    $@"Select last_insert_rowid();";
                try
                {
                    stackID = Convert.ToInt32(tableCmd.ExecuteScalar());

                    if (stackID > 0)
                    {
                        //session.Id = insertedId;
                    }
                    else
                    {
                        // The insert did not affect any rows (may indicate an issue).
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("LoadStack2");
                    Console.WriteLine(exception);
                }
                connection.Close();
            }
            return stackID;
        }
    }
}
