namespace Classertion.Internal
{
    public abstract class Verifiable : IVerifiable
    {
        private protected Verifiable()
        {
        }

        protected abstract string Name { get; }

        string IVerifiable.Name => Name;

        void IVerifiable.Verify()
        {
            Verify();
        }

        protected abstract void Verify();
    }
}