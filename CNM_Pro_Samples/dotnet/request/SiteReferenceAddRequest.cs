using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.SiteReferences
{
    public class SiteReferenceAddRequest
    {
        [Required]
        [Range(1,9, ErrorMessage = "Please select a Reference Type")]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "User Not Found")]
        public int UserId { get; set; }
    }
}
