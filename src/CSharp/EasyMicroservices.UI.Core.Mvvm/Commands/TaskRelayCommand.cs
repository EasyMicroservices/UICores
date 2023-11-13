using EasyMicroservices.UI.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Core.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskRelayCommand : TaskBaseCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public TaskRelayCommand(Func<Task> execute, Func<bool> canExecute)
            : this(default, execute, canExecute)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute"></param>
        public TaskRelayCommand(Func<Task> execute)
            : this(default, execute, () => true)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busyViewModel"></param>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public TaskRelayCommand(IBusyViewModel busyViewModel, Func<Task> execute, Func<bool> canExecute)
            : base(busyViewModel, (x) => execute(), (x) => canExecute())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busyViewModel"></param>
        /// <param name="execute"></param>
        public TaskRelayCommand(IBusyViewModel busyViewModel, Func<Task> execute)
            : this(busyViewModel, execute, () => true)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TaskRelayCommand<T> : TaskBaseCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public TaskRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute)
            : this(default, execute, canExecute)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute"></param>
        public TaskRelayCommand(Func<T, Task> execute)
            : this(default, execute, (x) => true)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busyViewModel"></param>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public TaskRelayCommand(IBusyViewModel busyViewModel, Func<T, Task> execute, Func<T, bool> canExecute)
            : base(busyViewModel, (x) => execute((T)x), (x) => canExecute((T)x))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busyViewModel"></param>
        /// <param name="execute"></param>
        public TaskRelayCommand(IBusyViewModel busyViewModel, Func<T, Task> execute)
            : this(busyViewModel, execute, (x) => true)
        {

        }
    }
}
