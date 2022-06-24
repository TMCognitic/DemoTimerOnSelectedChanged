using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoTimerOnSelectedChanged.Tools
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            ArgumentNullException.ThrowIfNull(propertyName);            

            if(propertyName is not "")
            {
                Type type = typeof(T);
                PropertyInfo? propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo is null)
                    throw new ArgumentException("la valeur de 'propertyName' doit être le nom d'une propriété.");
            }

            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;   
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler? propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
