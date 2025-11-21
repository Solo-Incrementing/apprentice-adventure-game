using System.Collections.Generic;

public static class EnumNameMapper
{
    private static readonly Dictionary<CharacterType, string> CharacterMap = new()
    {
        {CharacterType.Narrator, ""},
        {CharacterType.SoftwareEngineer, "Software Engineer"},
        {CharacterType.SeniorSoftwareEngineer, "Senior Software Engineer"},
        {CharacterType.Coworker1, "Finance Coworker"},
        {CharacterType.Coworker2, "Other Deparment Coworker"}
    };

    public static string GetCharacterName(CharacterType key)
    {
        return CharacterMap.TryGetValue(key, out var path) ? path : string.Empty;
    }
}