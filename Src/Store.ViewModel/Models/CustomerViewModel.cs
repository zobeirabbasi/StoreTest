using System.ComponentModel.DataAnnotations;

namespace Store.ViewModel.Models
{
    public class CustomerViewModel
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
