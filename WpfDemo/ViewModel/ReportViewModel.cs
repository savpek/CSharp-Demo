using System.Collections.ObjectModel;
using Castle.Core.Internal;
using SqlScriptRunner.GUI;
using WpfDemo.Domain;
using WpfDemo.Messages;
using WpfDemo.Utilities;

namespace WpfDemo.ViewModel
{
    public class ReportViewModel
    {
        private readonly IRepository _dataSource;

        public ReportViewModel(IRepository dataSource, MessageAggregator messegenger)
        {
            Null.CheckArgument(() => dataSource);
            Null.CheckArgument(() => messegenger);

            _dataSource = dataSource;
            CarImages = new ObservableCollection<CarImage>();

            messegenger.Subscribe<FiltersAppliedMessage>(UpdateData);
        }

        private void UpdateData(FiltersAppliedMessage message)
        {
            CarImages.Clear();

            var data = _dataSource.Query<CarImage>();

            foreach (var filter in message.Filters)
            {
                data = filter.Filter(data);
            }

            data.ForEach(x => CarImages.Add(x));
        }

        public ObservableCollection<CarImage> CarImages { get; private set; }
    }
}
