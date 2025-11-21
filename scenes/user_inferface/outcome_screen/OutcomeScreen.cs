using Godot;

public partial class OutcomeScreen : Control
{
    [Export] public Label ApprenticeshipLabel { get; set; } = null;
    [Export] public Label HealthLabel { get; set; } = null;
    [Export] public Label ReputationLabel { get; set; } = null;
    [Export] public Label JobProgressLabel { get; set; } = null;

    [Export] public Button MainMenuButton { get; set; } = null;

    public override void _Ready()
    {
        if (MainMenuButton != null)
        {
            var controller = GetNode<GameFlowController>("/root/GameFlowController");
            MainMenuButton.Pressed += () => controller.ChangeState(GameState.MainMenu);
        }
    }

    public void SetFinalStats(GameStats finalStats)
    {
        if (ApprenticeshipLabel != null)
        {
            ApprenticeshipLabel.Text = $"Apprenticeship Progress: {finalStats.ApprenticeshipProgress}%";
        }
        if (HealthLabel != null)
        {
            HealthLabel.Text = $"Final Health: {finalStats.Health}%";
        }
        if (ReputationLabel != null)
        {
            ReputationLabel.Text = $"Reputation Progress: {finalStats.Reputation}%";
        }
        if (JobProgressLabel != null)
        {
            JobProgressLabel.Text = $"Job Progress: {finalStats.JobProgress}%";
        } 
    }
}