using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _00003741_DBSD_CW2.Models
{
    public class Suggestion
    {            
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int LeftInStock { get; set; }
        public int NumberOfCustomerBought { get; set; }
    }
}