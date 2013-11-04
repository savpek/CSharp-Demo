using System;

namespace WpfDemo.Domain.Settings.Attributes
{
    public class ConfigFileParameterAttribute : Attribute
    {
        public string Identifier { get; private set; }

        public ConfigFileParameterAttribute(string identifier)
        {
            Identifier = identifier;
        }
    }
}
