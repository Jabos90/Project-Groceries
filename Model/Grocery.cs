using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_Groceries.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    class Grocery : ModelBase
    {
        private int _quantity;

        [JsonProperty]
        public string Name { get; private set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) { return; }

                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        [JsonProperty]
        public decimal Price { get; private set; }

        public decimal TotalPrice => Quantity * Price;

        [JsonProperty]
        public string ImagePath { get; private set; }

        public ImageSource Image { get; }

        private static ImageSource PlaceholderImage { get; } = new BitmapImage(new Uri("pack://application:,,,/Resources/Questionmark.png"));

        [JsonConstructor]
        public Grocery(string name, decimal price, string imagePath)
        {
            Name = name;
            Price = price;
            ImagePath = imagePath;
            Image = ParseImage(ImagePath);
        }

        public Grocery(Grocery source)
        {
            Name = source.Name;
            Price = source.Price;
            ImagePath = source.ImagePath;
            Image = source.Image;
            Quantity = source.Quantity;
        }

        public static ImageSource ParseImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) { return PlaceholderImage; }

            // Check if local path
            if (!Path.IsPathRooted(imagePath))
            {
                imagePath = Path.GetFullPath(imagePath);
            }

            var imageUri = new Uri(imagePath, UriKind.Absolute);

            BitmapImage imageSource = null;
            try
            {
                imageSource = new BitmapImage(imageUri);
            }
            catch (Exception)
            {
                // Doesn't really matter why the image couldn't be loaded
            }

            return imageSource ?? PlaceholderImage;
        }
    }
}
