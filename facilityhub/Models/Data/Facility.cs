using System.ComponentModel.DataAnnotations;
using FacilityHub.Models.DTOs;
using NetTopologySuite.Geometries;

namespace FacilityHub.Models.Data;

public class Facility : Entity
{
    [StringLength(256)]
    public string Name { get; private set; }

    [StringLength(1024)]
    public string Address { get; private set; }

    public Point? Location { get; private set; }

    public List<User> Managers { get; private set; }

    public List<User> Owners { get; private set; } = new();

    public Tenant? Tenant { get; private set; }

    public List<Document> Documents { get; private set; } = new();

    public List<Issue> Issues { get; private set; } = new();

    public DateTimeOffset CreatedAt { get; private set; }

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

    public void AddManager(User user)
    {
        // if the user is an owner, they don't need to be a manager
        var isOwner = Owners.Any(x => x.Id == user.Id);
        var isAlreadyManager = Managers.Any(x => x.Id == user.Id);

        if (!isOwner && !isAlreadyManager)
            Managers.Add(user);
    }

    public void AddOwner(User user)
    {
        // if the user is a manager, then promote
        var isAlreadyOwner = Owners.Any(x => x.Id == user.Id);

        Managers.RemoveAll(x => x.Id == user.Id);

        if (!isAlreadyOwner)
            Owners.Add(user);
    }

    public void SetTenant(User inviter, User? user, DateOnly startsAt, DateOnly endsAt, DateOnly paidAt) =>
        Tenant = new Tenant(inviter, startsAt, endsAt, paidAt, user);
}
