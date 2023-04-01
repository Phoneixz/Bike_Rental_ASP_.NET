namespace BikeRentalSystem.Models
{
    public class RentalPoint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Vehicle> vehicles { get; set; } 
    }
}
