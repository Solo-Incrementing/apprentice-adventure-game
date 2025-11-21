using System.Collections.Generic;

// the main node representing a slice of the story
public class Scenario
{
    public int Id { get; set; }
    public CharacterType Speaker { get; set; }
    public string Dialogue { get; set; } = string.Empty;
    public bool IsMajorDecision { get; set; }
    public BackgroundType Background { get; set; }

    public List<DecisionOption> DecisionOptions { get; set; } = new List<DecisionOption>();
    public List<NextScenarioCheck> NextScenarios { get; set; } = new List<NextScenarioCheck>();

    // helper to determine if it's a decision node or text-only node
    public bool IsDecisionNode => DecisionOptions.Count > 0;
}