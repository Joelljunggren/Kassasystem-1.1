using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{

    public class App
    {
        //List<Products> product = new List<Products>();
        List<Receipt> receipt = new List<Receipt>();

        List <Products> product = new List<Products>()
            {
                new Products { ProductId = 201, Price = 20, PriceType = "Styck", ProductName = "Mjölk", Amount = 0 },
                new Products { ProductId = 202, Price = 5, PriceType = "Styck", ProductName = "Äpplen", Amount = 0 },
                new Products { ProductId = 203, Price = 50, PriceType = "Styck", ProductName = "Kaffe" , Amount = 0 },
                new Products { ProductId = 204, Price = 18, PriceType = "Per Kilo", ProductName = "Potatis", Amount = 0 },
                new Products { ProductId = 205, Price = 35, PriceType = "Per kilo", ProductName = "Vattenmelon" , Amount = 0 }
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

            Console.WriteLine("KASSA");
            Console.WriteLine($"KVITTO  {DateTime.Now}");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal>");
            Console.WriteLine("PAY");

            //Behöver lägga in någon while loop här för att fortsätta lägga till varor i kvittot tills jag trycker på Spara kvitto
            //slipper då ladda om NewCustomer hela tiden. Är lite lost för tillfället gällande exakt var den ska vara.

            Console.Write("Kommando: ");
            var kommando = Console.ReadLine();
            var splitItems = kommando.Split(' ');

            receipt.Add(new Receipt
            {
                PurchaseTime = DateTime.Now,//förstår inte hur jag plockar med price, productname osv från products
                ProductId = Convert.ToInt32(splitItems[0]),
                Amount = Convert.ToInt32(splitItems[1]),
                TotalCost = Convert.ToInt32(splitItems[1])
            }); //måste på något sätt få in products.price * split 1 här.

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Lägger till i kvittot");
            Console.ResetColor();

            Console.WriteLine("1: Lägg till fler varor");
            Console.WriteLine("2: Spara kvitto");
            var addMoreItems = Console.ReadKey();
            if (addMoreItems.KeyChar == '1')
                NewCustomer();
            if (addMoreItems.KeyChar == '2')
                SaveToFile();
        }

        //public void AddingItemToReceipt()
        //{
        //    Console.Clear();
        //    foreach (var products in product)
        //    {
        //        //products.GetPrice();
        //        Console.WriteLine($"{products.ProductName}, {products.Price}kr/{products.PriceType}, ID: {products.ProductId}");
        //    }

        //    Console.WriteLine("KASSA");
        //    Console.WriteLine($"KVITTO  {DateTime.Now}");
        //    Console.WriteLine("Kommandon:");
        //    Console.WriteLine("<productid> <antal>");
        //    Console.WriteLine("PAY");
        //    Console.Write("Kommando: ");
        //    var kommando = Console.ReadLine();
        //    var splitItems = kommando.Split(' ');

        //    receipt.Add(new Receipt
        //    {
        //        PurchaseTime = DateTime.Now,//förstår inte hur jag plockar med price, productname osv från products
        //        ProductId = Convert.ToInt32(splitItems[0]),
        //        Amount = Convert.ToInt32(splitItems[1]),
        //        TotalCost = Convert.ToInt32(splitItems[1])
        //    }); //måste på något sätt få in products.price * split 1 här.

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Lägger till i kvittot");
        //    Console.ResetColor();

        //    Console.WriteLine("1: Lägg till fler varor");
        //    Console.WriteLine("2: Spara kvitto");
        //    var addMoreItems = Console.ReadKey();
        //    if (addMoreItems.KeyChar == '1')
        //        AddingItemToReceipt();
        //    if (addMoreItems.KeyChar == '2')
        //        SaveToFile();
        //    //else MainMenu();
        //}

        public void SaveToFile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("KVITTO SPARAT\n");
            Console.ResetColor();
            foreach (var receipts in receipt)
            {
                Console.WriteLine($"Product ID: {receipts.ProductId}, Antal: {receipts.Amount} Tid: {receipts.PurchaseTime}");
            }

            //Spara ner all info från listan receipts till en txt fil
            receipt.Clear();
            Console.Write("\nTryck på valfri knapp för att återgå till startmenyn");
            Console.ReadKey();
            MainMenu();
        }
    }
}
