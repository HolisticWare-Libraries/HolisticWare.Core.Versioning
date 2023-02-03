using System;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// VersionSemantic class (abstraction/implementation
    ///
    /// </summary>
    public partial class VersionSemantic
    {
        public static 
            bool 
                                        operator==
                                            (
                                                VersionSemantic lhs, 
                                                VersionSemantic rhs
                                            )
        {
            if
                (
                    lhs.Major == rhs.Major
                    &&
                    lhs.Minor == rhs.Minor
                    &&
                    lhs.Patch == rhs.Patch
                    &&
                    string.Compare(lhs.PreRelease, rhs.PreRelease) == 0
                )
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(VersionSemantic lhs, VersionSemantic rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///
        /// Precedence for two pre-release versions with the same major, minor, and
        /// patch version MUST be determined by comparing each dot separated identifier
        /// from left to right until a difference is found as follows:
        ///
        /// 1. Identifiers consisting of only digits are compared numerically.
        ///
        /// 2. Identifiers with letters or hyphens are compared lexically in ASCII
        ///    sort order.
        ///
        /// 3. Numeric identifiers always have lower precedence than non-numeric
        ///    identifiers.
        ///
        /// 4. A larger set of pre-release fields has a higher precedence than a
        ///    smaller set, if all of the preceding identifiers are equal.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator<(VersionSemantic lhs, VersionSemantic rhs)
        {
            if (lhs.Major < rhs.Major)
            {
                return true;
            }

            if (lhs.Major == rhs.Major && lhs.Minor < rhs.Minor)
            {
                return true;
            }

            if (lhs.Major == rhs.Major && lhs.Minor == rhs.Minor && lhs.Patch < rhs.Patch)
            {
                return true;
            }

            if
                (
                    lhs.Major == rhs.Major && lhs.Minor == rhs.Minor && lhs.Patch == rhs.Patch
                )
            {
                if (lhs.PreRelease != null && rhs.PreRelease == null)
                {
                    return true;
                }

                if (string.Compare(lhs.PreRelease, rhs.PreRelease) == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public static bool operator >(VersionSemantic lhs, VersionSemantic rhs)
        {
            return lhs != rhs && ! (lhs < rhs);
        }
    }
}
