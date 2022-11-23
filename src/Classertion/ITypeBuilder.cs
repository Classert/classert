namespace Classertion
{
    public interface ITypeBuilder<T>
    {
        Type Type { get; }

        T Build();
    }
}