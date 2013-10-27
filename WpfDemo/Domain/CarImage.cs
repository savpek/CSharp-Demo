using WpfDemo.Components;

namespace WpfDemo.Domain
{
    public class CarImage : DataModelBase
    {
        public virtual string RegisterPlate { get; set; }
        public virtual string Color { get; set; }
        public virtual int Speed { get; set; }
    }
}
