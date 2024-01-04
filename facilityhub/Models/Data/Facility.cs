using System.ComponentModel.DataAnnotations;
using FacilityHub.Models.DTOs;
using NetTopologySuite.Geometries;

namespace FacilityHub.Models.Data;

public class Facility : Entity
{
    [StringLength(256)]
    public string Name { get; set; }

    [StringLength(1024)]
    public string Address { get; set; }

    public Point? Location { get; set; }

    public List<User> Managers { get; set; }

    public List<User> Owners { get; set; } = new();

    public Tenant? Tenant { get; private set; }

    public List<Document> Documents { get; set; } = new();

    public List<Issue> Issues { get; set; } = new();

    public DateTimeOffset CreatedAt { get; set; }

#pragma warning disable CS8618
    private Facility() { }
#pragma warning restore CS8618

    public Facility(string name, User creator, string address, LocationDto? location)
    {
        Name = name;
        Address = address;
        CreatedAt = DateTimeOffset.Now;
        Managers = new List<User> { creator };

        if (location is not null)
            Location = new Point(location.Longitude, location.Latitude);
    }
}
