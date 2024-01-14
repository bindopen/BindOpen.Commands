using BindOpen.Scoping;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// This class represents a option factory.
    /// </summary>
    public static partial class BdoCommands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string GetHelpText<T>(
            this IBdoScope scope,
            IOption option)
            where T : IHelpGenerator, new()
        {
            var generator = new T().WithScope(scope);
            return generator.GetHelpText(option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string GetHelpText(
            this IBdoScope scope,
            IOption option)
            => scope.GetHelpText<StandardHelpGenerator>(option);
    }
}
