using Godot;
using System;

public partial class GameFlowController : Node
{
    private const string MainMenuScenePath = "res://scenes/user_inferface/main_menu/main_menu.tscn";
    private const string MainGameScenePath = "res://scenes/game_state_machine/game_state_machine.tscn";
    private const string OutcomeScreenScenePath = "res://scenes/user_inferface/outcome_screen/outcome_screen.tscn";

    private PackedScene MainMenuScene { get; set; } = null;
    private PackedScene MainGameScene { get; set; } = null;
    private PackedScene OutcomeScreenScene { get; set; } = null;

    private GameState _currentState = GameState.MainMenu;
    private GameStats _currentGameStats = null;

    private Node _currentScene = null;

    public override void _Ready()
    {
        MainMenuScene = LoadScene(MainMenuScenePath);
        MainGameScene = LoadScene(MainGameScenePath);
        OutcomeScreenScene = LoadScene(OutcomeScreenScenePath);

        ChangeState(GameState.MainMenu);
    }

    private PackedScene LoadScene(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            GD.PrintErr("FATAL: A scene path is empty. Cannot load.");
            return null;
        }

        var loadedResource = ResourceLoader.Load(path);

        if (loadedResource is PackedScene packedScene)
        {
            return packedScene;
        }

        GD.PrintErr($"FATAL: Failed to load PackedScene from path: {path}. Check path existence and case sensitivity.");
        return null;
    }

    public void ChangeState(GameState newState)
    {
        if (_currentState == newState) return;

        if (_currentScene != null && IsInstanceValid(_currentScene))
        {
            _currentScene.QueueFree();
            _currentScene = null;
        }

        PackedScene nextScenePack = newState switch
        {
            GameState.MainMenu => MainMenuScene,
            GameState.InGame => MainGameScene,
            GameState.OutcomeScreen => OutcomeScreenScene,
            _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, "Unknown game state requested.")
        };

        if (nextScenePack == null)
        {
            GD.PrintErr($"FATAL: The PackedScene for state '{newState}' is not assigned or failed to load. Please verify the {newState}Scene export in the Inspector.");
            return;
        }

        _currentScene = nextScenePack.Instantiate();

        GetTree().Root.AddChild(_currentScene);

        _currentState = newState;

        if (newState == GameState.InGame)
        {
            InitializeGame();
        }

        if (newState == GameState.OutcomeScreen)
        {
            InitializeOutcomeScreen();
        }
    }

    public void StartNewGame()
    {
        _currentGameStats = new GameStats();

        ChangeState(GameState.InGame);
    }

    private void InitializeGame()
    {
        if (_currentScene is GameStateMachine gameStateMachine)
        {
            gameStateMachine.SetGameStats(_currentGameStats);
        }
        else
        {
            GD.PrintErr("Could not find GameStateMachine in the loaded scene.");
        }
    }

    public void EndGameFlow()
    {
        ChangeState(GameState.OutcomeScreen);
    }

    private void InitializeOutcomeScreen()
    {
        if (_currentScene is OutcomeScreen outcomeScreen)
        {
            outcomeScreen.SetFinalStats(_currentGameStats);
        }
        else
        {
            GD.PrintErr("Could not find OutcomeScreen in the loaded scene.");
        }
    }
}