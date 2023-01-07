namespace SocialWebAPI.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dateOnly)
        {
            var age = DateOnly.FromDateTime(DateTime.UtcNow).Year - dateOnly.Year;

            if (dateOnly > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-age)) age--;

            return age;
        }
    }
}
