using System;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    ///
    /// https://semver.org/
    /// </summary>
    /// https://regex101.com/r/Ly7O1x/3/
    /// ^(?P<major>0|[1-9]\d*)\.(?P<minor>0|[1-9]\d*)\.(?P<patch>0|[1-9]\d*)(?:-(?P<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?P<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
	/// .NET does not support '?P'
    /// ^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
    /// https://regex101.com/r/vkijKf/1/
    /// ^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
    public partial class VersionSemantic
    {
        static VersionSemantic()
        {
            re01 = new Regex(@"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");
            re02 = new Regex(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

            // https://dotnetfiddle.net/tnKTPd
            re03 = new Regex("^(?:\\d+\\.){2}\\d+(?:-(?:0|[1-9]\\d*|\\d*[a-z-][a-z\\d-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-z-][a-z\\d-]*))*)?(?:\\+[a-z\\d-]+(?:\\.[a-z\\d-]+)*)?$", RegexOptions.IgnoreCase);

            ParsingMethod = ParseDummyNaive;

            return;
        }

        public static Func
            <
                string,
                (
                    int major,
                    int minor,
                    int patch,
                    string prerelease,
                    string build
                )
            >                                       ParsingMethod;

        static Regex re01 = null;
        static Regex re02 = null;
        static Regex re03 = null;


        public static
            (
                int major,
                int minor,
                int patch,
                string prerelease,
                string build
            )
                                                    ParseDummyNaive
                                                        (
                                                            string text
                                                        )
        {
            /*
                <valid semver> ::= <version core>
                         | <version core> "-" <pre-release>
                         | <version core> "+" <build>
                         | <version core> "-" <pre-release> "+" <build>
            */

            bool contains_minus = text.Contains("-");
            bool contains_plus  = text.Contains("+");
            int idx_minus   = text.IndexOf('-');
            int idx_plus    = text.IndexOf('+');

            int major = -1;
            int minor = -1;
            int patch = -1;
            string prerelease = null;
            string build = null;

            string core = null;

            if ( idx_minus > -1 )
            {
                core = text.Substring(0, idx_minus);
            }
            else
            {
                core = text;
            }

            string[] parts_core = core.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts_core.Length == 3)
            {
                if
                    (
                        (parts_core[0].Length > 1 && parts_core[0].StartsWith("0")) || parts_core[0].StartsWith("-")
                        ||
                        (parts_core[1].Length > 1 && parts_core[1].StartsWith("0")) || parts_core[1].StartsWith("-")
                        ||
                        (parts_core[2].Length > 1 && parts_core[2].StartsWith("0")) || parts_core[2].StartsWith("-")
                    )
                {
                    throw new InvalidOperationException("Semantic Version not recognized");
                }

                bool parsed_int_1 = int.TryParse(parts_core[0], out major);
                if (parsed_int_1 == false)
                {
                    throw new InvalidOperationException("Semantic Version not recognized");
                }
                bool parsed_int_2 = int.TryParse(parts_core[1], out minor);
                if (parsed_int_2 == false)
                {
                    throw new InvalidOperationException("Semantic Version not recognized");
                }
                bool parsed_int_3 = int.TryParse(parts_core[2], out patch);
                if (parsed_int_3 == false)
                {
                    throw new InvalidOperationException("Semantic Version not recognized");
                }
            }
            else if (parts_core.Length == 2)
            {
                throw new InvalidOperationException("Semantic Version not recognized");
            }
            else if (parts_core.Length > 3)
            {
                throw new InvalidOperationException("Semantic Version not recognized");
            }
            else if (parts_core.Length == 1)
            {
                throw new InvalidOperationException("Semantic Version not recognized");
            }
            else
            {
                throw new InvalidOperationException("Semantic Version not recognized");
            }

            if ( ! contains_minus && ! contains_plus )
            {
                // no op
            }
            else if ( contains_minus && ! contains_plus )
            {
                prerelease = text.Substring(idx_minus + 1, text.Length - idx_minus - 1);

                string[] parts_prerelease = prerelease.Split(new char[] { '.' }, StringSplitOptions.None);
                foreach(string pp in parts_prerelease)
                {
                    if ( pp.StartsWith("0") || pp == null )
                    {
                        throw new InvalidOperationException("Semantic Version not recognized");
                    }
                }
            }
            else if ( ! contains_minus && contains_plus )
            {
                build = text.Substring(idx_plus, text.Length - idx_plus);
            }
            else if ( contains_minus && contains_plus)
            {
                build = text.Substring(idx_plus + 1, text.Length - idx_plus - 1);
                if (build.Contains("+"))
                {
                    throw new InvalidOperationException("Semantic Version not recognized");
                }
                prerelease = text.Substring(idx_minus + 1, text.Length - idx_minus - 1)
                                 .Replace($"+{build}", "");
            }
            else
            {
                throw new InvalidOperationException("Semantic Version not recognized");
            }

            return
                    (
                        major: major,
                        minor: minor,
                        patch: patch,
                        prerelease: prerelease,
                        build: build
                    );
        }

        public static
            (
                int major,
                int minor,
                int patch,
                string prerelease,
                string build
            )
                                                ParseRegexV01
                                                            (
                                                                string text
                                                            )
        {
            int major = -1;
            int minor = -1;
            int patch = -1;
            string prerelease = null;
            string build = null;

            MatchCollection matches = re01.Matches(text);

            foreach (Match m in matches)
            {
                if (m.Groups["major"].Success)
                {
                    major = int.Parse(m.Groups["major"].Value);
                }
                if (m.Groups["minor"].Success)
                {
                    minor = int.Parse(m.Groups["minor"].Value);
                }
                if (m.Groups["patch"].Success)
                {
                    patch = int.Parse(m.Groups["patch"].Value);
                }
                if (m.Groups["prerelease"].Success)
                {
                    prerelease = m.Groups["prerelease"].Value;
                }
                if (m.Groups["buildmetadata"].Success)
                {
                    build = m.Groups["buildmetadata"].Value;
                }
            }

            return
                    (
                        major: major,
                        minor: minor,
                        patch: patch,
                        prerelease: prerelease,
                        build: build
                    );
        }

        public static
            (
                int major,
                int minor,
                int patch,
                string prerelease,
                string build
            )
                                                ParseRegexV02
                                                            (
                                                                string text
                                                            )
        {
            int major = -1;
            int minor = -1;
            int patch = -1;
            string prerelease = null;
            string build = null;

            MatchCollection matches = re02.Matches(text);

            foreach (Match m in matches)
            {
                if (m.Groups["1"].Success)
                {
                    major = int.Parse(m.Groups["1"].Value);
                }
                if (m.Groups["2"].Success)
                {
                    minor = int.Parse(m.Groups["2"].Value);
                }
                if (m.Groups["3"].Success)
                {
                    patch = int.Parse(m.Groups["3"].Value);
                }
                if (m.Groups["4"].Success)
                {
                    prerelease = m.Groups["4"].Value;
                }
                if (m.Groups["5"].Success)
                {
                    build = m.Groups["5"].Value;
                }
            }

            return
                    (
                        major: major,
                        minor: minor,
                        patch: patch,
                        prerelease: prerelease,
                        build: build
                    );
        }

        private static
            (
                int major,
                int minor,
                int patch,
                string prerelease,
                string build
            )
                                                ParseRegexV03
                                                            (
                                                                string text
                                                            )
        {
            int major = -1;
            int minor = -1;
            int patch = -1;
            string prerelease = null;
            string build = null;


            MatchCollection matches = re03.Matches(text);

            foreach (Match m in matches)
            {
                if (m.Groups["0"].Success)
                {
                    major = int.Parse(m.Groups["major"].Value);
                }
                if (m.Groups["minor"].Success)
                {
                    minor = int.Parse(m.Groups["minor"].Value);
                }
                if (m.Groups["patch"].Success)
                {
                    patch = int.Parse(m.Groups["patch"].Value);
                }
                if (m.Groups["prerelease"].Success)
                {
                    prerelease = m.Groups["prerelease"].Value;
                }
                if (m.Groups["buildmetadata"].Success)
                {
                    build = m.Groups["buildmetadata"].Value;
                }
            }

            return
                    (
                        major: major,
                        minor: minor,
                        patch: patch,
                        prerelease: prerelease,
                        build: build
                    );
        }



        public VersionSemantic()
        {
            return;
        }

        public
                                    VersionSemantic
                                            (
                                                int major,
                                                int minor,
                                                int patch,
                                                string prerelease = null,
                                                string build = null
                                            )
        {
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
            this.PreRelease = prerelease;
            this.Build = build;

            return;
        }

        public
                                    VersionSemantic
                                            (
                                                string version
                                            )
        {
            (
                int major,
                int minor,
                int patch,
                string prerelease,
                string build
            ) parsed = ParsingMethod(version);

            this.Major = parsed.major;
            this.Minor = parsed.minor;
            this.Patch = parsed.patch;
            this.PreRelease = parsed.prerelease;
            this.Build = parsed.build;

            return;
        }

        public int Major
        {
            get;
            set;
        }

        public int Minor
        {
            get;
            set;
        }

        public int Patch
        {
            get;
            set;
        }

        public string PreRelease
        {
            get;
            set;
        }

        public string Build
        {
            get;
            set;
        }

        public static bool operator==(VersionSemantic lhs, VersionSemantic rhs)
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
