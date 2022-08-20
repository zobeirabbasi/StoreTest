using Store.Domain.Common;

namespace Store.Domain.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        bool IsExistCustomer(Customer model);
    }
}
