using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Commands;

/// <summary>
/// This class represents a option parser.
/// </summary>
public static partial class ArgumentHelper
{
    public static string[] GetArguments(
        this string commandLine)
    {
        if (string.IsNullOrEmpty(commandLine)) return Array.Empty<string>();

        var args = new List<string>();

        int i = 0;
        while (i > -1)
        {
            var j = commandLine.IndexOfNextString(" ", i);
            if (j > -1)
            {
                // avoid subsequent spaces
                if (commandLine[i] != ' ')
                {
                    var arg = commandLine[i..j];
                    args.Add(arg);
                }

                i = j + 1;
            }
            else
            {
                var arg = commandLine[i..];
                args.Add(arg);
                i = -1;
            }
        }

        return args?.ToArray();
    }

    /// <summary>
    /// Retrieves the arguments from the specified argument string values.
    /// </summary>
    /// <param name="arguments">The argument string values to consider.</param>
    /// <param name="optionSet">The option specification set to consider.</param>
    /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the log of argument building.</returns>
    public static IParameterSet ParseArguments(
        this IBdoScope scope,
        string[] arguments,
        IOption optionRoot = null,
        bool checkArguments = true,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        IParameterSet paramSet = new ParameterSet();

        int index = 0;
        if (arguments != null)
        {
            while (index < arguments.Length)
            {
                string argSt = arguments[index];

                if (argSt != null)
                {
                    Parameter param = null;

                    Option option = null;

                    int aliasIndex = -2;
                    if (optionRoot != null)
                    {
                        option = optionRoot?._Children.FirstOrDefault(p => argSt.IsArgumentMarching(p as IOption, out aliasIndex)) as Option;
                    }

                    if (optionRoot == null || option == null)
                    {
                        param = BdoData.NewScalar<object, Parameter>(argSt, DataValueTypes.Text);
                        param.WithData(arguments.GetAt(index));
                        paramSet.Add(param);
                    }
                    else if (option != null && optionRoot != null)
                    {
                        if ((option.DataType?.ValueType ?? DataValueTypes.Any) == DataValueTypes.Any)
                        {
                            option.WithDataType(DataValueTypes.Text);
                        }
                        param = BdoData.NewScalar<object, Parameter>(option.Name, option.DataType.ValueType)
                            .WithSchema(option);

                        var localVarSet = BdoData.NewSet(varSet?.ToArray());
                        localVarSet.Add(BdoData.__VarName_This, param);

                        var requirementLevel = param.GetRequirementLevel(scope, localVarSet, log);

                        if (requirementLevel == RequirementLevels.None || requirementLevel.IsPossible())
                        {
                            if (!string.IsNullOrEmpty(option.Label))
                            {
                                IBdoMetaSet tokenSet = null;

                                while (tokenSet == null && index < arguments.Length)
                                {
                                    tokenSet = argSt.ExtractTokenMetas(option.Label);

                                    if (tokenSet == null)
                                    {
                                        index++;
                                        argSt += " " + arguments.GetAt(index);
                                    }
                                }

                                if (tokenSet?.Count > 0)
                                {
                                    tokenSet.Map(
                                        (BdoMetaDataProperties.Value, q =>
                                        {
                                            var obj = q.GetData<string>().ToObject(option.DataType?.ValueType ?? DataValueTypes.Any);
                                            param.WithData(obj);
                                        }
                                    )
                                    );
                                }

                                if (option.Label?.Contains(BdoMetaDataProperties.Value) != true)
                                {
                                    if (param.DataType.ValueType == DataValueTypes.Boolean
                                        || param.DataType.ValueType == DataValueTypes.Any)
                                    {
                                        param.WithData(true);
                                    }
                                    else if (param.DataType.ValueType == DataValueTypes.Text)
                                    {
                                        param.WithData("true");
                                    }
                                }
                            }

                            paramSet.Add(param);
                        }
                    }
                }

                index++;
            }
        }

        if (checkArguments)
        {
            var valid = paramSet.Check(optionRoot, scope, varSet, log);

            if (!valid)
            {
                paramSet = null;
            }
        }

        return paramSet;
    }

    /// <summary>
    /// Indicates whether the specified argument matches with this instance.
    /// </summary>
    /// <param name="spec">The specification to consider.</param>
    /// <param name="arg">The argument to consider.</param>
    /// <returns>Returns True if the specified matches this instance.</returns>
    public static bool IsArgumentMarching(this IOption spec, string arg)
    {
        return arg.IsArgumentMarching(spec, out _);
    }

    /// <summary>
    /// Indicates whether the specified argument matches with this instance.
    /// </summary>
    /// <param name="option">The specification to consider.</param>
    /// <param name="arg">The argument to consider.</param>
    /// <param name="aliasIndex">The alias index to consider. -2 not found. -1 Name matches. otherwise the index of matched alias.</param>
    /// <returns>Returns True if the specified matches this instance.</returns>
    public static bool IsArgumentMarching(
        this string arg,
        IOption option,
        out int aliasIndex)
    {
        aliasIndex = -2;

        if (option != null && arg != null)
        {
            if (IsArgumentMarching(arg, option.Name, option.Label))
            {
                aliasIndex = -1;
                return true;
            }
            else if (option.Aliases != null)
            {
                for (int i = 0; i < option.Aliases.Count; i++)
                {
                    if (IsArgumentMarching(arg, option.Aliases[i], option.Label))
                    {
                        aliasIndex = i;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private static bool IsArgumentMarching(string arg, string option, string pattern)
    {
        if (arg?.BdoKeyEquals(option) == true) return true;

        if (!string.IsNullOrEmpty(pattern))
        {
            arg?.ExtractTokenMetas(pattern)?.Map(
                (BdoMetaDataProperties.Name, q => { arg = q.GetData<string>(); }
            )
            );

            return arg?.BdoKeyEquals(option) == true;
        }

        return false;
    }

}
