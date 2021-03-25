using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Models
{
    public class Customer: IdentityUser
    {
        public int premisesNumber { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string AccountNum { get; set; }
    }
}
