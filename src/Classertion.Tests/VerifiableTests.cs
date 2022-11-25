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

            _factory.Override(f => f.GoDoWork()).Calls(c =>
            {
                var work = new Work("I overwrote the original result");
            });

            _factory.Override(f => f.GoDoYourWork()).Calls(c => { return new Work("I overwrote the original result"); });

            _interface.Override(i => i.Factory).Returns(() => _factory.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            _controller = new Controller(_interface.Object, _factory.Object, _provider.Object);

            // Act

            // Assert
        }
    }
}