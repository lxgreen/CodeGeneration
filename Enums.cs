namespace Igloo.Tools
{
    public enum ClassAccessModifier
    {
        Public,
        Internal,
        Private
    }

    public enum ClassRegionType
    {
        PrivateFields,
        Ctor,
        Properties,
        Methods,
        ClassAttributes,
        Usings,
        ClassDeclaration,
        NameSpace
    }

    public enum ClassModifiers
    {
        None = 0,
        Sealed = 1,
        Abstract = 2,
        Static = 4,
    }
}
