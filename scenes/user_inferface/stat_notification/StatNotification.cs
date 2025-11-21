using Godot;

public partial class StatNotification : PanelContainer
{
	[Export]
	public Label StatLabel { get; set; }

    private string _notificationText = "";
    private bool _isPositive = false;

    // called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Hide();

        // --- APPLY CHANGES HERE WHEN READY ---
        if (StatLabel != null)
        {
            StatLabel.Text = _notificationText;
        }
        else
        {
            // Defensive error check remains useful
            GD.PrintErr("FATAL ERROR: StatLabel is NOT wired up in _Ready()!");
            return;
        }

        // Apply positive/negative theme after StatLabel is ready
        if (_isPositive)
        {
            SetPositive();
        }
        else
        {
            SetNegative();
        }

        // PlayPopup will now run after all setup is complete
        PlayPopup();
    }

    public void Setup(string text, bool isPositive)
    {
        _notificationText = text;
        _isPositive = isPositive;
    }

    public void PlayPopup()
    {
        var tween = GetTree().CreateTween();

        Show();
        tween.TweenInterval(4);
        tween.TweenProperty(this, "modulate",
            Color.FromHsv(SelfModulate.H, SelfModulate.S, SelfModulate.V, 0), 1);

        tween.Finished += QueueFree;
    }

    public void SetPositive()
    {
        
        if (Theme != null)
        {
            ThemeTypeVariation = "PositiveStatNotificationContainer";
        }
        else
        {
            GD.PrintErr("WARNING: StatNotification (PanelContainer) is missing a Theme resource.");
        }

        if (StatLabel != null && StatLabel.Theme != null)
        {
            StatLabel.ThemeTypeVariation = "PositiveStatNotificationLabel";
        }
        else
        {
            GD.PrintErr($"WARNING: StatLabel is null: {StatLabel == null}. OR StatLabel.Theme is null: {StatLabel?.Theme == null}");
        }
    }

    public void SetNegative()
    {
        if (Theme != null)
        {
            ThemeTypeVariation = "NegativeStatNotificationContainer";
        }

        if (StatLabel != null && StatLabel.Theme != null)
        {
            StatLabel.ThemeTypeVariation = "NegativeStatNotificationLabel";

        }
    }
}
