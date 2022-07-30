using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Friends
{
    public class FriendAddRequestV2
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }
        [Required]
        [MinLength(2)]
        public string Bio { get; set; }
        [Required]
        [MinLength(2)]
        public string Summary { get; set; }
        [Required]
        [MinLength(2)]
        public string Headline { get; set; }
        [Required]
        [MinLength(2)]
        public string Slug { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public ImageAddRequest PrimaryImage { get; set; }
       

    }
}
