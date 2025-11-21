using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class TimeUtilityTest
{
    private void AssertTime(int minutes, string expected)
    {
        AssertString(TimeUtility.MinutesTo12HourString(minutes))
            .IsEqual(expected);
    }

    [TestCase]
    public void Test_Midnight()
    {
        AssertTime(0, "12:00 AM");
        AssertTime(1440, "12:00 AM"); // wrap-around
    }

    [TestCase]
    public void Test_EarlyMorning()
    {
        AssertTime(60, "1:00 AM");
        AssertTime(90, "1:30 AM");
        AssertTime(360, "6:00 AM");
    }

    [TestCase]
    public void Test_Noon()
    {
        AssertTime(720, "12:00 PM");
        AssertTime(780, "1:00 PM");
    }

    [TestCase]
    public void Test_Evening()
    {
        AssertTime(1020, "5:00 PM");
        AssertTime(1380, "11:00 PM");
    }

    [TestCase]
    public void Test_WrapAround_LargePositive()
    {
        AssertTime(1440 + 60, "1:00 AM");
        AssertTime(2880 + 720, "12:00 PM");
    }

    [TestCase]
    public void Test_NegativeInputs()
    {
        AssertTime(-1, "11:59 PM");
        AssertTime(-60, "11:00 PM");
        AssertTime(-720, "12:00 PM");
        AssertTime(-1441, "11:59 PM"); // wraps as expected
    }

    [TestCase]
    public void Test_Boundaries()
    {
        AssertTime(1, "12:01 AM");
        AssertTime(59, "12:59 AM");
        AssertTime(719, "11:59 AM");
        AssertTime(721, "12:01 PM");
        AssertTime(1439, "11:59 PM");
    }
}