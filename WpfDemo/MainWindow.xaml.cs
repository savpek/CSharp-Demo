using System.Windows;
using WpfDemo.Domain;
using WpfDemo.ViewModel;

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
            var reportViewModel = new ReportViewModel(dataSource, null);
            DataContext = reportViewModel;
        }
    }
}
