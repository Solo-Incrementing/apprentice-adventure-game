using Godot;

public partial class GameStats : Resource
{
    [Export]
    public int DayTime { get; set; } = Constants.DAY_START_TIME;
    [Export]
    public int Health { get; set; } = Constants.START_HEALTH;
    [Export]
    public int Reputation { get; set; } = Constants.START_REPUTATION;
    [Export]
    public int DayNumber { get; set; } = Constants.START_DAY;
    [Export]
    public int JobProgress { get; set; } = Constants.START_JOB_PROGRESS;
    [Export]
    public int ApprenticeshipProgress { get; set; } = Constants.START_APPRENTICESHIP_PROGRESS;

    public int GetStat(Stat statName)
    {
        return statName switch
        {
            Stat.Time => DayTime,
            Stat.Health => Health,
            Stat.Reputation => Reputation,
            Stat.Day => DayNumber,
            Stat.Job => JobProgress,
            Stat.Apprenticeship => ApprenticeshipProgress,
            _ => 0
        };
    }

    public void ApplyModifier(StatModifier modifier)
    {
        int value = modifier.Value;

        switch (modifier.Stat)
        {
            case Stat.Time:
                if (modifier.Operation == OperationType.Change) DayTime += value;
                else if (modifier.Operation == OperationType.Set) DayTime = value;
                break;
            case Stat.Health:
                if (modifier.Operation == OperationType.Change) Health += value;
                else if (modifier.Operation == OperationType.Set) Health = value;
                break;
            case Stat.Reputation:
                if (modifier.Operation == OperationType.Change) Reputation += value;
                else if (modifier.Operation == OperationType.Set) Reputation = value;
                break;
            case Stat.Day:
                if (modifier.Operation == OperationType.Change) DayNumber += value;
                else if (modifier.Operation == OperationType.Set) DayNumber = value;
                break;
            case Stat.Job:
                if (modifier.Operation == OperationType.Change) JobProgress += value;
                else if (modifier.Operation == OperationType.Set) JobProgress = value;
                break;
            case Stat.Apprenticeship:
                if (modifier.Operation == OperationType.Change) ApprenticeshipProgress += value;
                else if (modifier.Operation == OperationType.Set) ApprenticeshipProgress = value;
                break;
            default:
                GD.PrintErr($"WARNING: Attempted to modify unknown stat: {modifier.Stat}");
                break;
        }

        GD.Print($"STAT MODIFIED: {EnumNameMapper.GetStatName(modifier.Stat)} changed. New value: {GetStat(modifier.Stat)}");
    }
}