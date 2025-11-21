using System.Collections.Generic;

public static class AssetMapper
{
    private static readonly Dictionary<BackgroundType, string> BackgroundMap = new()
    {
        {BackgroundType.BedroomMorning, "res://assets/images/backgrounds/bedroom_morning.png"},
        {BackgroundType.BedroomEvening, "res://assets/images/backgrounds/bedroom_evening.png" },
        {BackgroundType.BedroomDawn, "res://assets/images/backgrounds/bedroom_dawn.png" },
        {BackgroundType.OnLaptopDesk, "res://assets/images/backgrounds/laptop_on_desk.png" },
        {BackgroundType.OffLaptopDesk, "res://assets/images/backgrounds/laptop_off_on_desk.png" },
        {BackgroundType.OfficeEmpty, "res://assets/images/backgrounds/office_room_empty.png" },
        {BackgroundType.OfficeFull, "res://assets/images/backgrounds/office_room_full.png" },
        {BackgroundType.OfficeCommunal, "res://assets/images/backgrounds/office_communal_space.png"},
    };

    private static readonly Dictionary<CharacterType, string> CharacterMap = new()
    {
        {CharacterType.Narrator, ""},
        {CharacterType.SoftwareEngineer, "res://assets/images/characters/software_engineer.png"},
        {CharacterType.SeniorSoftwareEngineer, "res://assets/images/characters/senior_software_engineer.png"},
        {CharacterType.Coworker1, "res://assets/images/characters/female_coworker_1.png"},
        {CharacterType.Coworker2, "res://assets/images/characters/female_coworker_2.png"}
    };

    public static string GetBackgroundPath(BackgroundType key)
    {
        return BackgroundMap.TryGetValue(key, out var path) ? path : string.Empty;
    }

    public static string GetCharacterPath(CharacterType key)
    {
        return CharacterMap.TryGetValue(key, out var path) ? path : string.Empty;
    }
}