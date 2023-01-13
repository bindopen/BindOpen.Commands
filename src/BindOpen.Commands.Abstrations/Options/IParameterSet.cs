using BindOpen.MetaData.Items;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParameterSet : ITBdoItemSet<IParameter>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IParameterSet AddOptions(params IParameter[] options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IParameter GetOption(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetOptionValue(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasOption(string name);
    }
}