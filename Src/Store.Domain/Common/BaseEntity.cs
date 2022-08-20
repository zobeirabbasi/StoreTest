using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime ModifiedDate { get; private set; }
        public BaseEntity()
        {
            ModifiedDate = DateTime.Now;
        }
    }
}
