namespace ConnectBakery.Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; } // Clé étrangère vers Product
        public int Quantity { get; set; }
        public DateTime UpdateAt { get; set; }
        public Product Product { get; set; }
    }
}
