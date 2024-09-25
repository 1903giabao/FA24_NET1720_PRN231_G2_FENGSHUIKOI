using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Data.Dto
{
    public class ComboRequestDTO
    {
        public int Id { get; set; }

        public int? MemberId { get; set; }

        public int? ElementId { get; set; }

        public int? ProductDetailId { get; set; }

        public string ComboName { get; set; }

        public decimal ComboPrice { get; set; }

        public decimal? Discount { get; set; }

        public bool? Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
