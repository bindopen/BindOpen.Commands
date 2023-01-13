using BindOpen.MetaData;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option parser.
    /// </summary>
    public static class BdoOptionsParser
    {
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

            int i = name1.IndexOf(StringHelper.__PatternEmptyValue);
            if (i > -1)
            {
                name1 = name1.ToSubstring(0, i - 1);
                name2 = name2.ToSubstring(0, i - 1);
            }
            return name1.BdoKeyEquals(name2);
        }
    }
}
