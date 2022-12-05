using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSpecSet :
        ITDataItemSet<IOptionSpec>, ITNamedPoco<IOptionSpecSet>,
        ITDescribedPoco<IOptionSpecSet>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(string uiCulture = "*");
    }
}