using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SocialWebAPI.Extensions
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter()
            : base(dateOnly =>
                    dateOnly.ToDateTime(TimeOnly.MinValue),
                dateTime => DateOnly.FromDateTime(dateTime))
        { }
    }
}
