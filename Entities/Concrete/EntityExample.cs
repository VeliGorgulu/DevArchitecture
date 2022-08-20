
using Core.Entities;
namespace Entities.Concrete;

/// <summary>
/// Includes usage examples for  BaseEntity, IEntity, ITenancy
/// </summary>
public class EntityExample : DomainBaseEntity, IEntity, ITenancy
{

    public int TenantId { get; set; }
    public string Name { get; set; }

}
