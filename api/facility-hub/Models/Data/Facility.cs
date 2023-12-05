using FacilityHub.Models.DTOs;
using NetTopologySuite.Geometries;

namespace FacilityHub.Models.Data;

public class Facility : Entity
{
    public string Name { get; set; }

    public string? Address { get; set; }

    public Point? Location { get; set; }

    public List<User> Managers { get; set; }

    public User? Owner { get; set; }

    public List<Issue> Issues { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

#pragma warning disable CS8618
    private Facility() { }
#pragma warning restore CS8618

    public Facility(string name, User creator, string? address, LocationDto? location)
    {
        Name = name;
        Address = address;
        CreatedAt = DateTimeOffset.Now;
        Issues = new List<Issue>();
        Managers = new List<User> { creator };

        if (location is not null)
            Location = new Point(location.Longitude, location.Latitude);
    }
}
