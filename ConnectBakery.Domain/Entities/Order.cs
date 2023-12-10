

namespace ConnectBakery.Domain.Entities
{
    public class Order 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public Guid? ClientId { get; set; }
        public Client Client { get; set; }
    }
}
