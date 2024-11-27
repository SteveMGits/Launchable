using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchable.Core.CommandDefinition;

public class CommandLine : CommandDefinitionBase
{
    public string? ScriptFilePath { get; set; }
    public string? CommandText { get; set; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public override async Task RunCommandAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var showWindow = Options.ContainsKey("ShowOutput") && Options["ShowOutput"]?.ToString()?.ToUpper() == "TRUE" ;

            Process? proc = null;

            var shell = System.OperatingSystem.IsWindows() ? "powershell.exe" : "sh";

            if (!string.IsNullOrWhiteSpace(CommandText))
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = shell,
                        Arguments = CommandText,
                        UseShellExecute = true,
                        CreateNoWindow = showWindow,
                    }
                };
            }

            else if (!String.IsNullOrWhiteSpace(ScriptFilePath))
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments = $"'{ScriptFilePath}'",
                        UseShellExecute = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = showWindow,
                    }
                };


            }

            proc?.Start();
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
