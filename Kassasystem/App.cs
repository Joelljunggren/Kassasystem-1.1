using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{

    public class App
    {
        List<Products> product = new List<Products>();

        public void MainMenu()
        {
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
            var product = new List<Products>()
            {
                new Products { ProductId = 201, Price = 20, PriceType = "Styck", ProductName = "Mjölk", Amount = 0 },
                new Products { ProductId = 202, Price = 5, PriceType = "Styck", ProductName = "Äpplen", Amount = 0 },
                new Products { ProductId = 203, Price = 50, PriceType = "Styck", ProductName = "Kaffe" , Amount = 0 },
                new Products { ProductId = 204, Price = 18, PriceType = "Per Kilo", ProductName = "Potatis", Amount = 0 },
                new Products { ProductId = 205, Price = 35, PriceType = "Per kilo", ProductName = "Vattenmelon" , Amount = 0 }
            };

            foreach (var products in product)
            {
                Console.WriteLine($"{products.ProductName}, {products.Price}kr/{products.PriceType}");
            }
            
            Console.WriteLine("KASSA");
            Console.WriteLine($"KVITTO  {DateTime.Now}");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal>");
            Console.WriteLine("PAY");
            Console.Write("Kommando: ");
            Console.ReadKey();
            
            //Readline ProductId SPLIT Antal
            //Lägg till hela produkten till ett objekt som heter kvitto
            //Spara detta till en fil
        }
    }
}
