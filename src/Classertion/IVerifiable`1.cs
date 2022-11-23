using System.Linq.Expressions;

namespace Classertion
{
    public interface IVerifiable<T> : IVerifiable where T : class
    {
        IMethodCall<T, TResult> Method<TResult>(Expression<Func<T, TResult>> methodCall);
    }
}