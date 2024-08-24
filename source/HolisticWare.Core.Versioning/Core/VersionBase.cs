namespace Core;

/// <summary>
/// /// VersionSemantic class (abstraction/implementation
///
/// </summary>
public partial class
                                        VersionBase

{
    public int Major
    {
        get;
        set;
    }
    = 0;

    public int Minor
    {
        get;
        set;
    }
    = 0;

    public int Patch
    {
        get;
        set;
    }
    = 0;

    // this is possible in Maven
    public int ReleaseRevision
    {
        get;
        set;
    }
    = 0;

    public string PreRelease
    {
        get;
        set;
    }
    = null;

    public string BuildMetadata
    {
        get;
        set;
    }
    = null;

    public string TextualMajor
    {
        get;
        set;
    }
    = null;

    public string TextualMinor
    {
        get;
        set;
    }
    = null;

    public string TextualPatch
    {
        get;
        set;
    }
    = null;

    // this is possible in Maven
    public string TextualReleaseRevision
    {
        get;
        set;
    }
    = null;

    public string TextualVersionOriginal
    {
        get;
        set;
    }
    = null;

}
