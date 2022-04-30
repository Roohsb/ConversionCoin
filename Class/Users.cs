using System;
using System.ComponentModel.DataAnnotations;

namespace Conversion.Class
{
    public class Users
    {
     [Key]
        public int IDUser { get; set; }

        public string Name { get; set; }
        public string email { get; set; }
        public string Telephone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
