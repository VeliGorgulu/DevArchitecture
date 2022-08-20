using Core.Entities;
namespace Entities.Concrete;

/// <summary>
/// Includes usage examples for  BaseEntity, ITenancy
/// </summary>
public class DocumentDbEntityExample : DocumentDbEntity, ITenancy
{
    public int TenantId { get; set; }
    public string Name { get; set; }
}
