using System;
using System.Collections.Generic;
using System.Text;


namespace Sabio.Models.Domain.Addresses
{
    public class Address : BaseAddress
    {
        public int SuiteNumber { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}

