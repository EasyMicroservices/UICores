using EasyMicroservices.ServiceContracts.Exceptions;
using EasyMicroservices.UI.Core.Interfaces;
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
    /// <summary>
    /// 
    /// </summary>
    protected readonly Action<object> _execute = null;
    /// <summary>
    /// 
    /// </summary>
    protected readonly Func<object, bool> _canExecute = null;
    /// <summary>
    /// /
    /// </summary>
    protected readonly IBusyViewModel _busyViewModel = null;

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
    /// <param name="busyViewModel"></param>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    public BaseCommand(IBusyViewModel busyViewModel, Action<object> execute, Func<object, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
        _busyViewModel = busyViewModel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public virtual bool CanExecute(object parameter = null)
    {
        try
        {
            return _canExecute == null || _canExecute(parameter);
        }
        catch (Exception ex)
        {
            _busyViewModel?.OnError(ex);
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    public virtual void Execute(object parameter)
    {
        try
        {
            if (_busyViewModel != null)
            {
                _busyViewModel.Busy();
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
            _execute(parameter);
        }
        catch (InvalidResultOfMessageContractException ex)
        {
            _busyViewModel?.DisplayFetchError(ex.MessageContract.Error);
        }
        catch (Exception ex)
        {
            _busyViewModel?.OnError(ex);
        }
        finally
        {
            if (_busyViewModel != null)
            {
                _busyViewModel.UnBusy();
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
