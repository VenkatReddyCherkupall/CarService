/*VenkatReddy Cherkupalli*/

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarService.Models
{
    public class PartsUsed
    {
        public int PartsUsedID { get; set; }
        [MaxLength(15)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Part Name must be 15 letters and It shoudld be a-z, A-Z only. ")]
        public string PartName { get; set; }
        [MaxLength(5)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage =" Part Number must be 5 letters and It should be 0-9 characters only.")]
        public string PartNumber { get; set; }
        public decimal Cost { get; set; }
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Description must be 30 letters and It shoudld be a-z, A-Z only. ")]
        public string Description { get; set; }

        //forign key nullable
        public int? ServiceID { get; set; }

        //lazy loading
        public virtual Service Service { get; set; }
    }
}
