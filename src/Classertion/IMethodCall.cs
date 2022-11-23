namespace Classertion
{
    public interface IMethodCall<T, TResult> : IVerifiable<T> where T : class
    {
        void Returns(TResult value);
         
        void Calls(Func<MethodCallContext<T>, TResult> func);
    }
}