namespace Classertion.Verification
{
    public interface ITypeBuilder
    {
        T BuildType<T>(IClassert<T> target) where T : class;
    }
}