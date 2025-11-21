using System.Collections.Generic;

// defines a choice the player can make
public class DecisionOption
{
    public string Text { get; set; } = string.Empty;
    public int NextNodeIndex { get; set; }
    public List<StatRequirement> Requirements { get; set; } = new List<StatRequirement>();
    public List<StatModifier> Modifiers { get; set; } = new List<StatModifier>();
}

// defines a conditional path for text-only nodes
public class NextScenarioCheck
{
    public int NextScenarioIndex { get; set; }
    public List<StatRequirement> Requirements { get; set; } = new List<StatRequirement>();

    // modifiers on next scenarios are optional for flow consequences like end-of-day stat changes
    public List<StatModifier>? Modifiers { get; set; }
}