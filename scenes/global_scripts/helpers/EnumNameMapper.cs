using System.Collections.Generic;

public static class EnumNameMapper
{
    private static readonly Dictionary<CharacterType, string> CharacterMap = new()
    {
        {CharacterType.Narrator, ""},
        {CharacterType.SoftwareEngineer, "Software Engineer"},
        {CharacterType.SeniorSoftwareEngineer, "Senior Software Engineer"},
        {CharacterType.FinanceCoworker, "Finance Coworker"},
        {CharacterType.HrCoworker, "HR Coworker"}
    };

    private static readonly Dictionary<Stat, string> StatMap = new()
    {
        {Stat.Time, "Time" },
        {Stat.Health, "Health" },
        {Stat.Reputation, "Reputation" },
        {Stat.Day, "Day" },
        {Stat.Job, "Job Progress" },
        {Stat.Apprenticeship, "Apprenticeship Progress" }
    };

    public static string GetCharacterName(CharacterType key)
    {
        return CharacterMap.TryGetValue(key, out var path) ? path : string.Empty;
    }

    public static string GetStatName(Stat key)
    {
        return StatMap.TryGetValue(key, out var path) ? path : string.Empty;
    }
}