namespace Classertion.Verification.Internal
{
    internal class ClassertBuilder : IClassertBuilder
    {
        private static volatile IClassertBuilder _instance;

        private ClassertBuilder() { }

        static ClassertBuilder()
        {
            _instance = new ClassertBuilder();
        }

        public static IClassertBuilder Instance => _instance;

        ITypeBuilder IClassertBuilder.GetTypeBuilder(Action<SetupArgs>? argsProvider = null)
        {
            return new ClassertTypeBuilder(AssemblyGenerator.Instance, argsProvider);
        }
    }
}