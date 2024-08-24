using System;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// VersionSemantic class (abstraction/implementation
    ///
    /// </summary>
    ///
    /// <seealso href="https://semver.org/"/>
    /// <seealso href="https://semver.org/spec/v1.0.0.html"/>
    /// <seealso href="https://semver.org/spec/v2.0.0.html"/>
    /// <summary>
    public partial class
                                        VersionSemantic
    {
        static
                                        VersionSemantic
                                        (
                                        )
        {
            ParsingMethod = ParseDummyNaive;

            return;
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
            this.BuildMetaData = build;

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
            this.BuildMetaData = parsed.build;

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

        public string BuildMetaData
        {
            get;
            set;
        }
    }
}
