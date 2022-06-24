using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTimerOnSelectedChanged.Tools.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public ViewModelBase()
        {
            //Loading and Assign all RaiseCanExecuteChanged of commands to PropertyChanged event 
        }
    }

    public abstract class ViewModelBase<TEntity> : ViewModelBase
    {
        protected TEntity Entity { get; init; }

        public ViewModelBase(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            Entity = entity;
        }
    }
}
