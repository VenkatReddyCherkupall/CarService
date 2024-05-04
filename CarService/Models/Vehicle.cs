/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        [Display (Name = "VIN #")]
        [MaxLength(10)]
        [RegularExpression("^[0-9]+$", ErrorMessage = " VIN Number shoudld be 0-9 characters only. ")]
        public string VinNumber { get; set; }
        [MaxLength(15)]
        public string Make { get; set; }
        [MaxLength(15)]
        public string Model { get; set; }
        [MaxLength(4)]
        public string Year { get; set; }
        [MaxLength(15)]
        public string Color { get; set; }
        
        //foreign key nullable
        public int? CustomerID { get; set; }
        //Lazy loading
        public virtual Customer Customer { get; set; }

        //vehicle and service are in 1:M relationship
        public virtual ICollection<Service> Services { get; set; }
    }
}
