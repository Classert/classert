using System.ComponentModel;
using System.Linq.Expressions;

namespace Classertion
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IVerifiable : IFluentInterface
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Name { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Verify();
    }
}