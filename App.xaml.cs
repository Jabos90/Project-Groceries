using Project_Groceries.ViewModel;
using System.Windows;

namespace Project_Groceries
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var view = new MainView();
            var viewModel = new MainViewModel();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
