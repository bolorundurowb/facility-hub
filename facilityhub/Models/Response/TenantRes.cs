﻿namespace FacilityHub.Models.Response;

public class TenantRes
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateOnly? StartsAt { get; set; }

    public DateOnly? EndsAt { get; set; }
}
