using System.Linq.Expressions;

namespace Classertion.Verification.Internal
{
    public class MethodCall : Verifiable, IMethodCall
    {
        private readonly MethodCallExpression _expression;

        private Action? _method;
        private Exception? _exception;

        internal MethodCall(MethodCallExpression expression)
        {
            _expression = expression;
        }

        internal MethodCallExpression Expression => _expression;

        MethodCallExpression IMethodCall.Expression => Expression;

        protected override void Verify()
        {
            throw new NotImplementedException();
        }

        void IMethodCall.Call(Action method)
        {
            _method = method;
        }

        void IMethodCall.Throws(Exception exception)
        {
            _exception = exception;
        }
    }

    public class MethodCall<T> : MethodCall, IMethodCall<T> where T : class
    {
        private Action<OverrideContext<T>>? _action;
        private Func<OverrideContext<T>>? _method;

        internal MethodCall(MethodCallExpression expression) : base(expression)
        {
        }

        void IMethodCall<T>.Call(Action<OverrideContext<T>> action)
        {
            _action = action;
        }

        void IMethodCall.Call(Action method)
        {
            throw new NotImplementedException();
        }
    }

    internal class MethodCall<T, TReturn> : MethodCall<T>, IMethodCall<T, TReturn> where T : class
    {
        private Action<OverrideContext<T, TReturn>>? _action;
        private Func<OverrideContext<T, TReturn>, TReturn>? _func;

        internal MethodCall(MethodCallExpression expression): base(expression)
        {
        }

        void IMethodCall<T, TReturn>.Call(Action<OverrideContext<T, TReturn>> method)
        {
            throw new NotImplementedException();
        }

        void IMethodCall<T, TReturn>.Call(Func<OverrideContext<T, TReturn>, TReturn> method)
        {
            _func = method;
        }

        void IMethodCall<T>.Call(Action<OverrideContext<T>> method)
        {
            _action = method;
        }
    }
}