using Godot;

public partial class MainMenu : Control
{
    [Export] public Button PlayButton { get; set; } = null;

    [Export] public Button QuitButton { get; set; } = null;

    public override void _Ready()
    {
        if (PlayButton != null)
        {
            PlayButton.Pressed += OnNewGameButtonPressed;
        }

        if (QuitButton != null)
        {
            QuitButton.Pressed += OnQuitButtonPressed;
        }
    }

    private void OnNewGameButtonPressed()
    {
        var gameFlow = GetNode<GameFlowController>("/root/GameFlowController");

        gameFlow.StartNewGame();
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}