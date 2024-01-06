using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FacilityHub.DataContext.Converters;

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d.ToUniversalTime())
    { }
}
