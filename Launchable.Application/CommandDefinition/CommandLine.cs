using Avalonia.Controls;
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

    public override Task RunCommandAsync(Window param)
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

            return Task.CompletedTask;
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromException(e);
        }
    }
}
