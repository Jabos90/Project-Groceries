using System.Windows;
using System.Windows.Controls;

namespace Project_Groceries.View
{
    /// <summary>
    /// Interaction logic for GroceriesListView.xaml
    /// </summary>
    public partial class GroceriesListView : Window
    {
        public GroceriesListView() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            DialogResult = button.Name == ConfirmButton.Name;
            Close();
        }
    }
}
