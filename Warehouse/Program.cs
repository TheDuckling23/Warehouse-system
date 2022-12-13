namespace WarehouseInformationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = null; 
            string userInput = "";

            bool fileOpen = false;

            List<string> warehouseStock = new List<string>();

            while (userInput != "exit")
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Open");
                Console.WriteLine("2. Close");
                Console.WriteLine("3. Save");
                Console.WriteLine("4. Save As");
                Console.WriteLine("5. Help");
                Console.WriteLine("6. Print");
                Console.WriteLine("7. Add Product");
                Console.WriteLine("8. Exit");

                userInput = Console.ReadLine();


                switch (userInput)
                {
                    case "1":
                        if (fileOpen)
                        {
                            Console.WriteLine("Error: File is already open");
                        }
                        else
                        {
                            try
                            {
                                filename = Console.ReadLine();

                                warehouseStock = File.ReadAllLines(filename).ToList();

                                fileOpen = true;

                                Console.WriteLine("File opened successfully");
                            }
                            catch (IOException)
                            {
                                Console.WriteLine("Error: Unable to open file");
                            }
                        }
                        break;

                    case "2":
                        if (fileOpen)
                        {
                            fileOpen = false;

                            Console.WriteLine("File closed successfully");
                        }
                        else
                        {
                            Console.WriteLine("Error: File is not open");
                        }
                        break;

                    case "3":
                        if (fileOpen)
                        {
                            try
                            {
                                File.WriteAllLines(filename, warehouseStock);

                                Console.WriteLine("File saved successfully");
                            }
                            catch (IOException)
                            {
                                Console.WriteLine("Error: Unable to save file");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: File is not open");
                        }
                        break;

                    case "4":
                        if (fileOpen)
                        {
                            Console.WriteLine("Enter the new filename:");
                            string newFilename = Console.ReadLine();

                            try
                            {
                                File.WriteAllLines(newFilename, warehouseStock);

                                Console.WriteLine("File saved successfully as " + newFilename);
                            }
                            catch (IOException)
                            {
                                Console.WriteLine("Error: Unable to save file");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: File is not open");
                        }
                        break;

                    case "5":
                        Console.WriteLine("The Warehouse Information System allows you to manage a warehouse inventory.");
                        Console.WriteLine();
                        Console.WriteLine("Use the 'Open' option to open an existing inventory file or create a new one.");
                        Console.WriteLine("Use the 'Close' option to close the current inventory file.");
                        Console.WriteLine("Use the 'Save' option to save any changes made to the current inventory file.");
                        Console.WriteLine("Use the 'Save As' option to save the current inventory file with a new name.");
                        Console.WriteLine("Use the 'Exit' option to exit the program.");
                        Console.WriteLine();
                        break;

                    case "6": // Print
                        if (fileOpen)
                        {
                            Console.WriteLine("Available products in the warehouse:");
                            foreach (string item in warehouseStock)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You should first open a file.");
                        }
                        break;

                    case "7": //ADD
                        if (fileOpen)
                        {
                            Console.WriteLine("Enter product name:");
                            string productName = Console.ReadLine();

                            Console.WriteLine("Enter expiration date (yyyy-mm-dd):");
                            string expirationDate = Console.ReadLine();

                            Console.WriteLine("Enter quantity:");
                            int quantity = int.Parse(Console.ReadLine());

                            string newProduct = productName + "," + expirationDate + "," + quantity;

                            //Chech if produtc with same name and exp date exists
                            bool productExists = false;
                            foreach (string product in warehouseStock)
                            {
                                string[] productData = product.Split(',');
                                if (productData[0] == productName && productData[1] == expirationDate)
                                {
                                    productExists = true;
                                    break;
                                }
                            }

                            if (productExists)
                            {
                                //Add quantity to existing producto
                                for (int i = 0; i < warehouseStock.Count; i++)
                                {
                                    string[] productData = warehouseStock[i].Split(',');
                                    if (productData[0] == productName && productData[1] == expirationDate)
                                    {
                                        int existingQuantity = int.Parse(productData[2]);
                                        int updatedQuantity = existingQuantity + quantity;
                                        string updatedProduct = productName + "," + expirationDate + "," + updatedQuantity;
                                        warehouseStock[i] = updatedProduct;

                                    }
                                }
                            }
                            else
                            {
                                warehouseStock.Add(newProduct);
                            }




                        }
                        else
                            Console.WriteLine("You should first open a file.");
                        break;

                    case "8":
                        Console.WriteLine("Exiting the Warehouse Information System. Goodbye!");
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}