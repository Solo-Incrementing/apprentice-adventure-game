using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class GameStatsTest
{
    private GameStats _gameStats = null!;

    // [BeforeTest] ensures this method runs before each test method in this class.
    [BeforeTest]
    public void Setup()
    {
        // initialize a new GameStats object before every test to ensure isolation.
        _gameStats = new GameStats();
    }

    [TestCase]
    public void TestInitialDayTimeIs360()
    {
        // checks that the default value for DayTime is loaded correctly.
        // as defined in GameStats.cs: public int DayTime { get; set; } = 360;
        AssertThat(_gameStats.DayTime).IsEqual(360);
    }

    [TestCase]
    public void TestHealthChangeModifierIncreasesHealth()
    {
        // ARRANGE: Setup initial state (Health starts at 30) and the modifier.
        var modifier = new StatModifier
        {
            Stat = Stat.Health,
            Operation = OperationType.Change, // Add the value
            Value = 15,
            IsEffectPositive = true,
            Significance = SignificanceType.Minor
        };

        // ACT: Apply the modifier.
        _gameStats.ApplyModifier(modifier);

        // ASSERT: Check the new state. Expected: 30 + 15 = 45.
        AssertThat(_gameStats.Health).IsEqual(45);
    }

    [TestCase]
    public void TestReputationSetModifierOverwritesValue()
    {
        // ARRANGE: Reputation starts at 0.
        var modifier = new StatModifier
        {
            Stat = Stat.Reputation,
            Operation = OperationType.Set, // Overwrite the value
            Value = 75,
            IsEffectPositive = true,
            Significance = SignificanceType.Significant
        };

        // ACT: Apply the modifier.
        _gameStats.ApplyModifier(modifier);

        // ASSERT: Check the new state. Expected: 75, regardless of initial value.
        AssertThat(_gameStats.Reputation).IsEqual(75);
    }

    [TestCase]
    public void TestJobProgressDecreasesOnNegativeChange()
    {
        // ARRANGE: Set job progress to a known value first (e.g., 50)
        _gameStats.JobProgress = 50;

        var modifier = new StatModifier
        {
            Stat = Stat.Job,
            Operation = OperationType.Change, // Subtract the value
            Value = -20,
            IsEffectPositive = false,
            Significance = SignificanceType.Major
        };

        // ACT: Apply the modifier.
        _gameStats.ApplyModifier(modifier);

        // ASSERT: Check the new state. Expected: 50 + (-20) = 30.
        AssertThat(_gameStats.JobProgress).IsEqual(30);
    }
}