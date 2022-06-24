using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTimerOnSelectedChanged.Tools.ViewModels
{
    public abstract class CollectionViewModelBase<TViewModel> : ViewModelBase
        where TViewModel : ViewModelBase
    {
        private ObservableCollection<TViewModel>? _items;
        private TViewModel? _selectedItem;

        public ObservableCollection<TViewModel> Items
        {
            get => _items ??= LoadItems();
        }

        public TViewModel? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if(_selectedItem != value)
                {
                    if (_selectedItem is not null && _selectedItem is ITimedCollectionViewModel oldTimedViewModel)
                    {
                        oldTimedViewModel.IsSelected = false;
                    }

                    if (value is not null && value is ITimedCollectionViewModel newTimedViewModel)
                    {
                        newTimedViewModel.IsSelected = true;
                    }
                }

                Set(ref _selectedItem, value);
            }
                    
        }

        protected abstract ObservableCollection<TViewModel> LoadItems();        
    }
}
