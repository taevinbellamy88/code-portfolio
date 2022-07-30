using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.ExternalLinks
{
    public class ExternalLinkUpdateRequest : ExternalLinkAddRequest, IModelIdentifier
    {
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Invalid External Link Id")]
        public int Id { get; set; }
       
    }
}
