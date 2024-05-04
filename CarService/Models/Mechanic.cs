/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Mechanic : Person
    {
        [Display(Name = "Experience (years)")]
        public int Experience { get; set; }
        public string Speciality { get; set; }
        public DateTime StartDate { get; set; }
        [Display(Name = "PayRate($)")]
        public decimal PayRate { get; set; }

        //Mechanic and Service are in 1:M realtionship
        public virtual ICollection<Service> Services { get; set; }

    }
}
