using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DemoTimerOnSelectedChanged.ViewModels
{
    public class DetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _disposing;
        private Task? _cleaningTask;
        private CancellationTokenSource? _cancellationTokenSource;

        private ObservableCollection<string>? _items;


        public ObservableCollection<string>? Items
        {
            get
            {
                return _items ??=LoadItems();
            }
            private set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));

            }
        }

        public string Text { get; init; }

        public DetailViewModel(string text)
        {            
            Text = text;    
        }

        private ObservableCollection<string> LoadItems()
        {
            return new ObservableCollection<string>()
            {
                $"{Text} value 1",
                $"{Text} value 2",
                $"{Text} value 3",
                $"{Text} value 4",
                $"{Text} value 5",
            };
        }

        public void Clear()
        {
            if (!_disposing)
            {
                _disposing = true;
                Console.WriteLine($"Cleaning process starting on {Text}");
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                cancellationToken.Register(() =>
                {
                    Console.WriteLine($"Cancel cleaning Task for {Text}...");
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                    _disposing = false;
                });

                _cleaningTask = Task.Run(() =>
                {
                    int delay = 3000;
                    Console.WriteLine($"Cleaning process started on {Text}, deletion in {delay / 1000} secs...");
                    Task.Delay(delay, cancellationToken).Wait();

                    if (!cancellationToken.IsCancellationRequested)
                    {                        
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Items!.Clear();
                        });
                        Items = null;
                        Console.WriteLine($"Content of {Text} is clear...");
                        _cancellationTokenSource.Dispose();
                        _cancellationTokenSource = null;
                        _disposing = false;
                    }                   
                }, cancellationToken);
            }
        }

        public void CancelClear()
        {
            if (_disposing && _cancellationTokenSource is not null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}