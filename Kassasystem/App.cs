using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kassasystem
{

    public class App
    {
        List<Receipt> receipt = new List<Receipt>();
        List <Products> product = new List<Products>()
            {
                new Products { ProductId = 201, Price = 20, PriceType = "Styck", ProductName = "Mjölk"},
                new Products { ProductId = 202, Price = 5, PriceType = "Styck", ProductName = "Äpplen" },
                new Products { ProductId = 203, Price = 50, PriceType = "Styck", ProductName = "Kaffe" },
                new Products { ProductId = 204, Price = 18, PriceType = "Per Kilo", ProductName = "Potatis" },
                new Products { ProductId = 205, Price = 35, PriceType = "Per kilo", ProductName = "Vattenmelon" }
            };

        public void MainMenu()
        {
            Console.Clear();
            bool keepAlive = true;
            while(keepAlive)
            {
                Console.WriteLine("KASSA");
                Console.WriteLine("1: Ny kund");
                Console.WriteLine("0: Avsluta");
                var menuChoice = Console.ReadKey();

                switch(menuChoice.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        NewCustomer();
                        keepAlive = false;
                        break;
                    case '0':
                        Console.Clear();
                        Console.WriteLine("Stänger kassan");
                        keepAlive = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Välj ett korrekt alternativ");
                        break;
                }
            }
        }
        public void NewCustomer()
        {
            Console.Clear();
            foreach (var products in product)
            {
                //products.GetPrice();
                Console.WriteLine($"{products.ProductName}, {products.Price}kr/{products.PriceType}, ID: {products.ProductId}");
            }

            Console.WriteLine("\nKASSA");
            Console.WriteLine($"KVITTO  {DateTime.Now}");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal>");
            Console.WriteLine("<PAY>");
            Console.Write("Kommando: ");
            var kommando = Console.ReadLine();
            var splitItems = kommando.Split(' ');

            //Hela skiten här är skevt, vill egentligen kolla igenom mina products för att se efter om splitItems[0]
            //matchar ett ID och sen lägga till hela objektet i listan receipt.
            //Kan då även använda det som felhantering så att man inte kan lägga till produkter som inte finns. 
            if (kommando == "PAY")
                SaveToFile();

            receipt.Add(new Receipt
            {
                //förstår inte hur jag plockar med price, productname osv från products
                ProductId = Convert.ToInt32(splitItems[0]),
                Amount = Convert.ToInt32(splitItems[1]),
                TotalCost = Convert.ToInt32(splitItems[1])
            }); //måste på något sätt få in products.price * split 1 här.
                //Vill se till så att det bara finns en purchaseTime för kvittot. Inte för varje gång jag lägger till en produkt

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Lägger till i kvittot");
            Console.ResetColor();


            while (kommando != "PAY")
            {
                Console.Write("Kommando: ");
                kommando = Console.ReadLine().ToUpper();
                splitItems = kommando.Split(' ');

                if (kommando == "PAY")
                    SaveToFile();

                receipt.Add(new Receipt
                {
                    ProductId = Convert.ToInt32(splitItems[0]),
                    Amount = Convert.ToInt32(splitItems[1]),
                    TotalCost = Convert.ToInt32(splitItems[1])
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lägger till i kvittot");
                Console.ResetColor();
            }
        }

        public void SaveToFile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("KVITTO SPARAT\n");
            Console.ResetColor();
            Console.WriteLine(DateTime.Now);
            foreach (var receipts in receipt)
            {
                //Vill ha med all information här, produktnamn, produktID, antal och total kostnad för hela köpet
                Console.WriteLine($"Product ID: {receipts.ProductId}, Antal: {receipts.Amount}");
            }

            JsonToFile();

            receipt.Clear();
            Console.Write("\nTryck på valfri knapp för att återgå till startmenyn");
            Console.ReadKey();
            MainMenu();
        }

        public void JsonToFile()
        {
            var date = DateTime.Now.ToString();
            string jsonString = JsonConvert.SerializeObject(receipt, Formatting.Indented);
            //Console.WriteLine(jsonString);
            StreamWriter writeToFile = new StreamWriter(@$"RECEIPT_{DateTime.Now:yyyyMMdd}.txt", true);
            writeToFile.WriteLine($"KVITTO: " + date + jsonString); writeToFile.Close();
        }
    }
}
