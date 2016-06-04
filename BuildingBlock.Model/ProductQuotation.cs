using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ProductQuotation
    {
        public ProductQuotation()
        {
            Status = ProductQuotationStatus.Requested;
            RequestDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime RequestDate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

        public Guid RequestorUserId { get; set; }

        public int? SellerId { get; set; }
        public ProductSeller Seller { get; set; }

        public string Message { get; set; }

        public float Price { get; set; }

        public ProductQuotationStatus Status { get; set; }

        public ICollection<ProductQuotationActivity> Activity { get; set; }


    }

    public enum ProductQuotationStatus
    {
        Requested = 1,
        Updated,
        Solved
    }
}
