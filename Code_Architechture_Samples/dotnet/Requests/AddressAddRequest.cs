using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Addresses
{

   
    public class AddressAddRequest
    {
        [Required]
        public string LineOne { get; set; }

        [Required]
        [Range(1,10000)]
        public int SuiteNumber { get; set; }
       
        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }

        [Range(-90,90)]
        public float Lat { get; set; }

        [Range(-180, 180)]
        public float Long { get; set; }
    }
}
