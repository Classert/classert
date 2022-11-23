namespace Classertion
{
    public class MethodCallContext<T> where T : class
    {
        internal MethodCallContext()
        {
            ParentType = typeof(T);
        }

        public Type ParentType { get; private set; }
    }
}