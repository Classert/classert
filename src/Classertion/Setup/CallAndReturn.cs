//using Classertion.Verification.Internal;
//using System.ComponentModel;

//namespace Classertion.Internal
//{
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    internal class CallAndReturn<T> : ICallAndReturn<T>
//    {
//        internal CallAndReturn(IMethodCall<T> method)
//        {
//            MethodCall = method;
//        }

//        public IMethodCall<T> MethodCall { get; }

//        internal void Calls(Func<T> method)
//        {
//            MethodCall.Call(method);
//        }
//    }

//    [EditorBrowsable(EditorBrowsableState.Never)]
//    internal class CallAndReturn<T, TReturn> : ICallAndReturn<T, TReturn>
//    {
//        internal CallAndReturn(MethodCall<T, TReturn> method)
//        {
//            MethodCall = method;
//        }

//        public IMethodCall<T, TReturn> MethodCall { get; }

//        internal void Calls(Func<T, TReturn> method)
//        {
//            MethodCall.Call(method);
//        }
//    }

//    [EditorBrowsable(EditorBrowsableState.Never)]
//    internal class CallAndReturn<T, T2, TReturn> : ICallAndReturn<T, T2, TReturn>
//    {
//        internal CallAndReturn(IMethodCall<T, T2, TReturn> method)
//        {
//            MethodCall = method;
//        }

//        public IMethodCall<T, T2, TReturn> MethodCall { get; }

//        internal void Calls(Func<T, T2, TReturn> method)
//        {
//            MethodCall.Call(method);
//        }
//    }
//}