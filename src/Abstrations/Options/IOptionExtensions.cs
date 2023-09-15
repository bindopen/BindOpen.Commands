using BindOpen.System.Data;
using System;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public static class IOptionExtensions
    {
        public static T Execute<T>(
            this T option,
            Action action)
            where T : IOption
            => option.Execute(null, action);

        public static T Execute<T>(
            this T option,
            IBdoExpression expression,
            Action action)
            where T : IOption
        {
            if (option != null)
            {
                option.Executions ??= new();
                option.Executions.Add((expression, action));
            }

            return option;
        }
    }
}