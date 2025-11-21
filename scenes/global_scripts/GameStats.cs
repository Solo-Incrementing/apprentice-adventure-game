using Godot;

public partial class GameStats : Resource
{
    public const int FINAL_DAY = 5;

    [Export]
    public int DayTime { get; set; } = 360; // day starts at 6am by default which is 360 minutes
    [Export]
    public int Health { get; set; } = 30;
    [Export]
    public int Reputation { get; set; } = 0;
    [Export]
    public int DayNumber { get; set; } = 1;
    [Export]
    public int JobProgress { get; set; } = 0;
    [Export]
    public int ApprenticeshipProgress { get; set; } = 0;

    public int GetStat(string statName)
    {
        return statName switch
        {
            "Time" => DayTime,
            "Health" => Health,
            "Reputation" => Reputation,
            "Day" => DayNumber,
            "Job Progress" => JobProgress,
            "Apprenticeship Progress" => ApprenticeshipProgress,
            _ => 0
        };
    }

    public void ApplyModifier(StatModifier modifier)
    {
        int value = modifier.Value;

        switch (modifier.StatName)
        {
            case "Time":
                if (modifier.Operation == OperationType.Change) DayTime += value;
                else if (modifier.Operation == OperationType.Set) DayTime = value;
                break;
            case "Health":
                if (modifier.Operation == OperationType.Change) Health += value;
                else if (modifier.Operation == OperationType.Set) Health = value;
                break;
            case "Reputation":
                if (modifier.Operation == OperationType.Change) Reputation += value;
                else if (modifier.Operation == OperationType.Set) Reputation = value;
                break;
            case "Day":
                if (modifier.Operation == OperationType.Change) DayNumber += value;
                else if (modifier.Operation == OperationType.Set) DayNumber = value;
                break;
            case "Job Progress":
                if (modifier.Operation == OperationType.Change) JobProgress += value;
                else if (modifier.Operation == OperationType.Set) JobProgress = value;
                break;
            case "Apprenticeship Progress":
                if (modifier.Operation == OperationType.Change) ApprenticeshipProgress += value;
                else if (modifier.Operation == OperationType.Set) ApprenticeshipProgress = value;
                break;
            default:
                GD.PrintErr($"WARNING: Attempted to modify unknown stat: {modifier.StatName}");
                break;
        }

        GD.Print($"STAT MODIFIED: {modifier.StatName} changed. New value: {GetStat(modifier.StatName)}");
    }
}