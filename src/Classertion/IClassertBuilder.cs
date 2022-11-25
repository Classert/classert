namespace Classertion
{
    public interface IClassertBuilder
    {
        ITypeBuilder GetTypeBuilder(Action<SetupArgs>? argsProvider = null);
    }
}