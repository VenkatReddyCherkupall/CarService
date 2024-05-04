/*VenkatReddy Cherkupalli*/


using CarService.Models;

namespace CarService.ViewModels
{
    public class CustomerSearchViewModel
    {
        public Customer Customer { get; set; }
        public string SearchError { get; set; }
        public List<Customer> ResultList { get; set; }
        public CustomerSearchViewModel()
        {
            ResultList = new List<Customer>();
        }
    }
}
