﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class Products
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string PriceType { get; set; }

        //public double GetPrice()
        //{
        //    var totalprice = 0;

        //    totalprice = Price * Amount;

        //    return totalprice;
        //}
    }
}
