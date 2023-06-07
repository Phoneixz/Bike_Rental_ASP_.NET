namespace BikeRentalSystem.Models
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public string Status { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        
        public double TotalCost { get; set; }
    }
}
