using System.Reflection.Emit;

namespace Classertion.Internal
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

        ITypeBuilder<T> IClassertBuilder.GetBuilderForType<T>()
        {
            return new ClassertTypeBuilder<T>(this);
        }
    }
}