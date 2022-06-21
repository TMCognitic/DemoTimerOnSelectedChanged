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
    public class MainViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<DetailViewModel>? _items;
        private DetailViewModel? _selectedItem;
        private readonly ICommand _clearSelectionCommand;


        public ObservableCollection<DetailViewModel> Items { 
            get
            {
                return _items ??= LoadItems();
            } 
        }


        public DetailViewModel? SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if(_selectedItem != value)
                {
                    if (value is not null)
                    {
                        value.CancelClear();
                    }

                    if (_selectedItem is not null)
                    {
                        _selectedItem.Clear();
                    }

                    _selectedItem = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                }
            }
        }

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
        private ObservableCollection<DetailViewModel> LoadItems()
        {
            return new ObservableCollection<DetailViewModel>()
            {
                new DetailViewModel("Detail 1 "),
                new DetailViewModel("Detail 2 "),
                new DetailViewModel("Detail 3 "),
                new DetailViewModel("Detail 4 "),
                new DetailViewModel("Detail 5 ")
            };
        }
    }
}
