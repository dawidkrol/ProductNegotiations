using Microsoft.Extensions.Logging;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.Services;

namespace ProductNegotiations.Library.ValidityChecks
{
    public static class NegotiationValidityCheck
    {
        public static bool Check(this NegotiationModel model, ILogger logger, INegotiaitionService negotiaitionService, IProductService productService, Action<CheckValues>? options = null)
        {
            var values = new CheckValues();

            options?.Invoke(values);

            bool output = false;

            var specifications = GetSpecifications(logger, negotiaitionService, productService, values);

            output = specifications.All(x => x.IsSatisfied(model).GetAwaiter().GetResult());

            return output;
        }

        private static List<ISpecification<NegotiationModel>> GetSpecifications(ILogger logger, INegotiaitionService negotiaitionService, IProductService productService, CheckValues values)
        {

            var specifications = new List<ISpecification<NegotiationModel>>();
            if (values?.MaxAttempts != null)
                specifications.Add(new AttemptSpecification(logger, productService, negotiaitionService, values.MaxAttempts.Value));
            if (values?.MaxTimesLowerPrice != null)
                specifications.Add(new PriceSpecification(logger, productService, negotiaitionService, values.MaxTimesLowerPrice.Value));

            return specifications;
        }
    }
}
