using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class Receipt
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int TotalCost { get; set; }
    }
}
