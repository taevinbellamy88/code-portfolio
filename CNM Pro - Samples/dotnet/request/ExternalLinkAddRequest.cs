using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.ExternalLinks
{
    public class ExternalLinkAddRequest 
    {
        [Required]
        [Range(int.MinValue,int.MaxValue, ErrorMessage = "Invalid UserId")]
        public int UserId { get; set; }
        
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Invalid Url Type")]
        public int UrlTypeId { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "Invalid Url, Please Correct Url")]
        public string Url { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Invalid Entity Id")]
        public int EntityId { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Invalid Entity Type")]
        public int EntityTypeId { get; set; }
        
    }
}
