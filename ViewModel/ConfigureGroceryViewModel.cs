using Microsoft.Win32;
using Project_Groceries.Commands;
using Project_Groceries.Model;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_Groceries.ViewModel
{
    class ConfigureGroceryViewModel : ViewModelBase
    {
        private string _imagePath;
        private ImageSource _image;

        public ConfigureGroceryViewModel() : this(string.Empty, decimal.Zero, string.Empty) => Title = "Create Grocery";

        public ConfigureGroceryViewModel(Grocery source) : this(source.Name, source.Price, source.ImagePath) => Title = "Edit Grocery";

        private ConfigureGroceryViewModel(string name, decimal price, string imagePath)
        {
            Name = name;
            Price = price;
            ImagePath = imagePath;

            BrowseCommand = new DelegateCommand(o => BrowseImage());
        }

        public string Title { get; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (ImagePath == value) { return; }

                _imagePath = value;

                Image = Grocery.ParseImage(ImagePath);
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public ImageSource Image
        {
            get => _image;
            private set
            {
                if (Image == value) { return; }

                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ICommand BrowseCommand { get; }

        private void BrowseImage()
        {
            var fileDialog = new OpenFileDialog { Filter = "Image Files|*.bmp; *.gif; *.jpg; *.jpeg; *.png" };
            if (fileDialog.ShowDialog() == true)
            {
                ImagePath = fileDialog.FileName;
            }
        }
    }
}
