using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public static class IOptionExtensions
    {
        public static T Execute<T>(
            this T option,
            IBdoScope scope,
            IBdoExpression expression,
            Action action,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : IOption
        {
            if (option != null && expression != null)
            {
                option.Executions ??= new();
                option.Executions.Add(((IBdoMetaData param) =>
                {
                    var localVarSet = BdoData.NewSet(varSet?.ToArray());
                    localVarSet.Add(BdoData.__VarName_This, param);

                    var invoke = scope.Interpreter.Evaluate<bool?>(expression, localVarSet, log);

                    return invoke ?? false;
                }, action));
            }

            return option;
        }
    }
}