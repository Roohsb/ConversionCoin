using System;
using System.ComponentModel.DataAnnotations;

namespace Conversion.Class
{
    public class Converts

    {
        [Key] public int IDTransaction { get; set; }

        public int IDUser { get; set; }

        public string CurrencyOrigin { get; set; }

        public double OriginValue { get; set; }

        public string DestCurrency { get; set; }

        public double DestValue { get; set; }

        public double Rate { get; set; }

        public DateTime Date_Time { get; set; }
    }
}