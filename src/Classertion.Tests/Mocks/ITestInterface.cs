
namespace Classertion.Tests.Mocks
{
    internal interface ITestInterface
    {
        Model Create(Model model);

        Factory Factory { get; }
    }

    public class Model
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class Factory
    {
        private readonly IProvider _provider;

        internal Factory(IProvider provider)
        {
            _provider = provider;
        }
    }

    internal interface IProvider
    {
    }
}