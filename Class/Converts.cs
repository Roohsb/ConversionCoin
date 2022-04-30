using System;
using System.ComponentModel.DataAnnotations;

namespace Conversion.Class
{
    public class Converts

    {
        [Key]
        public int IDTransaction { get; set; }

        public int IDUser { get; set; }

        public string CurrencyOrigin { get; set; }

        public decimal OriginValue { get; set; }

        public string DestCurrency { get; set; }

        public decimal DestValue { get; set; }

        public double Rate { get; set; }

        public DateTime Date_Time { get; set; }


    }
}
