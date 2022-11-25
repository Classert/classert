using System.ComponentModel;

namespace Classertion.Verification
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IVerifiable : IFluentInterface
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Verify();
    }

    public interface IVerifiable<T> : IVerifiable
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type Type { get; }
    }
}