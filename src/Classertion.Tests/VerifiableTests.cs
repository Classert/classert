using Classertion.Tests.Mocks;

namespace Classertion.Tests
{
    [TestClass]
    public class VerifiableTests
    {
        private IClassert<ITestInterface> _interface;
        private IClassert<Factory> _factory;
        private IClassert<IProvider> _provider;

        private Controller _controller;

        [TestInitialize]
        public void Initialize()
        {
            _interface = Classert.Setup<ITestInterface>();
            _provider = Classert.Setup<IProvider>();
            _factory = Classert.Setup<Factory>(args => {
                args.Args = _provider.Object;
            });


            _factory.Override(f => f.GoDoYourWork(/* o1 T.Val<T> */null))
                .ToThrow(new NullReferenceException());
            
            _factory.Override(f => f.GoDoWork()).Calls(c =>
            {
                var work = new Work("I overwrote the original method to call this.");
            });

            // TODO: Add and It.IsAny<T> of some sort for mocking parameters.

            _factory.Override(f => f.GoDoYourWork(/* o1 T.Val<Object1> */null, /* o2 T.Val<Object2> */null)).Calls(c =>
            {
                var obj1 = c.Get<object>("o1"); // f.GoDoYourWork o1
                var obj2 = c.Get<object>("o2"); // f.GoDoYourWork o2
                
                // TODO: I don't know if we should allow them to return here?
                return new Work("I overwrote the original result");
            });

            _interface.Override(i => i.Factory).Returns(() => _factory.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            _controller = new Controller(_interface.Object, _factory.Object, _provider.Object);

            // Act
            _controller.DoSomeWork();

            // Assert
            _factory.Verify();
        }
    }
}