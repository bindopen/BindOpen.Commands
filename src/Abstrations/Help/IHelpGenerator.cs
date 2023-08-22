using BindOpen.System.Data.Helpers;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHelpGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(IOptionSet optionSet, string uiCulture = StringHelper.__Star);
    }
}