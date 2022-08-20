using Store.Domain.Models;
using Store.ServiceLayer.Contracts;
using Store.ViewModel.Models;

namespace Store.ServiceLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// create a new customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateAsync(CustomerAddViewModel model)
        {
            var customer = MapViewModelToModel(model);
            await _repository.CreateAsync(customer);
            await _repository.SaveChangesAsync();

        }

        /// <summary>
        /// deleting customer by CustomerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            var customer = await _repository.GetAsync(id);

            await _repository.RemoveAsync(customer);
            await _repository.SaveChangesAsync();

        }

        /// <summary>
        /// return a customer by customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerViewModel> GetByIdAsync(long id)
        {
            var customer = await _repository.GetAsync(id);

            if (customer == null) return new CustomerViewModel();
            var result = new CustomerViewModel
            {
                Name = customer.Name,
                Email = customer.Email,

                Id = customer.Id
            };
            return result;

        }

        /// <summary>
        /// Editing a Customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task UpdateAsync(CustomerViewModel model)
        {
            var customer = await _repository.GetAsync(model.Id);
            if (customer != null)
            {
                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Id = model.Id;

                await _repository.UpdateAsync(customer);
                await _repository.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        /// <summary>
        /// Getting list of customers
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            var result = customers.Select(m => new CustomerViewModel
            {
                Name = m.Name,
                Email = m.Email,
                Id = m.Id

            }).ToList();
            return result;
        }

        /// <summary>
        /// finding duplicate customer record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsCustomerExist(CustomerAddViewModel model)
        {
            var isExistCustomer = _repository.IsExistCustomer(MapViewModelToModel(model));
            return isExistCustomer;
        }

        /// <summary>
        /// private method to mapping model to view model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Customer MapViewModelToModel(CustomerAddViewModel model)
        {
            var customer = new Customer
            {
                Name = model.Name,
                Email = model.Email,
                Id = model.Id
            };
            return customer;
        }
    }
}
