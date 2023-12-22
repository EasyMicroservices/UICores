using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores.Interfaces;
/// <summary>
/// 
/// </summary>
public interface IResponsibleViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<object> GetResult();
    /// <summary>
    /// 
    /// </summary>
    void Close();
}
