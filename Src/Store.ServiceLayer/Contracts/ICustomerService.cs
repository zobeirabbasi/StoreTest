using Store.ViewModel.Models;

namespace Store.ServiceLayer.Contracts;

public interface ICustomerService
{
    Task CreateAsync(CustomerAddViewModel model);
    Task DeleteAsync(long id);
    Task<CustomerViewModel> GetByIdAsync(long id);
    Task UpdateAsync(CustomerViewModel model);
    Task<List<CustomerViewModel>> GetAllAsync();
    bool IsCustomerExist(CustomerAddViewModel model);
}