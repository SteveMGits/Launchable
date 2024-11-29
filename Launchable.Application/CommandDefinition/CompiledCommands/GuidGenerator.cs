using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launchable.Core.CommandDefinition.CompiledCommands;

public class GuidGenerator : CommandDefinitionBase
{
    public override async Task RunCommandAsync(Window param)
    {
        var clipboard = param.Clipboard;

        if(clipboard is not null)
            await clipboard.SetTextAsync(Guid.NewGuid().ToString());
    }
}
