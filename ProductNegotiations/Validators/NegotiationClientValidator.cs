using FluentValidation;
using ProductNegotiations.API.Models;

namespace ProductNegotiations.API.Validators
{
    public class NegotiationClientValidator : AbstractValidator<NegotiationClientCreateModel>
    {
        public NegotiationClientValidator()
        {
            RuleFor(x => x.ProposedPrice).GreaterThan(0);
            RuleFor(x => x.AdditiionalInformations).MaximumLength(250);
            RuleFor(x => x.Product).NotNull();
            RuleFor(x => x.Product.Id).NotNull();
        }
    }
}
