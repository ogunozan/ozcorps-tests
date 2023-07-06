using NetTopologySuite.Geometries;
using Ozcorps.Generic.Entity;

namespace Ozcorps.Generic.ApiTest.Entity;

public sealed class Poi : EntityBase, IModifiedEntity
{
    public string Name { get; set; }

    public Geometry Geoloc { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? InsertedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long? InsertedUserId { get; set; }

    public long? ModifiedUserId { get; set; }
}
