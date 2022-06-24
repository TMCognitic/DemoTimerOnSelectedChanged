using DemoTimerOnSelectedChanged.Tools.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoTimerOnSelectedChanged.ViewModels
{
    public class MainViewModels : CollectionViewModelBase<DetailViewModel>
    {
        private readonly ICommand _clearSelectionCommand;

        public ICommand ClearSelectionCommand
        {
            get
            {
                return _clearSelectionCommand;
            }
        }

        public MainViewModels()
        {
            _clearSelectionCommand = new Command(() => SelectedItem = null);
        }
        protected override ObservableCollection<DetailViewModel> LoadItems()
        {
            Console.WriteLine("LoadItems Call of MainViewModel");
            return new ObservableCollection<DetailViewModel>()
            {
                new DetailViewModel("Detail 1"),
                new DetailViewModel("Detail 2"),
                new DetailViewModel("Detail 3"),
                new DetailViewModel("Detail 4"),
                new DetailViewModel("Detail 5")
            };
        }
    }
}
