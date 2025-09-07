namespace ADevSolvedIt.Services;

public class DateTimeService
{
    public static string GetReadableRelativeTime(DateTime inputDateTime)
    {
        var timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - inputDateTime.Ticks);
        double delta = timeSpan.TotalSeconds;

        if (delta < 2)
        {
            return "one second ago";
        }

        if (delta < 60)
        {
            return timeSpan.Seconds + " seconds ago";
        }

        if (delta < 120)
        {
            return "a minute ago";
        }

        if (delta < 3600) // 60 * 60
        {
            return timeSpan.Minutes + " minutes ago";
        }

        if (delta < 7200) // 2 * 60 * 60
        {
            return "an hour ago";
        }

        if (delta < 86400) // 24 * 60 * 60
        {
            return timeSpan.Hours + " hours ago";
        }

        return GetReadableDateTime(inputDateTime);
    }

    public static string GetReadableDateTime(DateTime creationDate)
    {
        return creationDate.ToString("MMM d, yyyy") + " at " + creationDate.ToString("HH:mm");
    }

    public static string ToZuluTime(DateTime input)
    {
        return input.ToString("yyyy-MM-dd HH:mm:ssZ");
    }
}
