using Classertion.Tests.Mocks;

namespace Classertion.Tests
{
    [TestClass]
    public class VerifiableTests
    {
        private Classert<ITestInterface> _interface;

        [TestInitialize]
        public void Initialize()
        {
            _interface = Classert.Setup<ITestInterface>();
            _interface.Method(t => t.Create(new Model())).Calls(c => c != null ? new Model() : new Model());
        }

        [TestMethod]
        public void TestMethod1()
        {
            var obj = _interface.Object;
        }
    }
}