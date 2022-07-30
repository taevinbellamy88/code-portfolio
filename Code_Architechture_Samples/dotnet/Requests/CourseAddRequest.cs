using Sabio.Models.CodeChallenge.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.CodeChallenge.Requests
{
    public class CourseAddRequest
    {


        [Required]
        public string Name { get; set; }
       
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Only Positive Numbers Allowed")]
        public int SeasonTermId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only Positive Numbers Allowed")]
        public int TeacherId { get; set; }

        public List<Students> Students { get; set; }    

       

    }
}
