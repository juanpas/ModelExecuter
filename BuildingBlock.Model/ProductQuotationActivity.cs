using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ProductQuotationActivity
    {
        public ProductQuotationActivity()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int QuotationId { get; set; }
        public ProductQuotation Quotation { get; set; }

        public Guid SourceUserId { get; set; }
        public Guid? TargetUserId { get; set; }

        public bool MessageSent { get; set; }
        public bool PublicMessage { get; set; }
        public string Message { get; set; }

        public ProductQuotationStatus NewStatus { get; set; }

        public ICollection<File> Attachments { get; set; }

    }
}
