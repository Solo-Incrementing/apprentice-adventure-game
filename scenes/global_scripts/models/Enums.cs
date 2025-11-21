using System.Text.Json.Serialization;

// comparison types for requirements conditionals
public enum ComparisonType
{
    GreaterThanOrEqual,
    LessThan
}

// operation types for modification to stats
public enum OperationType
{
    Change, // applies a relative shift (+Value or -Value)
    Set     // overwrites the current stat with the exact Value
}

public enum SignificanceType
{
    Minor,
    Major,
    Significant
}

// define what background to use for a scenario
public enum BackgroundType
{
    BedroomDawn,
    BedroomMorning,
    BedroomEvening,
    OnLaptopDesk,
    OffLaptopDesk,
    OfficeEmpty,
    OfficeFull,
    OfficeCommunal
}

public enum CharacterType
{
    Narrator,
    SoftwareEngineer,
    SeniorSoftwareEngineer,
    Coworker1,
    Coworker2,
}

// higher level game states
public enum GameState
{
    MainMenu,
    InGame,
    OutcomeScreen
}