using System;

namespace EasyMicroservices.UI.Core.Commands;

/// <summary>
/// 
/// </summary>
public class RelayCommand : BaseCommand
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    public RelayCommand(Action execute, Func<bool> canExecute)
        : base((x) => execute(), (x) => canExecute())
    {

    }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class RelayCommand<T> : BaseCommand
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        : base((x) => execute((T)x), (x) => canExecute((T)x))
    {

    }
}