namespace BikeRentalSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime? PickUpDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public Vehicle Vehicle { get; set; }
        
        public decimal TotalCost { get; set; }
    }
}
