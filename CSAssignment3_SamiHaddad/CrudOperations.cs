using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * CrudOperations - Create-Read-Update-Delete
 */
namespace CSAssignment3_SamiHaddad
{

    class CrudOperations
    {

        // private fields
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _dataSet;
        private DataTable _tblVehicle;
        private DataTable _tblInventory;
        private DataTable _tblRepair;
        private static string _connectionString = "ConnectionStrings:CarRepairDB";

        // default constructor
        public CrudOperations()
        {

            _conn = new SqlConnection(GetConnectionString(_connectionString));

            // Inserting default entries to Vehicle Table
            //InsertVehicle("Mercedes-Benz", "C300", 2016, "Used");
            //InsertVehicle("Toyota", "Corolla", 2022, "New");
            //InsertVehicle("Honda", "Accord", 2012, "Used");
            //InsertVehicle("Bentley", "Continental", 2011, "Used");
            //InsertVehicle("Nissan", "Rogue", 2016, "Used");

            // Inserting default entries to Inventory Table
            //InsertInventory(1, 5, 40000, 45000);
            //InsertInventory(2, 10, 30000, 35000);
            //InsertInventory(3, 12, 34000, 37000);
            //InsertInventory(4, 2, 80000, 87000);
            //InsertInventory(5, 5, 50000, 55000);

            // Inserting default entries to Repair Table
            //InsertRepair(1, "Change engine coolant");
            //InsertRepair(2, "Needs Oil change");
            //InsertRepair(3, "Change washer fluid");
            //InsertRepair(4, "Change spark plug");
            //InsertRepair(5, "Change tires");

            // Testing print functions
            //PrintVehicle();
            //PrintInventory();
            //PrintRepair();

            //GetInventoryByVehicleId(1);

        }

        // argument constructor
        public CrudOperations(int crudOption)
        {
            
        }

        static string GetConnectionString(string connectionString)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();

            return config[connectionString];
        }

        /*/ Vehicle CRUD Operations /*/

        // Create Vehicle
        public void InsertVehicle(string make, string model, int year, string condition)
        {
            string query = "INSERT INTO Vehicle (make, model, year, condition) VALUES (@make, @model, @year, @condition)";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("make", make);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("year", year);
                cmd.Parameters.AddWithValue("condition", condition);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Vehicle ({make} {model}, {year}) was added successfully");
                else
                    Console.WriteLine("Vehicle Failed to add");
            }
        }

        // Read Vehicle
        public static void PrintVehicle()
        {
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ID, make, model, year, condition from Vehicle";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("//--------------------------- Vehicle Table ---------------------------\\\\");
                Console.WriteLine($"{"ID",-10}{"Make",-17}{"Model",-20}" +
                    $"{"Year",-15}{"Condition"}");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string make = reader.GetString(1);
                    string model = reader.GetString(2);
                    int year = reader.GetInt32(3);
                    string cond = reader.GetString(4);

                    Console.WriteLine($"{id,-10}{make, -17}" +
                        $"{model, -20}{year, -15}" +
                        $"{cond}");
                }
                Console.WriteLine("\\\\--------------------------- End of Table ---------------------------//");
            }
        }

        // Update Vehicle
        public static void UpdateVehicleById(int id, string make, string model, int year, string condition)
        {
            string query = "UPDATE Vehicle SET make = @make, model = @model, year = @year, condition = @condition WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("make", make);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("year", year);
                cmd.Parameters.AddWithValue("condition", condition);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Vehicle number {id} was updated successfully");
                else
                    Console.WriteLine("Vehicle Failed to update");
            }
        }

        // Delete Vehicle
        public static void DeleteVehicleById(int id)
        {
            string query = "DELETE VEHICLE WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Vehicle #{id} deleted");
                else
                    Console.WriteLine($"Vehicle #{id} failed to delete");
            }
        }

        // Count Repair Rows
        public static int CountVehicleRows()
        {
            string query = "SELECT COUNT(*) FROM Vehicle";
            string cs = GetConnectionString(_connectionString);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                return (Int32)cmd.ExecuteScalar();
            }

        }
        /*** End Vehicle Functions ***/

        /*/ Start Inventory CRUD Functions /*/

        // Create Inventory
        public void InsertInventory(int vehicleID, int numberOnHand, decimal price, decimal cost)
        {
            string query = "INSERT INTO Inventory (vehicleID, numberOnHand, price, cost) " +
                "VALUES (@vehicleID, @numberOnHand, @price, @cost)";
            string cs = GetConnectionString(_connectionString);
            
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("vehicleID", vehicleID);
                cmd.Parameters.AddWithValue("numberOnHand", numberOnHand);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("cost", cost);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Inventory entry added");
                else
                    Console.WriteLine("Inventory entry failed");
            }
        }

        // Read Inventory
        public static void PrintInventory()
        {
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ID, vehicleID, numberOnHand, price, cost from Inventory";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("//------------------------- Inventory Table -------------------------\\\\");
                Console.WriteLine($"{"ID",-10}{"Vehicle ID",-15}{"Number on Hand",-20}" +
                    $"{"Price",-15}{"Cost"}");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int vehicleId = reader.GetInt32(1);
                    int numberOnHand = reader.GetInt32(2);
                    decimal price = reader.GetDecimal(3);
                    decimal cost = reader.GetDecimal(4);

                    Console.WriteLine($"{id,-10}{vehicleId,-15}" +
                        $"{numberOnHand,-20}{price,-15}" +
                        $"{cost}");
                }
                Console.WriteLine("\\\\------------------------- End of Table -------------------------//");
            }
        }

        // Update Inventory
        public static void UpdateInventoryById(int id, int vehicleID, int numberOnHand, decimal price, decimal cost)
        {
            string query = "UPDATE Inventory SET vehicleID = @vehicleID, numberOnHand = @numberOnHand, " +
                "price = @price, cost = @cost WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("vehicleID", vehicleID);
                cmd.Parameters.AddWithValue("numberOnHand", numberOnHand);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("cost", cost);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Inventory entry #{id} was updated successfully");
                else
                    Console.WriteLine("Inventory Failed to update");
            }
        }

        // Delete Inventory
        public static void DeleteInventoryById(int id)
        {
            string query = "DELETE Inventory WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Inventory #{id} deleted");
                else
                    Console.WriteLine($"Inventory #{id} failed to delete");
            }
        }

        // method to print a single Inventory based on its Vehicle ID
        public void GetInventoryByVehicleId(int vehicleID)
        {
            string cs = GetConnectionString(_connectionString);
            string query = "SELECT ID, vehicleID, numberOnHand, price, cost FROM Inventory WHERE vehicleID = 2;";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("vehicleID", vehicleID);
                SqlDataReader reader = cmd.ExecuteReader();
                
                Console.WriteLine($"{"ID",-10}{"Vehicle ID",-15}{"Number on Hand",-20}" +
                    $"{"Price",-15}{"Cost"}");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    //int vehicleId = reader.GetInt32(1);
                    int numberOnHand = reader.GetInt32(2);
                    decimal price = reader.GetDecimal(3);
                    decimal cost = reader.GetDecimal(4);

                    Console.WriteLine($"{id,-10}{vehicleID,-15}" +
                        $"{numberOnHand,-20}{price,-15}" +
                        $"{cost}");
                }
            }
        }

        // Count Inventory Rows
        public static int CountInventoryRows()
        {
            string query = "SELECT COUNT(*) FROM Inventory";
            string cs = GetConnectionString(_connectionString);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                return (Int32)cmd.ExecuteScalar();

            }

        }
        /*** End Inventory Functions ***/

        /*/ Start Repair CRUD Functions /*/

        // Create Repair
        public void InsertRepair(int inventoryID, string whatToRepair)
        {
            string query = "INSERT INTO Repair (inventoryID, whatToRepair) " +
                "VALUES (@inventoryID, @whatToRepair)";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("inventoryID", inventoryID);
                cmd.Parameters.AddWithValue("whatToRepair", whatToRepair);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Repair entry added");
                else
                    Console.WriteLine("Repair entry failed");
            }
        }

        // Read Repair
        public static void PrintRepair()
        {
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT ID, inventoryID, whatToRepair from Repair";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // print Table header
                Console.WriteLine("//----------------- Repair Table -----------------\\\\");
                Console.WriteLine($"{"ID",-10}{"Inventory ID",-20}{"What To Repair"}");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int inventoryID = reader.GetInt32(1);
                    string whatToRepair = reader.GetString(2);

                    Console.WriteLine($"{id, -10}{inventoryID,-20}{whatToRepair}");
                }
                Console.WriteLine("\\\\----------------- End of Table -----------------//");
            }
        }

        // Update Repair
        public static void UpdateRepairById(int id, int inventoryID, String whatToRepair)
        {
            string query = "UPDATE Repair SET inventoryID = @inventoryID, whatToRepair = @whatToRepair WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("inventoryID", inventoryID);
                cmd.Parameters.AddWithValue("whatToRepair", whatToRepair);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Repair entry #{id} was updated successfully");
                else
                    Console.WriteLine("Repair Failed to update");
            }
        }

        // Delete Repair
        public static void DeleteRepairById(int id)
        {
            string query = "DELETE Repair WHERE ID = @id;";
            string cs = GetConnectionString(_connectionString);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine($"Repair #{id} deleted");
                else
                    Console.WriteLine($"Repair #{id} failed to delete");
            }
        }

        // Count Repair Rows
        public static int CountRepairRows()
        {
            string query = "SELECT COUNT(*) FROM repair";
            string cs = GetConnectionString(_connectionString);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Connection.Open();

                return (Int32)cmd.ExecuteScalar();
            }
            
        }
        /*** End Repair Functions ***/
        
    }

}
