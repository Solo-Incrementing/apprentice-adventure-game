// represents a required condition to show an option or follow a path
public class StatRequirement
{
    public Stat Stat { get; set; }
    public ComparisonType Comparison { get; set; }
    public int ComparisonValue { get; set; }
}

// represents a change to be applied to a stat
public class StatModifier
{
    public Stat Stat { get; set; }
    public OperationType Operation { get; set; }
    public int Value { get; set; }
    public bool IsEffectPositive { get; set; }
    public SignificanceType Significance { get; set; }
}