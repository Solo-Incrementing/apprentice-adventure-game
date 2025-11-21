using System;

public static class TimeUtility
{
    public static string MinutesTo12HourString(int totalMinutes)
    {
        int minutesInDay = totalMinutes % 1440;
        if (minutesInDay < 0)
        {
            // handles negative inputs by wrapping around (e.g -60 minutes becomes 11:00 PM)
            minutesInDay += 1440;
        }

        int hours = minutesInDay / 60;
        int minutes = minutesInDay % 60;

        DateTime midnight = new DateTime(2000, 1, 1, 0, 0, 0);
        DateTime timeOfDay = midnight.AddMinutes(minutesInDay);

        return timeOfDay.ToString("h:mm tt");
    }
}