using Project_Groceries.Commands;
using Project_Groceries.Model;
using Project_Groceries.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Project_Groceries.ViewModel
{
    class GroceriesListViewModel : ViewModelBase
    {
        private string _filter = string.Empty;

        internal GroceriesListViewModel(IEnumerable<Grocery> groceries)
        {
            Groceries = new ObservableCollection<Grocery>(groceries);

            GroceriesView = CollectionViewSource.GetDefaultView(Groceries);
            GroceriesView.Filter = o => (o as Grocery).Name.Contains(Filter);

            CreateGroceryCommand = new DelegateCommand(o => CreateGrocery());
            EditGroceryCommand = new DelegateCommand(o => EditGrocery(o as Grocery));
            DeleteGroceryCommand = new DelegateCommand(o => DeleteGrocery(o as Grocery));
            MoveUpCommand = new DelegateCommand(o => MoveGrocery(o as Grocery, -1));
            MoveDownCommand = new DelegateCommand(o => MoveGrocery(o as Grocery, 1));
        }

        public ObservableCollection<Grocery> Groceries { get; }

        public ICollectionView GroceriesView { get; }

        public ICommand CreateGroceryCommand { get; }

        public ICommand EditGroceryCommand { get; }

        public ICommand DeleteGroceryCommand { get; }

        public ICommand MoveUpCommand { get; }

        public ICommand MoveDownCommand { get; }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter == value) { return; }

                _filter = value;
                GroceriesView.Refresh();
            }
        }

        private void CreateGrocery()
        {
            var view = new ConfigureGroceryView();
            var viewModel = new ConfigureGroceryViewModel();
            view.DataContext = viewModel;
            
            if (view.ShowDialog() == true)
            {
                var newGrocery = new Grocery(viewModel.Name, viewModel.Price, viewModel.ImagePath);
                Groceries.Add(newGrocery);
            }
        }

        private void EditGrocery(Grocery grocery)
        {
            var view = new ConfigureGroceryView();
            var viewModel = new ConfigureGroceryViewModel(grocery);
            view.DataContext = viewModel;
            
            if (view.ShowDialog() == true)
            {
                var editedGrocery = new Grocery(viewModel.Name, viewModel.Price, viewModel.ImagePath);
                Groceries[Groceries.IndexOf(grocery)] = editedGrocery;
            }
        }

        private void DeleteGrocery(Grocery grocery) => Groceries.Remove(grocery);

        private void MoveGrocery(Grocery grocery, int steps)
        {
            var currentIndex = Groceries.IndexOf(grocery);

            var newIndex = currentIndex + steps;
            newIndex = Math.Min(newIndex, Groceries.Count - 1);
            newIndex = Math.Max(newIndex, 0);

            if (currentIndex == newIndex) { return; }

            Groceries.Move(currentIndex, newIndex);
        }
    }
}
