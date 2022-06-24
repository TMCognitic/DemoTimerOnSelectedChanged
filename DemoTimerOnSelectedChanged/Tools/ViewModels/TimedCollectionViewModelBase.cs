using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DemoTimerOnSelectedChanged.Tools.ViewModels
{
    public abstract class TimedCollectionViewModelBase<TViewModel> : CollectionViewModelBase<TViewModel>, ITimedCollectionViewModel
        where TViewModel : ViewModelBase
    {
        private readonly int _delay;
        private bool _isCleaning;
        private Task? _cleanupTask;
        private CancellationTokenSource? _cancellationTokenSource;

        public bool IsSelected
        {
            set
            {
                if (value)
                {
                    CancelCleanupOrReload();
                }
                else
                {
                    Cleanup();
                }
            }
        }

        private void Cleanup()
        {
            if(!_isCleaning)
            {
                _isCleaning = true;
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                cancellationToken.Register(() =>
                {
                    Console.WriteLine($"Cleanup task canceled for {GetHashCode()}...");
                    Reset();
                });

                Console.WriteLine($"Cleanup task begin for {GetHashCode()}...");
                _cleanupTask = Task.Delay(_delay, cancellationToken).ContinueWith((t) =>
                {
                    if(!t.IsCanceled)
                    {
                        lock(Items)
                        {
                            Application.Current.Dispatcher.Invoke(() => Items.Clear());
                            Reset();
                            Console.WriteLine($"View model is clean for {GetHashCode()}...");
                            Console.WriteLine($"Item count {Items.Count}...");
                        }
                    }
                });

                void Reset()
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                    _isCleaning = false;
                }
            }
        }

        private void CancelCleanupOrReload()
        {
            if (_isCleaning)
            {
                _cancellationTokenSource?.Cancel();
            }
            else if(Items.Count == 0)
            {
                foreach (TViewModel item in LoadItems())
                {
                    Items.Add(item);
                }
            }
        }

        protected TimedCollectionViewModelBase(int delay = 3000)
        {
            _delay = delay;
        }
    }
}
