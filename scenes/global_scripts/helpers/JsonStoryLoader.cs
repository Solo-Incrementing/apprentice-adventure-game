using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonStoryLoader
{
    public static Dictionary<int, Scenario> LoadStory(string[] filePaths)
    {
        List<Scenario> finalScenarioList = new List<Scenario>();

        foreach (var path in filePaths)
        {
            string jsonText = ReadFile(path);

            if (string.IsNullOrEmpty(jsonText))
            {
                GD.PrintErr($"Failed to read file: {path}");
                return new Dictionary<int, Scenario>();
            }

            List<Scenario>? scenarioList;

            try
            {
                scenarioList = JsonSerializer.Deserialize<List<Scenario>>(jsonText, GetSerializerOptions());
            }
            catch (JsonException ex)
            {
                GD.PrintErr($"JSON Deserialization Error in {path}: {ex.Message}");
                return new Dictionary<int, Scenario>();
            }

            if (scenarioList == null)
            {
                GD.PrintErr($"Deserialized list is null for file: {path}");
                return new Dictionary<int, Scenario>();
            }

            finalScenarioList.AddRange(scenarioList);
        }

        return finalScenarioList.ToDictionary(s => s.Id, s => s);
    }

    private static string ReadFile(string filePath)
    {
        if (!Godot.FileAccess.FileExists(filePath))
        {
            return string.Empty;
        }

        using var file = Godot.FileAccess.Open(filePath, Godot.FileAccess.ModeFlags.Read);
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