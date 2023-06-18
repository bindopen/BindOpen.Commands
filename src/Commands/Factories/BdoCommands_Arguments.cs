using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// This class represents a option parser.
    /// </summary>
    public static partial class BdoCommands
    {
        public static string[] GetArguments(
            this string commandLine)
        {
            var args = new List<string>();

            int i = 0;
            while (i > -1)
            {
                var j = commandLine.IndexOfNextString(" ", i);
                if (j > -1)
                {
                    var arg = commandLine[i..j];
                    args.Add(arg);
                }
                i = j + 1;
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
            this string[] arguments,
            IOptionSet optionSet = null,
            bool allowMissingItems = false,
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
                        IParameter param = null;

                        Option option = null;

                        int aliasIndex = -2;
                        if (optionSet != null)
                        {
                            option = optionSet
                               .FirstOrDefault(p => p.IsArgumentMarching(argSt, out aliasIndex))
                               as Option;
                        }

                        if (optionSet == null || option == null && allowMissingItems)
                        {
                            param = BdoMeta.NewScalar<object, Parameter>(argSt, DataValueTypes.Text);
                            param.WithData(arguments.GetAt(index));
                            paramSet.Add(param);
                        }
                        else if (option != null && optionSet != null)
                        {
                            if (option.DataValueType == DataValueTypes.Any)
                            {
                                option.WithDataType(DataValueTypes.Text);
                            }
                            param = BdoMeta.NewScalar<object, Parameter>(
                                option.Name, option.DataValueType, option);

                            param.WithSpecs(option);
                            if (option.DataRequirement.IsPossible())
                            {
                                if (!string.IsNullOrEmpty(option.Label))
                                {
                                    option.Label.ExtractTokens(option.Name).Map(
                                        (LabelFormatsExtensions.__Script_This_Name, q => { param.WithName(q.GetData<string>()); }
                                    ),
                                        (LabelFormatsExtensions.__Script_This_Value, q => { param.WithData(q.GetData()); }
                                    )
                                    );
                                }
                                else
                                {
                                    index++;
                                    if (index < arguments.Length)
                                    {
                                        param.WithData(arguments.GetAt(index));
                                    }
                                }
                            }

                            paramSet.Add(param);
                        }
                    }
                    index++;
                }
            }

            paramSet.Check(optionSet, log: log);

            return paramSet;
        }

        /// <summary>
        /// Indicates whether the specified argument matches with this instance.
        /// </summary>
        /// <param name="spec">The specification to consider.</param>
        /// <param name="arg">The argument to consider.</param>
        /// <param name="aliasIndex">The alias index to consider. -2 not found. -1 Name matches. otherwise the index of matched alias.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public static bool IsArgumentMarching(
            this IOption spec,
            string arg,
            out int aliasIndex)
        {
            aliasIndex = -2;
            if (spec != null && arg != null)
            {
                if (IsNameMatching(spec.Name, arg))
                {
                    aliasIndex = -1;
                }
                else if (spec.Aliases != null)
                {
                    for (int i = 0; i < spec.Aliases.Count; i++)
                    {
                        if (IsNameMatching(spec.Aliases[i], arg))
                        {
                            aliasIndex = i;
                            break;
                        }
                    }
                }
            }

            return aliasIndex > -2;
        }

        /// <summary>
        /// Indicates whether the specified argument matches with this instance.
        /// </summary>
        /// <param name="spec">The specification to consider.</param>
        /// <param name="arg">The argument to consider.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public static bool IsArgumentMarching(this IOption spec, string arg)
        {
            return spec.IsArgumentMarching(arg, out _);
        }

        private static bool IsNameMatching(string name1, string name2)
        {
            if (name1 == null || name2 == null)
                return false;

            int i = name1.IndexOf(" ");
            if (i > -1)
            {
                name1 = name1.ToSubstring(0, i - 1);
                name2 = name2.ToSubstring(0, i - 1);
            }
            return name1.BdoKeyEquals(name2);
        }
    }
}
