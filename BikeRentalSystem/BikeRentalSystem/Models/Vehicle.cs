namespace BikeRentalSystem.Models
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }

        public string Make { get; set; }
 
        public string Description { get; set; }

        public double Price { get; set; }

        public string ImgURL { get; set; }
        
        public int VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }

        public bool Availability { get; set; }
        public virtual RentalPoint RentalPoint { get; set; }
        public int RentalPointId { get; set; }
    }
}
