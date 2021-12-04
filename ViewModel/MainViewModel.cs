using Newtonsoft.Json;
using Project_Groceries.Commands;
using Project_Groceries.Model;
using Project_Groceries.View;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Project_Groceries.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private const string _groceriesFile = "Groceries.json";

        private string _searchFilter;

        private ObservableCollection<Grocery> _groceryList = new();
        private ObservableCollection<Grocery> _groceriesCollection = new();

        internal MainViewModel()
        {
            AddGroceryCommand = new DelegateCommand(o =>
            {
                var grocery = o as Grocery;
                if (!GroceryList.Contains(grocery))
                {
                    GroceryList.Add(grocery);
                    GroceryList = new ObservableCollection<Grocery>(GroceryList.OrderBy(x => _groceriesCollection.IndexOf(x)));
                    OnPropertyChanged(nameof(GroceryList));
                }

                grocery.Quantity++;
                OnPropertyChanged(nameof(SumTotalPrice));
            });

            RemoveGroceryCommand = new DelegateCommand(o =>
            {
                var grocery = o as Grocery;
                GroceryList.Remove(grocery);
                grocery.Quantity = 0;
                OnPropertyChanged(nameof(SumTotalPrice));
                OnPropertyChanged(nameof(GroceryList));
            });

            CellUpdatedCommand = new DelegateCommand(o => OnPropertyChanged(nameof(SumTotalPrice)));

            EditGroceriesCommand = new DelegateCommand(o => EditGroceries());

            LoadGroceries();
        }

        public ObservableCollection<Grocery> GroceryList
        {
            get => _groceryList;
            private set
            {
                if (_groceryList == value) { return; }

                _groceryList = value;
                OnPropertyChanged(nameof(GroceryList));
            }
        }

        public ObservableCollection<Grocery> GroceriesCollection
        {
            get => string.IsNullOrEmpty(_searchFilter)
                ? _groceriesCollection
                : new ObservableCollection<Grocery>(_groceriesCollection.Where(x => x.Name.Contains(Filter)));
            set
            {
                if (_groceriesCollection == value) { return; }

                _groceriesCollection = value;
                OnPropertyChanged(nameof(GroceriesCollection));
            }
        }

        public ICommand AddGroceryCommand { get; }

        public ICommand RemoveGroceryCommand { get; }

        public ICommand CellUpdatedCommand { get; }

        public ICommand EditGroceriesCommand { get; }

        public decimal SumTotalPrice => GroceryList?.Sum(x => x.TotalPrice) ?? decimal.Zero;

        public string Filter
        {
            get => _searchFilter;
            set
            {
                if (Filter == value) { return; }

                _searchFilter = value;
                OnPropertyChanged(nameof(GroceriesCollection));
            }
        }

        private void EditGroceries()
        {
            var view = new GroceriesListView();
            var viewModel = new GroceriesListViewModel(_groceriesCollection);
            view.DataContext = viewModel;

            if (view.ShowDialog() == true && !GroceriesCollection.SequenceEqual(viewModel.Groceries))
            {
                GroceriesCollection = viewModel.Groceries;

                var filteredGroceryList = GroceryList.Where(x => _groceriesCollection.Contains(x));
                var sortedGroceryList = filteredGroceryList.OrderBy(x => _groceriesCollection.IndexOf(x));
                GroceryList = new ObservableCollection<Grocery>(sortedGroceryList);

                SaveGroceries();
            }
        }

        private void LoadGroceries()
        {
            if (!File.Exists(_groceriesFile))
            {
                GroceriesCollection = new ObservableCollection<Grocery>();
                SaveGroceries();
                return;
            }

            var jsonString = File.ReadAllText(_groceriesFile, Encoding.UTF8);
            var collection = JsonConvert.DeserializeObject<Grocery[]>(jsonString) ?? Array.Empty<Grocery>();
            GroceriesCollection = new ObservableCollection<Grocery>(collection);
        }

        private void SaveGroceries()
        {
            var jsonString = JsonConvert.SerializeObject(_groceriesCollection, Formatting.Indented);
            File.WriteAllText(_groceriesFile, jsonString, Encoding.UTF8);
        }
    }
}
