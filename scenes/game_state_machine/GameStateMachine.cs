using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class GameStateMachine : Node
{
    private Dictionary<int, Scenario> _storyGraph = new();
    private GameStats _gameStats { get; set; } = new GameStats();

    private int _currentScenarioId = 0;
    private const string STORY_FILE_PATH = "res://data/story_data/story_data_vertical_slice.json";

    // UI references
    [Export] public TextureRect BackgroundDisplay { get; set; } = null!;
    [Export] public TextureRect CharacterDisplay { get; set; } = null!;
    [Export] public Label DayLabel { get; set; } = null;
    [Export] public Label TimeLabel { get; set; } = null;
    [Export] public Label DialogueLabel { get; set; } = null;
    [Export] public Label SpeakerLabel { get; set; } = null;
    [Export] public Control OptionsContainer { get; set; } = null;
    [Export] public Control NotificationsContainer { get; set; } = null;
    [Export] public Button ContinueButton { get; set; } = null;
    [Export] public PackedScene ChoiceButtonScene { get; set; } = null;
    [Export] public PackedScene StatNotificationScene { get; set; } = null;

    public override void _Ready()
    {
        _storyGraph = JsonStoryLoader.LoadStory(STORY_FILE_PATH);
        if (!_storyGraph.Any())
        {
            GD.PrintErr("FATAL ERROR: Story graph failed to load or is empty.");
            return;
        }

        if (ContinueButton != null)
        {
            ContinueButton.Pressed += OnNextButtonPressed;
        }

        GoToScenario(_storyGraph.First().Key);
        UpdateStatsUI();
    }

    public void SetGameStats(GameStats stats)
    {
        _gameStats = stats;
        UpdateStatsUI();
    }

    private void GoToScenario(int id)
    {
        if (!_storyGraph.TryGetValue(id, out var scenario))
        {
            GD.PrintErr($"SCENARIO NOT FOUND: Could not find scenario with ID {id}. Ending flow.");
            return;
        }

        _currentScenarioId = id;

        UpdateVisuals(scenario);

        foreach (Node child in OptionsContainer.GetChildren())
        {
            child.QueueFree();
        }

        if (scenario.IsDecisionNode)
        {
            HandleDecisionNode(scenario);
        }
        else
        {
            HandleTextOnlyNode(scenario);
        }
    }

    private bool CheckRequirements(List<StatRequirement> requirements)
    {
        if (requirements == null || !requirements.Any())
        {
            return true;
        }

        foreach (var req in requirements)
        {
            int statValue = _gameStats.GetStat(req.StatName);
            bool passed = false;

            switch (req.Comparison)
            {
                case ComparisonType.LessThan:
                    passed = statValue < req.ComparisonValue;
                    break;
                case ComparisonType.GreaterThanOrEqual:
                    passed = statValue >= req.ComparisonValue;
                    break;
            }

            if (!passed)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleDecisionNode(Scenario scenario)
    {
        ContinueButton.Hide();
        OptionsContainer.Show();

        foreach (var option in scenario.DecisionOptions)
        {
            bool isVisible = CheckRequirements(option.Requirements);

            if (isVisible)
            {
                var buttonInstance = ChoiceButtonScene.Instantiate<Button>();
                buttonInstance.Text = option.Text;

                buttonInstance.Pressed += () => OnChoiceSelected(option);

                OptionsContainer.AddChild(buttonInstance);
            }
        }
    }

    private void HandleTextOnlyNode(Scenario scenario)
    {
        OptionsContainer.Hide();
        ContinueButton.Show();
    }

    private void OnChoiceSelected(DecisionOption option)
    {
        foreach (var modifier in option.Modifiers)
        {
            _gameStats.ApplyModifier(modifier);
            PlayNotification(modifier);
        }

        UpdateStatsUI();
        GoToScenario(option.NextNodeIndex);
        
    }

    private void OnNextButtonPressed()
    {
        if (!_storyGraph.TryGetValue(_currentScenarioId, out var currentScenario))
        {
            return;
        }

        if (currentScenario.IsDecisionNode) return;

        foreach (var nextPath in currentScenario.NextScenarios)
        {
            if (CheckRequirements(nextPath.Requirements))
            {
                if (nextPath.Modifiers != null)
                {
                    foreach (var modifier in nextPath.Modifiers)
                    {
                        _gameStats.ApplyModifier(modifier);
                        PlayNotification(modifier);
                    }
                }

                UpdateStatsUI();

                CheckForGameEndCondition();
                GoToScenario(nextPath.NextScenarioIndex);
                
                return;
            }
        }

        GD.PrintErr($"FLOW ERROR: No valid path found from scenario {_currentScenarioId}.");
        EndGame();
    }

    private void UpdateStatsUI()
    {
        DayLabel.Text = $"Day {_gameStats.DayNumber}";
        TimeLabel.Text = TimeUtility.MinutesTo12HourString(_gameStats.DayTime);
    }

    private void UpdateVisuals(Scenario scenario)
    {
        string backgroundPath = AssetMapper.GetBackgroundPath(scenario.Background);
        string characterPath = AssetMapper.GetCharacterPath(scenario.Speaker);

        SetTexture(BackgroundDisplay, backgroundPath);
        SetTexture(CharacterDisplay, characterPath);

        SpeakerLabel.Text = EnumNameMapper.GetCharacterName(scenario.Speaker);
        DialogueLabel.Text = scenario.Dialogue;
    }

    private void SetTexture(TextureRect displayNode, string path)
    {
        if (displayNode == null) return;

        if (string.IsNullOrEmpty(path))
        {
            displayNode.Visible = false;
            return;
        }

        displayNode.Visible = true;

        Texture2D newTexture = ResourceLoader.Load<Texture2D>(path);

        if (newTexture != null)
        {
            displayNode.Texture = newTexture;
        }
        else
        {
            GD.PrintErr($"Failed to load Texture at path: {path}. Check AssetPathMapper and file system.");
            displayNode.Visible = false;
        }
    }

    private void PlayNotification(StatModifier statModifier)
    {
        var statNotificationInstance = StatNotificationScene.Instantiate<StatNotification>();
        var notificationText = "";

        switch (statModifier.Significance)
        {
            case SignificanceType.Minor:
                notificationText = statModifier.Value > 0 ? "↑" : "↓";
                break;
            case SignificanceType.Major:
                notificationText = statModifier.Value > 0 ? "↑↑" : "↓↓";
                break;
            case SignificanceType.Significant:
                notificationText = statModifier.Value > 0 ? "↑↑↑↑" : "↓↓↓↓";
                break;
        }

        notificationText += $" {statModifier.StatName}";

        statNotificationInstance.Setup(notificationText, statModifier.IsEffectPositive);

        NotificationsContainer.AddChild(statNotificationInstance);
    }

    public void CheckForGameEndCondition()
    {
        if (_gameStats.DayNumber > GameStats.FINAL_DAY)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        var gameFlow = GetNode<GameFlowController>("/root/GameFlowController");
        gameFlow.EndGameFlow();
    }
}