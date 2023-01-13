using BindOpen.MetaData;
using BindOpen.MetaData.Items;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSet :
        ITBdoItemSet<IOption>, ITNamedPoco<IOptionSet>,
        ITGloballyDescribedPoco<IOptionSet>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(string uiCulture = "*");
    }
}