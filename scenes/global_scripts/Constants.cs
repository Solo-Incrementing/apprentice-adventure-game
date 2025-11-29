using Godot;

public partial class Constants : Node
{
    public const int START_SCENARIO_ID = 0;
    public const int DAY_START_TIME = 360; // day starts at 6am by default which is 360 minutes
    public const int START_DAY = 1;
    public const int FINAL_DAY = 5;

    public const int START_HEALTH = 30;
    public const int START_REPUTATION = 30;
    public const int START_JOB_PROGRESS = 0;
    public const int START_APPRENTICESHIP_PROGRESS = 0;

    public static readonly string[] STORY_FILE_PATHS =
    [
        "res://data/story_data/morning.json",
        "res://data/story_data/work_from_home.json",
        "res://data/story_data/home_apprenticeship.json",
        "res://data/story_data/home_job.json",
        "res://data/story_data/home_social.json",
        "res://data/story_data/work_from_office.json",
        "res://data/story_data/office_apprenticeship.json",
        "res://data/story_data/office_job.json",
        "res://data/story_data/office_social.json",
        "res://data/story_data/evening.json"
    ];
}