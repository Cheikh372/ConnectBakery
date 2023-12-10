

using ConnectBakery.Common.Enum;
using ConnectBakery.Domain.EnttityBase;

namespace ConnectBakery.Domain.Entities
{
    public class Client : Personne
    {
       
        public ClientType ClientType { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
