namespace ConnectBakery.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; } // Clé étrangère vers Order
        public Guid ProductId { get; set; } // Clé étrangère vers Product
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
