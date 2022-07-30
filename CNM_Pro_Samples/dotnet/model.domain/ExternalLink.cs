using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class ExternalLink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public LookUp UrlTypes { get; set; }
        public string Url { get; set; }
        public int EntityId { get; set; }
        public LookUp EntityTypes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
