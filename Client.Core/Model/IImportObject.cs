
namespace PayrollEngine.Client.Model;

/// <summary>An import object</summary>
public interface IImportObject<in T>
{
    /// <summary>The object exchange</summary>
    void Import(T source);
}