using System.Linq.Expressions;

namespace Classertion
{
    public static partial class ClassertExtensions
    {
        public static IOverride<T> Override<T>(this IClassert<T> classert, Expression<Action<T>> expression) where T : class
        {
            if (expression.Body is MethodCallExpression methodCall)
            {
                return new Override<T>(
                    Classert.Register(classert, new MethodCall<T>(methodCall))
                );
            }

            throw new ArgumentException("Parameter must be a method call expression.");
        }

        public static IOverride<T, TResult> Override<T, TResult>(this IClassert<T> classert, Expression<Func<T, TResult>> expression) where T : class
        {
            if (expression.Body is MethodCallExpression methodCall)
            {
                return new Override<T, TResult>(
                    Classert.Register(classert, new MethodCall<T, TResult>(methodCall))
                );
            }

            throw new ArgumentException("Parameter must be a method call expression.");
        }
    }
}
