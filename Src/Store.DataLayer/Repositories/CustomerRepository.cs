using Store.DataLayer.Common;
using Store.Domain.Models;

namespace Store.DataLayer.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        ApplicationContext _dbContext;
        public CustomerRepository(ApplicationContext context) : base(context)
        {
            _dbContext = context;
        }

        public bool IsExistCustomer(Customer model)
        {
            return _dbContext.Customers.Any(m => m.Email == model.Email
               );
        }

    }
}
