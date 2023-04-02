using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Filer2.View.Base;
internal abstract class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// базовый класс, нужел для сообщение модели  о том что её свойства изменились
    /// а так же предотвращает бесконечный цыкл если свойство уже равно значению
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanget([CallerMemberName] string PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }

    protected virtual bool Set<T>(ref T filed, T value, [CallerMemberName] string PropertyName = null)
    {
        if(Equals(filed, value))
            return false;
        filed = value;
        OnPropertyChanget(PropertyName);
        return true;
    }
}
