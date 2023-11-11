using System;
using System.Windows.Input;

namespace EasyMicroservices.UI.Core.Commands;

/// <summary>
/// 
/// </summary>
public abstract class BaseCommand : ICommand
{
    /// <summary>
    /// 
    /// </summary>
    public event EventHandler CanExecuteChanged;
    private readonly Action<object> _execute = null;
    private readonly Func<object, bool> _canExecute = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    public BaseCommand(Action<object> execute, Func<object, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public virtual bool CanExecute(object parameter = null)
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    public virtual void Execute(object parameter)
    {
        _execute(parameter);
    }
}
