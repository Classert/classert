using System.Collections.Concurrent;

namespace Classertion
{
    public static class Classert
    {
        private static readonly ConcurrentQueue<IVerifiable> _verifiables = new ConcurrentQueue<IVerifiable>();

        static Classert()
        {
        }

        public static Classert<T> Setup<T>() where T : class
        {
            var result = new Classert<T>(ClassertBuilder.Instance.GetBuilderForType<T>());

            _verifiables.Append(result);

            return result;
        }
    }

    public class Classert<T> : Verifiable<T>, IVerifiable<T> where T : class
    {
        private readonly Lazy<T> _lazyObject;

        internal Classert(ITypeBuilder<T> builder) : base(builder)
        {
            var type = builder.Type;

            if (type.IsAbstract)
            {

            }

            if (type.IsSealed || type.IsEnum)
            {
                throw new InvalidOperationException($"Type '{type.Name}' must be a non sealed/enum class.");
            }

            _lazyObject = new Lazy<T>(() =>
            {
                return builder.Build();
            });
        }

        public T Object
        {
            get
            {
                return _lazyObject.Value;
            }
        }
    }
}