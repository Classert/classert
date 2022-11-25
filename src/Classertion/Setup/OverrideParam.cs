namespace Classertion.Setup
{
    internal class OverrideParam
    {
        public OverrideParam(string name, object value, Type? type = default)
        {
            var getType = (object val) =>
                type
                    ?? value?.GetType()
                    ?? typeof(object);

            Name = name;
            Value = value;
            Type = getType(value);
        }
        public string Name { get; set; }

        public object Value { get; set; }

        public Type Type { get; set; }
    }
}