namespace BikeRentalSystem.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Make { get; set; }
 
        public string Description { get; set; }

        public double Price { get; set; }

        public string ImgURL { get; set; }
        
        public int VehicleTypeID { get; set; }
        public VehicleType VehicleType { get; set; }

        public bool Availability { get; set; }
    }
}
