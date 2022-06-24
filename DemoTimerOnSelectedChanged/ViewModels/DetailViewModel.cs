using DemoTimerOnSelectedChanged.Tools.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DemoTimerOnSelectedChanged.ViewModels
{
    public class DetailViewModel : TimedCollectionViewModelBase<ContentViewModel>
    {
        public string Text { get; init; }

        public DetailViewModel(string text)
        {            
            Text = text;    
        }

        protected override ObservableCollection<ContentViewModel> LoadItems()
        {
            Console.WriteLine($"LoadItems Call for {GetHashCode()}");
            return new ObservableCollection<ContentViewModel>()
            {
                new ContentViewModel() { Value = $"{Text} value 1" },
                new ContentViewModel() { Value = $"{Text} value 2" },
                new ContentViewModel() { Value = $"{Text} value 3" },
                new ContentViewModel() { Value = $"{Text} value 4" },
                new ContentViewModel() { Value = $"{Text} value 5" },
            };
        }
    }
}