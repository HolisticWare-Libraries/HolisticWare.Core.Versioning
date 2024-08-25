namespace Core;

/// <summary>
///
/// </summary>
/// <seealso href="https://semver.org/"/>
/// <seealso href="https://semver.org/spec/v1.0.0.html"/>
/// <seealso href="https://semver.org/spec/v2.0.0.html"/>
/*
    https://semver.org/

    https://regex101.com/r/vkijKf/1/
    "^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$"
    https://regex101.com/r/Ly7O1x/3/
    C#
    "^(?P<major>0|[1-9]\d*)\.(?P<minor>0|[1-9]\d*)\.(?P<patch>0|[1-9]\d*)(?:-(?P<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?P<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$"

    Modified RegExUsed to parse Maven Versions with Release/Revision part

    "^(?P<major>0|[1-9]\d*)\.(?P<minor>0|[1-9]\d*)\.(?P<patch>0|[1-9]\d*)\.(?P<release-revision>0|[1-9]\d*)(?:-(?P<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?P<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$"

    Maven

    0.0.4.1

    0.10.1.e92f734

        https://mvnrepository.com/artifact/org.aomedia.avif.android/avif

    2.8
    2.71828

        https://mvnrepository.com/artifact/com.squareup.picasso/picasso
*/
public partial class
                                        VersionMaven
                                        :
                                        VersionBase
{

    public static
        VersionMaven
                                        Parse
                                        (
                                            string version
                                        )
    {
        VersionMaven vm = new ();

        vm.TextualVersionOriginal = version;

        return vm;
    }
}
