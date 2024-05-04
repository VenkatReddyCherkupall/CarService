/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Customer : Person
    {
        [Display(Name ="Insurance#")]
        [MaxLength(10)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Insurance number must be 10 characters and It shoudld be 0-9 only. ")]
        public string InsuranceNumber { get; set; }


        public string InsuranceCompany { get; set; }
        public int DriversLicenceNumber { get; set; }

        // customer and vehicle are in 1:M relationship.
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
