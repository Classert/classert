using System.Linq.Expressions;

namespace Classertion.Internal
{
    internal class MethodCall<T, TReturn> : Verifiable<T>, IMethodCall<T, TReturn> where T : class
    {
        private static readonly string FullName = $"{typeof(T).Name}.{typeof(TReturn).Name}";

        private readonly IVerifiable<T> _parent;
        private readonly MethodCallExpression _expression;

        private Func<MethodCallContext<T>, TReturn> _call;
        private TReturn? _return;

        internal MethodCall(IVerifiable<T> parent, MethodCallExpression expression, ITypeBuilder<T> builder) : base(builder)
        {
            _parent = parent;
            _expression = expression;
        }

        internal IVerifiable<T> Parent => _parent;

        protected override string Name => typeof(T).Name;

        internal MethodCallExpression Method => _expression;

        void IMethodCall<T, TReturn>.Returns(TReturn value)
        {
            if (_call != null)
            {
                throw new InvalidOperationException("You cannot set a callback and a return.");
            }

            _return = value;
        }

        void IMethodCall<T, TReturn>.Calls(Func<MethodCallContext<T>, TReturn> func)
        {
            if (_return != null)
            {
                throw new InvalidOperationException("You cannot set a callback and a return.");
            }

            _call = func;
        }
    }
}