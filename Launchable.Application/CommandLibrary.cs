using Launchable.Core.CommandDefinition;
using Launchable.Core.CommandDefinition.CompiledCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Launchable.Core;

public static class CommandLibrary
{
    public static void HydrateCommands()
    {
        // Flush existing commands
        Commands = CompiledCommandFactory.CompiledCommands();

        var launchableDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{Path.DirectorySeparatorChar}Launchable";

        if (!Directory.Exists(launchableDirectory))
        {
            Directory.CreateDirectory(launchableDirectory);
        }

        var commandFiles = Directory.GetFiles(launchableDirectory); // SJM TODO - make this configurable

        var deserializerBuilder = new DeserializerBuilder();
        deserializerBuilder.IgnoreUnmatchedProperties();

        var deserializer = deserializerBuilder.Build();

        foreach (var commandFile in commandFiles)
        {
            try
            {
                var fileText = File.ReadAllText(commandFile);

                // SJM TODO - how to ignore fields that are not in the serialization target
                var commandDefinition = deserializer.Deserialize<EmptyCommandDefinition>(fileText);

                if (string.IsNullOrWhiteSpace(commandDefinition.Type))
                {
                    Console.WriteLine($"No command type for {commandFile}, skipping...");
                    continue;
                }

                var commandType = Type.GetType($"Launchable.Core.CommandDefinition.{commandDefinition.Type}");

                if (commandType is null)
                {
                    Console.WriteLine($"Unable to parse command type: {commandDefinition.Type}, skipping...");
                    continue;
                }

                var fullCommand = deserializer.Deserialize(fileText, commandType);

                if (fullCommand is null)
                {
                    Console.WriteLine($"Could not parse command {commandFile} as {commandType}. Skipping...");
                    continue;
                }

                Commands.Add(commandDefinition.CommandKey ?? commandFile, (CommandDefinitionBase)fullCommand);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex}");
                Console.WriteLine($"Skipping {commandFile}...");
            }
        }
    }

    public static Dictionary<string, CommandDefinitionBase> Commands { get; set; } = new Dictionary<string, CommandDefinitionBase>();
}
