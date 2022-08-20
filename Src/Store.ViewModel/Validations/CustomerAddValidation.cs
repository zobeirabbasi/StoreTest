using FluentValidation;
using Store.ViewModel.Models;

namespace Store.ViewModel.Validations
{

    public class CustomerAddValidation : AbstractValidator<CustomerAddViewModel>
    {
        private string _emptyMessage = "Please input value";
        public CustomerAddValidation()
        {

            RuleFor(c => c.Name).NotEmpty().WithMessage(_emptyMessage);
            RuleFor(c => c.Email).NotEmpty().WithMessage(_emptyMessage);

            RuleFor(c => c.Email).EmailAddress().WithMessage("The email Address is not correct");
        }
    }
}
