using System;
using System.ComponentModel;

namespace WpfDemo.Domain.Settings.Attributes
{
    public class ParameterInformationAttribute : Attribute
    {
        public ParameterInformationAttribute(string tittle)
        {
            Tittle = tittle;
            Description = "";
        }

        public string Tittle { get; set; }
        public string Description { get; set; }
    }
}
