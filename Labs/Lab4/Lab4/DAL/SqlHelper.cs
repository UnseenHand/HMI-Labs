using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab4.DAL
{
    public class SqlHelper
    {
        //OleDbConnection conn;
        //DataSet ds = new DataSet();
        //DbCommand comm;
        //public SqlHelper(string conString)
        //{
        //    conn = new OleDbConnection(conString);
        //}

        //public void OpenCon()
        //{
        //    conn.Open();
        //}

        //public void CloseCon()
        //{
        //    conn.Close();
        //}

        public static async Task CreateDb()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                //checking whether DB exists or not and creates it if not
                SqlCommand command1 = new SqlCommand();
                command1.CommandText = "SELECT * FROM dbo.Sysdatabases WHERE name=@namep";
                command1.Connection = connection;
                command1.Parameters.AddWithValue("namep", "RecreationBase");
                SqlDataReader reader = command1.ExecuteReader();
                if (!reader.Read())
                {
                    //creating 'RecreationBase' database
                    SqlCommand command2 = new SqlCommand();
                    command2.CommandText = "CREATE DATABASE RecreationBase";
                    command2.Connection = connection;
                    await command2.ExecuteNonQueryAsync();
                }
                else
                {
                    MessageBox.Show("RecreationBase DB already exists so you can add some tables to it");
                }
                connection.Close();
            }
        }

        public async Task CreateDbTable(string name)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                //checking whether Table exists or not and creates it if not
                DataTable dTable = connection.GetSchema("TABLES",
                               new string[] { null, null, name });
                if (dTable.Rows.Count <= 0)
                {
                    //creating table
                    SqlCommand command2 = new SqlCommand();
                    command2.CommandText = $"CREATE TABLE {name} (Id int NOT NULL PRIMARY KEY, BaseName nvarchar(100) NOT NULL, City nvarchar(100) NOT NULL, RoomPrice decimal NOT NULL, SeaDistance float NOT NULL);";
                    command2.Connection = connection;
                    await command2.ExecuteNonQueryAsync();
                }
                else
                {
                    MessageBox.Show("Bases Table already exists so you can add some records to it");
                }
                connection.Close();
            }
        }

        public async Task<int> Count()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select count(*) from Base";
                var count = command.ExecuteScalarAsync().Result;
                return (int)count;
            }
        }

        public async Task AddBase()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                //checking whether Table exists or not and creates it if not
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Bases (Id , BaseName, City, RoomPrice, SeaDistance) VALUES()";

                connection.Close();
            }
        }
    }
}
