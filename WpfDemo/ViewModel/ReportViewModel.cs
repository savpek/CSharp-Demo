using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfDemo.Annotations;
using WpfDemo.Domain;

namespace WpfDemo.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        public ReportViewModel(InMemoryRepository dataSource)
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
