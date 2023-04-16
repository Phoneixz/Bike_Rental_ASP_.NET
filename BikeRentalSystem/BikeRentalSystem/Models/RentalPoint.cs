namespace BikeRentalSystem.Models
{
    public class RentalPoint : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

    }
}
