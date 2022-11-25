//using Classertion.Verification;
//using Classertion.Verification.Internal;

//namespace Classertion.Internal
//{
//    public class Call : ICall
//    {
//        private readonly IMethodCall _method;

//        internal Call(IMethodCall method)
//        {
//            _method = method;
//        }

//        internal void Calls(Action method)
//        {
//            _method.Call(method);
//        }
//    }

//    public class Call<T> : ICall<T>
//    {
//        internal Call(IMethodCall<T> method)
//        {
//            MethodCall = method;
//        }

//        public IMethodCall<T> MethodCall { get; }

//        internal void Calls(Action<T> method)
//        {
//            MethodCall.Call(method);
//        }

//        internal void Calls(Func<T> method)
//        {
//            MethodCall.Call(method);
//        }
//    }

//    public class Call<T, T2> : ICall<T, T2>
//    {
//        internal Call(IMethodCall<T, T2> method)
//        {
//            MethodCall = method;
//        }

//        public IMethodCall<T, T2> MethodCall { get; }

//        internal void Calls(Action<T, T2> method)
//        {
//            MethodCall.Call(method);
//        }

//        internal void Calls(Func<T, T2> method)
//        {
//            MethodCall.Call(method);
//        }
//    }
//}