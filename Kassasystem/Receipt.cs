using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class Receipt: Products
    {
        public int TotalCost { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
