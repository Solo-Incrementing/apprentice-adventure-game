using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class EnumNameMapperTest
{
    [TestCase]
    public void Test_GetCharacterName_ReturnsCorrectValue()
    {
        AssertString(EnumNameMapper.GetCharacterName(CharacterType.SoftwareEngineer))
            .IsEqual("Software Engineer");

        AssertString(EnumNameMapper.GetCharacterName(CharacterType.FinanceCoworker))
            .IsEqual("Finance Coworker");
    }

    [TestCase]
    public void Test_GetCharacterName_ReturnsEmpty_OnInvalid()
    {
        AssertString(EnumNameMapper.GetCharacterName((CharacterType)999))
            .IsEqual("");
    }
}