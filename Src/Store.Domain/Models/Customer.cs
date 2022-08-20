using Store.Domain.Common;

namespace Store.Domain.Models
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
