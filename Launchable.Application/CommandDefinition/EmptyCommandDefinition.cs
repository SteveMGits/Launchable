using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchable.Core.CommandDefinition;

public class EmptyCommandDefinition : CommandDefinitionBase
{
    public string? Type { get; set; }

    public override Task RunCommandAsync()
    {
        throw new NotImplementedException("Can't execute an Empty Command Definition!");
    }
}
