using ConnectBakery.Domain.EnttityBase;

namespace ConnectBakery.Domain.Entities
{
    public class Employe : Personne
    {
        public string Qualification { get; set; } = string.Empty;
        public decimal Salaire { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
