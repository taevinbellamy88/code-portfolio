using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Friends
{
    public class ImageAddRequest
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        [MinLength(2)]
        public string Url { get; set; }
    }
}
