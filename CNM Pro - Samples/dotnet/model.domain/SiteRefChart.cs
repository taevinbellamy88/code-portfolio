using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Domain
{
    public class SiteRefChart
    {
        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Search { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Google { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Facebook { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int OtherSocial { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Email { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int WordOfMouth { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Recruiter { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int JobFair { get; set; }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Other { get; set; }

    }
}
