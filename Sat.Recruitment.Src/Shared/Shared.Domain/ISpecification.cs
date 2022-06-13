namespace Shared.Domain
{
    public interface ISpecification<in T>
    {
        bool IsSatisfied(T obj);
    }
}
