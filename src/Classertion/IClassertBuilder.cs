namespace Classertion
{
    public interface IClassertBuilder
    {
        ITypeBuilder<T> GetBuilderForType<T>();
    }
}