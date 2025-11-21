using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class AssetMapperTest
{
    [TestCase]
    public void Test_GetBackgroundPath_ReturnsCorrectPath()
    {
        AssertString(AssetMapper.GetBackgroundPath(BackgroundType.BedroomMorning))
            .IsEqual("res://assets/images/backgrounds/bedroom_morning.png");

        AssertString(AssetMapper.GetBackgroundPath(BackgroundType.OfficeFull))
            .IsEqual("res://assets/images/backgrounds/office_room_full.png");
    }

    [TestCase]
    public void Test_GetBackgroundPath_ReturnsEmpty_OnInvalid()
    {
        AssertString(AssetMapper.GetBackgroundPath((BackgroundType)999))
            .IsEqual("");
    }

    [TestCase]
    public void Test_GetCharacterPath_ReturnsCorrectPath()
    {
        AssertString(AssetMapper.GetCharacterPath(CharacterType.SoftwareEngineer))
            .IsEqual("res://assets/images/characters/software_engineer.png");

        AssertString(AssetMapper.GetCharacterPath(CharacterType.Coworker2))
            .IsEqual("res://assets/images/characters/female_coworker_2.png");
    }

    [TestCase]
    public void Test_GetCharacterPath_ReturnsEmpty_OnInvalid()
    {
        AssertString(AssetMapper.GetCharacterPath((CharacterType)999))
            .IsEqual("");
    }
}