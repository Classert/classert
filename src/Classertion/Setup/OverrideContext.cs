namespace Classertion.Setup
{
    public class OverrideContext<T> where T : class
    {
        private readonly List<OverrideParam> _paramters = new();
        private readonly IClassert<T> _parent;

        internal OverrideContext(IClassert<T> parent, IEnumerable<OverrideParam> parameters)
        {
            _parent = parent;
        }

        public IVerifiable<T> Parent => _parent;

        public P? Get<P>(string name)
        {
            var type = typeof(T);
            var param = _paramters.Find(p => p.Name == name && p.Type == type);

            if (param == null)
            {
                return default;
            }

            return (P)param.Value;
        }
    }
    public class OverrideContext<T, TReturn> where T : class
    {
        private readonly List<OverrideParam> _paramters = new List<OverrideParam>();

        internal OverrideContext(IClassert<T> parent, T value, IEnumerable<OverrideParam> parameters)
        {
        }
    }
}