using System.Windows;
using System.Windows.Controls;

namespace Project_Groceries.View
{
    /// <summary>
    /// Interaction logic for ConfigureGroceryView.xaml
    /// </summary>
    public partial class ConfigureGroceryView : Window
    {
        public ConfigureGroceryView() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            DialogResult = button.Name == ConfirmButton.Name;
            Close();
        }
    }
}
