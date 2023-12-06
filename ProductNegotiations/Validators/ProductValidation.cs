using FluentValidation;
using ProductNegotiations.API.Models;

namespace ProductNegotiations.API.Validators
{
    public class ProductValidation : AbstractValidator<ProductClientModel>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Name).MinimumLength(1);
        }
    }
}
