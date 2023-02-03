using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// VersionNuGet class (abstraction/implementation
    ///
    /// </summary>
    ///
    /// <seealso href="https://semver.org/"/>
    /// <seealso href="https://semver.org/spec/v1.0.0.html"/>
    /// <seealso href="https://semver.org/spec/v2.0.0.html"/>
    /// <summary>
    ///
    /// https://regex101.com/r/Ly7O1x/3/
    /// ^(?P<major>0|[1-9]\d*)\.(?P<minor>0|[1-9]\d*)\.(?P<patch>0|[1-9]\d*)(?:-(?P<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?P<build_metadatametadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
	/// .NET does not support '?P'
    /// ^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<build_metadatametadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
    /// https://regex101.com/r/vkijKf/1/
    /// ^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$
    public partial class VersionNuGet
    {
    }
}
