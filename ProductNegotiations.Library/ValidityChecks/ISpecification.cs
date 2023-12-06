namespace ProductNegotiations.Library.ValidityChecks
{
    public interface ISpecification<T>
    {
        Task<bool> IsSatisfied(T entity);
    }
}
