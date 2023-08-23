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
            foreach (var product in product)
            {
                Console.WriteLine($"{product.ProductName}, {product.Price}kr/{product.PriceType}, ID: {product.ProductId}");
            }

            Console.WriteLine("\nKASSA");
            Console.WriteLine($"KVITTO  {DateTime.Now}");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal>");
            Console.WriteLine("<PAY>");
            Console.Write("Kommando: ");
            var kommando = Console.ReadLine().ToUpper();
            var splitItems = kommando.Split(' ');

            if (kommando == "PAY")
                SaveToFile();


            int id = 0;
            int antal = 0;
            while (!int.TryParse(splitItems[0], out id) || !int.TryParse(splitItems[1], out antal))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fel ID eller antal");
                Console.ResetColor();
                Console.Write("Kommando: ");
                kommando = Console.ReadLine();
                splitItems = kommando.Split(' ');
            }

            Products products = product.Find(x => x.ProductId == id);
            while (products == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hittar inte ID");
                Console.ResetColor();
                Console.Write("Kommando: ");
                kommando = Console.ReadLine();
                splitItems = kommando.Split(' ');

                if (!int.TryParse(splitItems[0], out id)) ;
                products = product.Find(x => x.ProductId == id);
            }

            var productName = products.ProductName;
            if (!int.TryParse(splitItems[1], out antal)) ;
            var price = products.Price;
            var totalPrice = products.Price * antal;


            receipt.Add(new Receipt
            {
                ProductName = productName,
                ProductId = id,
                Amount = antal,
                Price = price,
                TotalCost = totalPrice,
            });

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

                if (!int.TryParse(splitItems[0], out id)) ;
                if (!int.TryParse(splitItems[1], out antal)) ;
                products = product.Find(x => x.ProductId == id);

                while (products == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hittar inte ID");
                    Console.ResetColor();
                    Console.Write("Kommando: ");
                    kommando = Console.ReadLine().ToUpper();
                    splitItems = kommando.Split(' ');
                    if (!int.TryParse(splitItems[0], out id));
                    if (!int.TryParse(splitItems[1], out antal)) ;
                    products = product.Find(x => x.ProductId == id);
                }

                productName = products.ProductName;
                if (!int.TryParse(splitItems[1], out antal)) ;
                price = products.Price;
                totalPrice = products.Price * antal;

                receipt.Add(new Receipt
                {
                    ProductName = productName,
                    ProductId = id,
                    Amount = antal,
                    Price = price,
                    TotalCost = totalPrice,
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
                Console.WriteLine($"{receipts.ProductName} | ID: {receipts.ProductId} | Antal: {receipts.Amount} | Pris: {receipts.TotalCost}Kr");
            }

            JsonToFile();

            receipt.Clear();
            Console.Write("\nTryck på valfri knapp för att återgå till startmenyn");
            Console.ReadKey();
            MainMenu();
        }

        public void JsonToFile()
        {
            var total = 0;
            foreach (var receipts in receipt)
            {
                total += receipts.TotalCost;
            }
            var date = DateTime.Now.ToString();
            string jsonString = JsonConvert.SerializeObject(receipt, Formatting.Indented);
            StreamWriter writeToFile = new StreamWriter(@$"RECEIPT_{DateTime.Now:yyyyMMdd}.txt", true);
            writeToFile.WriteLine($"KVITTO: " + date + jsonString + "TOTALKOSTNAD: " + total); writeToFile.Close();
        }
    }
}
