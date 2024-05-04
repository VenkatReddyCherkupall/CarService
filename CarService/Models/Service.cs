/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public DateTime ServiceDate { get; set; }
        [MaxLength(50)]

        public string Description { get; set; }
        public decimal ServiceCost { get; set; }

        //forign key nullable
        public int? MechanicID { get; set; }
        public int? VehicleID { get; set; }

        //Lazy loading
        public virtual Mechanic Mechanic { get; set; }
        public virtual Vehicle Vehicle { get;set; }

        //Service and PartsUsed are in 1:M realitonship
        public virtual ICollection<PartsUsed> PartsUsed { get; set; }
    }
}
