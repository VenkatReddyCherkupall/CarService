/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;

namespace CarService.Models

{
    public class Person
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage ="First Name must be 15 letters and It shoudld be a-z, A-Z only. ")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name must be 15 letters and It shoudld be a-z, A-Z only. ")]
        public string LName { get; set; }

        [MaxLength(11)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number must be 11 letters and It shoudld be 0-9 only. ")]

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
