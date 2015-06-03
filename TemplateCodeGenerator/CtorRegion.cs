namespace Igloo.Tools
{
    public class CtorRegion : ClassCodeRegion
    {
        public CtorRegion(ClassGenerator generator)
            : base(ClassRegionType.Ctor, generator)
        {
        }
    }
}
