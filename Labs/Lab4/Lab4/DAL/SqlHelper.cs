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
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand
                {
                    CommandText = "SELECT COUNT(*) FROM Bases",
                    Connection = connection
                };
                return (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task AddNewBase(Base @base)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Integrated Security=True;MultipleActiveResultSets=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Bases (Id , BaseName, City, RoomPrice, SeaDistance) VALUES(@Id, @BaseName, @City, @RoomPrice, @SeaDistance)";
                if (@base.BaseName != string.Empty
                    && @base.City != string.Empty
                    && @base.RoomPrice != default
                    && @base.SeaDistance != default)
                {
                    SqlCommand commandCheck = new SqlCommand();
                    commandCheck.CommandText = $"SELECT * FROM Bases";
                    commandCheck.Connection = connection;
                    var id = 0;
                    if (commandCheck.ExecuteScalarAsync() != null)
                    {
                        SqlCommand command1 = new SqlCommand();
                        command1.CommandText = "SELECT * FROM Bases";
                        command1.Connection = connection;
                        var records = new List<Base>();
                        using (SqlDataReader dataReader = await command1.ExecuteReaderAsync())
                        {
                            while (dataReader.Read())
                            {
                                var b = new Base
                                {
                                    Id = Convert.ToInt32(dataReader["Id"]),
                                    BaseName = Convert.ToString(dataReader["BaseName"]),
                                    City = Convert.ToString(dataReader["City"]),
                                    RoomPrice = Convert.ToDecimal(dataReader["RoomPrice"]),
                                    SeaDistance = Convert.ToDouble(dataReader["SeaDistance"])
                                };
                                records.Add(b);
                            }
                        }
                        id = records.Last().Id + 1;
                    }
                    else
                    {
                        id = 1;
                    }
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@BaseName", @base.BaseName);
                    command.Parameters.AddWithValue("@City", @base.City);
                    command.Parameters.AddWithValue("@RoomPrice", @base.RoomPrice);
                    command.Parameters.AddWithValue("@SeaDistance", @base.SeaDistance);
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                }
                connection.Close();
            }
        }

        public async Task<DataTable> GetBases()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=RecreationBase;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Bases";
                command.Connection = connection;
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    table.Load(dataReader);
                }
                return table;
            }
        }

    }
}
