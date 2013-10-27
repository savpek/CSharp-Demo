using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using SqlScriptRunner.GUI;
using WpfDemo.Domain;
using WpfDemo.ViewModel;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Use this as construction root for simplicity.
            // In real world app there should be good framework for this and this should be done
            // outside of main window for reason that then mainwindow is interchangeable too if required.
            var dataSource = new InMemoryRepository();

            // Dummy data for testing...
            dataSource.Add(new CarImage {Color="Black", RegisterPlate = "ABC-123", Speed = 140});

            var messenger = new MessageAggregator();
            var reportViewModel = new ReportViewModel(dataSource, messenger);
            var colorFilter = new ColorFilterViewModel();
            var filterViewModel = new FiltersViewModel(new List<IFilter> {colorFilter}, messenger);
            
            // Just for testing, apply dummy filter so that data is shown.
            messenger.Publish(new Messages.FiltersAppliedMessage());

            DataContext = new
            {
                Report = reportViewModel,
                Filters = filterViewModel
            };
        }
    }
}
