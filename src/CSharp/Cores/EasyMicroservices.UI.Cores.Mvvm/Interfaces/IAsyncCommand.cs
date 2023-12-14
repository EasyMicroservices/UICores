using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyMicroservices.UI.Cores.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task ExecuteAsync(object parameter);
    }
}
