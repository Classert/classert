using System.Linq.Expressions;

namespace Classertion.Internal
{
    public class Verifiable<T> : Verifiable, IVerifiable<T> where T : class
    {
        private static Dictionary<string, IVerifiable> Children = new Dictionary<string, IVerifiable>();

        internal Verifiable(ITypeBuilder<T> builder) : base()
        {
            Builder = builder;
        }

        protected ITypeBuilder<T> Builder { get; }

        protected override string Name => typeof(T).Name;

        public IMethodCall<T, TResult> Method<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (expression.Body is not MethodCallExpression methodCall)
            {
                throw new ArgumentException("Parameter must be a method call expression.");
            }

            return Register(new MethodCall<T, TResult>(this, methodCall, Builder));
        }

        protected internal IMethodCall<T, TResult> Register<TResult>(IMethodCall<T, TResult> methodCall)
        {
            Children[methodCall.Name] = methodCall;
            
            return methodCall;
        }

        protected override void Verify()
        {
            throw new NotImplementedException();
        }
    }
}