using System;

namespace WpfDemo.Domain.Settings.Attributes
{
    public class PersistedParameterAttribute : Attribute
    {
        public PersistedParameterAttribute(uint id)
        {
            Id = id;
        }
        public uint Id { get; private set; }
    }
}
