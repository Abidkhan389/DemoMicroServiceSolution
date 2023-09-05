using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.domain.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ProductPrice { get; set; }
        public int Status { get; set; }
        public Product()
        {
            // You can initialize default values or perform other setup here if needed.
            this.ProductId = Guid.NewGuid();
            this.Status = 1;
        }
        public Product(Product model)
        {
            if (model.ProductId == Guid.Empty || model.ProductId == null)
                this.ProductId = Guid.NewGuid();
            ProductName = model.ProductName;
            ProductCode = model.ProductCode;
            ProductPrice = model.ProductPrice;
            Status = 1;
        }
    }
}
