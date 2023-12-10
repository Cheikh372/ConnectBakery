using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectBakery.Application.Shared.Dtos
{
    public class StockDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; } // Clé étrangère vers Product
        public int Quantity { get; set; }
        public DateTime UpdateAt { get; set; }
        //public ProductDto Product { get; set; }
    }
}
