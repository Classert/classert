using System.ComponentModel;
using System.Reflection;

namespace Classertion
{
    public interface IClassert : IVerifiable
    {
        TMethod AddMethod<TMethod>(TMethod method) where TMethod : IMethodCall;
    }

    public interface IClassert<T> : IVerifiable<T>, IClassert where T : class
    {
        T Object { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        string TypeName { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetCompiledType();

        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetCompiledType(Assembly assembly);
    }
}

        //public ICallAndReturn<TResult> Method<TResult>(Expression<Func<T, TResult>> expression)
        //{
        //    if (expression.Body is MethodCallExpression methodCall)
        //    {
        //        return new CallAndReturn<TResult>(
        //            Classert.Register(this, new MethodCall<TResult>(methodCall))
        //        );
        //    }

        //    throw new ArgumentException("Parameter must be a method call expression.");
        //}

        //public ICallAndReturn<P, TResult> Method<P, TResult>(Expression<Func<T, TResult>> expression)
        //{
        //    if (expression.Body is MethodCallExpression methodCall)
        //    {
        //        return new CallAndReturn<P, TResult>(
        //            Classert.Register(this, new MethodCall<P, TResult>(methodCall))
        //        );
        //    }

        //    throw new ArgumentException("Parameter must be a method call expression.");
        //}