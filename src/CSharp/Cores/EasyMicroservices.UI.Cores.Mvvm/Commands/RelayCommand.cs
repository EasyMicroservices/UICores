using System;

namespace EasyMicroservices.UI.Cores.Commands;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="execute"></param>
    public RelayCommand(Action execute)
        : this(execute, () => true)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="execute"></param>
    public RelayCommand(Action<T> execute)
        : this((x) => execute(x), (x) => true)
    {

    }
}