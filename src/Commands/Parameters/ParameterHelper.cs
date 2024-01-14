using System.Linq;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// This class represents the application argument parser.
    /// </summary>
    public static partial class ParameterHelper
    {
        /// <summary>
        /// Executes the specified action if the specified value is true.
        /// </summary>
        /// <param key="value">The value to consider.</param>
        /// <param key="action">The action to consider.</param>
        public static T Invoke<T>(this T paramSet)
            where T : IParameterSet
        {
            if (paramSet != null)
            {
                foreach (var param in paramSet)
                {
                    var option = param.Spec as IOption;
                    if (option?.Executions != null)
                    {
                        foreach (var execution in option.Executions)
                        {
                            if (execution.Condition != null
                                && execution.Condition.Invoke(param) != false)
                            {
                                execution.Action.Invoke();
                                return paramSet;
                            }
                        }

                        var defaultExecution = option.Executions.FirstOrDefault(q => q.Condition == null);
                        if (defaultExecution != default)
                        {
                            defaultExecution.Action.Invoke();
                        }
                    }
                }
            }

            return paramSet;
        }
    }
}