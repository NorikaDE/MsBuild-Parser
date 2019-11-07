namespace Norika.MsBuild.Model.Interfaces
{
    public interface IMsBuildProperty : IMsBuildElement
    {
        string Name { get; }
        
        string Value { get; }
        
        bool HasPublicSetter { get; }
    }
}