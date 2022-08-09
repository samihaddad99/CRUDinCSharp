using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CSAssignment3_SamiHaddad
{
    class Program
    {

        static string[] mainMenu = {"Press 1 to modify vehicles",
                                "Press 2 to modify Inventory",
                                "Press 3 to modify repair",
                                "Press 4 to exit program" };

        static string[] vehicleMenu = {"Press 1 to list all vehicles",
                                    "Press 2 to add a new vehicle",
                                    "Press 3 to update a vehicle",
                                    "Press 4 to delete a vehicle",
                                    "Press 5 to return to main menu"
            };

        static string[] inventoryMenu = {"Press 1 to insert new inventory",
                                    "Press 2 view inventory for a vehicle",
                                    "Press 3 to update an inventory",
                                    "Press 4 to delete an inventory",
                                    "Press 5 to return to main menu"
            };

        static string[] repairMenu = {"Press 1 to view all repairs",
                                    "Press 2 add a repair",
                                    "Press 3 to update a repair",
                                    "Press 4 to delete a repair",
                                    "Press 5 to return to main menu"
            };

        CrudOperations crud = new CrudOperations(); // instantiate instance of crud operations

        static void Main(string[] args)
        {
            MainMenuFunctions();       
        }

        public static void MainMenuFunctions()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome, Please choose a command:");
                int mainMenuInput = ValidateInt(mainMenu.Length + 1, $"Please enter an integer value between 1 and {mainMenu.Length + 1}");
                switch (mainMenuInput)
                {
                    case 1: VehicleFunctions(); break;
                    case 2: InventoryFunctions(); break;
                    case 3: RepairFunctions(); break;
                    default: return;
                }
            }
        }


        // Vehicle Functions

        public static void VehicleFunctions()
        {
            Console.WriteLine("Please choose a vehicle function below:");
            int mainMenuInput = ValidateInt(vehicleMenu.Length + 1, $"Please enter an integer value between 1 and {vehicleMenu.Length + 1}");
            switch (mainMenuInput)
            {
                case 1: CrudOperations.PrintVehicle(); break;
                case 2: InsertVehicleRow(); break;
                case 3: UpdateVehicleRow(); break;
                case 4: DeleteVehicleRow(); break;
                default: MainMenuFunctions(); break;
            }
        }

        public static void InsertVehicleRow()
        {
            CrudOperations crud = new CrudOperations();

            string make = ValidateString("Enter the make of the vehicle");
            string model = ValidateString("Enter the model of the vehicle");
            string condition = ValidateString("Enter the condition of the vehicle (New/Used)");
            int year = ValidateInt(DateTime.Today.Year, $"Please enter a valid year; " +
                $"more than 0 and less than {DateTime.Today.Year}");
            crud.InsertVehicle(make, model, year, condition);
        }

        public static void UpdateVehicleRow()
        {
            CrudOperations crud = new CrudOperations();

            int id = ValidateInt(CrudOperations.CountVehicleRows(), $"Please enter a number between 1 and {CrudOperations.CountVehicleRows()} (inclusive)");
            
            string make = ValidateString("Enter the make of the vehicle");
            string model = ValidateString("Enter the model of the vehicle");
            string condition = ValidateString("Enter the condition of the vehicle (New/Used)");
            int year = ValidateInt(DateTime.Today.Year, $"Please enter a valid year; " +
                $"more than 0 and less than {DateTime.Today.Year}");
            CrudOperations.UpdateVehicleById(id, make, model, year, condition);
        }

        public static void DeleteVehicleRow()
        {
            CrudOperations crud = new CrudOperations();

            int id = ValidateInt(CrudOperations.CountVehicleRows(), $"Please enter a number between 1 and {CrudOperations.CountVehicleRows()} (inclusive)");

            CrudOperations.DeleteVehicleById(id);
        }

        // End Vehicle Functions


        // Inventory Functions

        private static void InventoryFunctions()
        {
            Console.WriteLine("Please choose a Inventory function below:");
            int mainMenuInput = ValidateInt(inventoryMenu.Length + 1, $"Please enter an integer value between 1 and {inventoryMenu.Length + 1}");
            switch (mainMenuInput)
            {
                case 1: CrudOperations.PrintInventory(); break;
                case 2: InsertInventoryRow(); break;
                case 3: UpdateInventoryRow(); break;
                case 4: DeleteInventoryRow(); break;
                default: MainMenuFunctions(); break;
            }
        }

        // Insert Row
        private static void InsertInventoryRow()
        {
            CrudOperations crud = new CrudOperations();

            int vehicleId = ValidateInt(CrudOperations.CountVehicleRows(), $"Enter Vehicle ID between 1 and {CrudOperations.CountVehicleRows()}");
            int numberOnHand = ValidateInt(100, "Enter a number of vehicles between 1 and 100");
            decimal price = ValidateDecimal("Enter the price of the vehicle", "Please enter a non-negative/non-zero number");
            decimal cost = ValidateDecimal("Enter the cost of the vehicle", "Please enter a non-negative/non-zero number");
            crud.InsertInventory(vehicleId, numberOnHand, price, cost);
        }

        // Update Row
        private static void UpdateInventoryRow()
        {
            CrudOperations crud = new CrudOperations();
            int id = ValidateInt(CrudOperations.CountInventoryRows(), $"Enter Inventory ID between 1 and {CrudOperations.CountInventoryRows()}");
            int vehicleId = ValidateInt(CrudOperations.CountVehicleRows(), $"Enter Vehicle ID between 1 and {CrudOperations.CountVehicleRows()}");
            int numberOnHand = ValidateInt(100, "Enter a number of vehicles between 1 and 100");
            decimal price = ValidateDecimal("Enter the price of the vehicle", "Please enter a non-negative/non-zero number");
            decimal cost = ValidateDecimal("Enter the cost of the vehicle", "Please enter a non-negative/non-zero number");
            CrudOperations.UpdateInventoryById(id, vehicleId, numberOnHand, price, cost);
        }

        // Delete Row
        private static void DeleteInventoryRow()
        {
            CrudOperations crud = new CrudOperations();
            int id = ValidateInt(CrudOperations.CountInventoryRows(), $"Enter Inventory ID between 1 and {CrudOperations.CountInventoryRows()}");
            CrudOperations.DeleteInventoryById(id);
        }

        // End Inventory Functions


        // Repair Functions

        private static void RepairFunctions()
        {
            Console.WriteLine("Please choose a Repair function below:");
            int repairMenuInput = ValidateInt(inventoryMenu.Length + 1, $"Please enter an integer value between 1 and {repairMenu.Length + 1}");
            switch (repairMenuInput)
            {
                case 1: CrudOperations.PrintInventory(); break;
                case 2: InsertRepairRow(); break;
                case 3: UpdateRepairRow(); break;
                case 4: DeleteRepairRow(); break;
                default: MainMenuFunctions(); break;
            }
        }

        // Insert Row
        private static void InsertRepairRow()
        {
            CrudOperations crud = new CrudOperations();

            int inventoryId = ValidateInt(CrudOperations.CountVehicleRows(), $"Enter Vehicle ID between 1 and {CrudOperations.CountInventoryRows()}");
            string numberOnHand = ValidateString("Enter the car repair details:");
            crud.InsertRepair(inventoryId, numberOnHand);
        }

        // Update Row
        private static void UpdateRepairRow()
        {
            CrudOperations crud = new CrudOperations();
            int id = ValidateInt(CrudOperations.CountRepairRows(), $"Enter Repair ID between 1 and {CrudOperations.CountRepairRows()}");
            int inventoryId = ValidateInt(CrudOperations.CountInventoryRows(), $"Enter Inventory ID between 1 and {CrudOperations.CountInventoryRows()}");
            string numberOnHand = ValidateString("Enter the car repair details:");
            CrudOperations.UpdateRepairById(id, inventoryId, numberOnHand);
        }

        // Delete Row
        private static void DeleteRepairRow()
        {
            CrudOperations crud = new CrudOperations();
            int id = ValidateInt(CrudOperations.CountRepairRows(), $"Enter Inventory ID between 1 and {CrudOperations.CountRepairRows()}");
            CrudOperations.DeleteRepairById(id);
        }

        // End Repair Functions

        /**
         * ValidateInt - get a user integer input and validate it
         * @return int - the user input when converted
         * errorMsg - when user enters wrong input
         * maxRange - for the largest possible value in the input (inclusive)
         */
        public static int ValidateInt(int maxRange, string errorMsg)
        {
            bool wrongInput = true;
            while (wrongInput)
            {
                int input = Convert.ToInt16(Console.ReadLine());
                if (input <= maxRange && input > 0)
                {
                    wrongInput = false;
                    return input;
                }
                else
                    Console.WriteLine(errorMsg);
            }
            return -1; // rogue value
        }

        public static string ValidateString(string prompt)
        {
            bool wrongInput = true;
            Console.WriteLine(prompt);
            string errorMsg = "Please enter a non-empty string";
            while (wrongInput)
            {
                string input = Console.ReadLine();
                if (input.Length >= 0 && input != null)
                {
                    wrongInput = false;
                    return input;
                }
                else
                    Console.WriteLine(errorMsg);
            }
            return null; // rogue value
        }

        public static decimal ValidateDecimal(string prompt, string errorMsg)
        {
            bool wrongInput = true;
            Console.WriteLine(prompt);
            while (wrongInput)
            {
                decimal input = Convert.ToDecimal(Console.ReadLine());
                if (input > 0)
                {
                    wrongInput = false;
                    return input;
                }
                else
                    Console.WriteLine(errorMsg);
            }
            return 0; // rogue value
        }
        /*
         * printArray - prints every individual element (elem) in the array list to the console
         * array - is a list of T type
         */
        static void PrintArray(String[] array)
        {
            foreach (var i in array)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
