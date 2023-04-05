namespace BikeRentalSystem.Models
{
    public class RentalPoint : IEntity
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Vehicle> vehicles { get; set; }
        public int Id { get; set; }
    }
}
