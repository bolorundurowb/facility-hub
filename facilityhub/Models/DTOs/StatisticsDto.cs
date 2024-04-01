namespace FacilityHub.Models.DTOs;

public record StatisticsDto(
    int FacilitiesRented,
    int FacilitiesOwned,
    int FacilitiesManaged,
    int IssuesFiled,
    int IssuesManaged,
    int IssuesResolved
);
