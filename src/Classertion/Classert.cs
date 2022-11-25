using System.Collections.Concurrent;
using System.Reflection;

namespace Classertion
{
    public static class Classert
    {
        private static readonly ConcurrentQueue<IVerifiable> _verifiables = new ConcurrentQueue<IVerifiable>();

        static Classert()
        {
        }

        internal static IClassertBuilder Builder
        {
            get { return ClassertBuilder.Instance; }
        }

        public static IClassert<T> Setup<T>(Action<SetupArgs>? argsProvider = null) where T : class
        {
            var result = new Classert<T>(Builder.GetTypeBuilder(argsProvider));

            _verifiables.Append(result);

            return result;
        }

        internal static TMethod Register<TMethod>(IClassert classert, TMethod methodCall) where TMethod : IMethodCall
        {
            return classert.AddMethod(methodCall);
        }

        //internal static PropertyAccess<T, TResult>? Register<T, TResult>(IClassert<T> classert, PropertyAccess<T, TResult> propertyAccess) where T : class
        //{
        //    return (classert as Classert<T>)?.AddProperty(propertyAccess);
        //}
    }

    internal class Classert<T> : Verifiable<T>, IClassert<T>, IVerifiable<T> where T : class
    {
        private readonly List<IMethodCall> Methods = new List<IMethodCall>();
        //private readonly List<IVerifiable<T>> Properties = new List<IVerifiable<T>>();

        private readonly Lazy<T> _lazyObject;
        private Type? _compiledType;

        internal Classert(ITypeBuilder builder)
        {
            var type = typeof(T);

            if (type.IsSealed || type.IsEnum)
            {
                throw new InvalidOperationException($"Type '{type.Name}' must be a non sealed/enum class.");
            }

            _lazyObject = new Lazy<T>(() => builder.BuildType(this));
        }

        internal Type? GetCompiledType() => _compiledType;

        Type? IClassert<T>.GetCompiledType() => _compiledType;

        string IClassert<T>.TypeName => base.TypeName;

        public T Object => _lazyObject.Value;

        internal void SetCompiledType(Assembly assembly) => _compiledType = assembly.ExportedTypes.First(t => t.Name == TypeName);

        void IClassert<T>.SetCompiledType(Assembly assembly) => SetCompiledType(assembly);

        TMethod IClassert.AddMethod<TMethod>(TMethod methodCall)
        {
            Methods.Add(methodCall);
            return methodCall;
        }
    }
}