using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonStoryLoader
{
    public static Dictionary<int, Scenario> LoadStory(string filePath)
    {
        string jsonText = ReadFile(filePath);

        if (string.IsNullOrEmpty(jsonText))
        {
            GD.PrintErr($"Failed to read file: {filePath}");
            return new Dictionary<int, Scenario>();
        }

        List<Scenario>? scenarioList;
        try
        {
            scenarioList = JsonSerializer.Deserialize<List<Scenario>>(jsonText, GetSerializerOptions());
        }
        catch (JsonException ex)
        {
            GD.PrintErr($"JSON Deserialization Error in {filePath}: {ex.Message}");
            return new Dictionary<int, Scenario>();
        }

        if (scenarioList == null)
        {
            GD.PrintErr($"Deserialized list is null for file: {filePath}");
            return new Dictionary<int, Scenario>();
        }

        return scenarioList.ToDictionary(s => s.Id, s => s);
    }

    private static string ReadFile(string filePath)
    {
        if (!FileAccess.FileExists(filePath))
        {
            return string.Empty;
        }

        using var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            return string.Empty;
        }

        return file.GetAsText();
    }

    private static JsonSerializerOptions GetSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        // critical fix to tell the deserializer to map JSON strings to enum names
        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}