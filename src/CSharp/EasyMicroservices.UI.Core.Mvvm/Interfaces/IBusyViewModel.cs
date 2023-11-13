using EasyMicroservices.ServiceContracts;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusyViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        Action<bool> OnBusyChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsBusy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Busy();

        /// <summary>
        /// 
        /// </summary>
        void UnBusy();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task OnError(Exception exception);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorContract"></param>
        /// <returns></returns>
        Task DisplayFetchError(ErrorContract errorContract);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task DisplayError(string message);
    }
}
