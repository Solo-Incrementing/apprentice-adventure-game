// represents a required condition to show an option or follow a path
public class StatRequirement
{
    public string StatName { get; set; } = string.Empty;
    public ComparisonType Comparison { get; set; }
    public int ComparisonValue { get; set; }
}

// represents a change to be applied to a stat
public class StatModifier
{
    public string StatName { get; set; } = string.Empty;
    public OperationType Operation { get; set; }
    public int Value { get; set; }
    public bool IsEffectPositive { get; set; }
    public SignificanceType Significance { get; set; }
}