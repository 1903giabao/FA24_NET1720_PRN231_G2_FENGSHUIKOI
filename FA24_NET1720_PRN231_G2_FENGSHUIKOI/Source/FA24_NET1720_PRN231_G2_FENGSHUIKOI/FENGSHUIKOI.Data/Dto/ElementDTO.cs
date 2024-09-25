using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Data.Dto
{
    public class ElementDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? TabooTime { get; set; }

        public string ImageUrl { get; set; }

        public bool? Status { get; set; }

        public string LuckyNumbers { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string UpdateBy { get; set; }
    }
}
