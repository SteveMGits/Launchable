using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchable.Core.CommandDefinition.CompiledCommands
{
    public static class CompiledCommandFactory
    {
        // SJM TODO - read and load these from the assembly
        public static Dictionary<string, CommandDefinitionBase> CompiledCommands() =>
            new Dictionary<string, CommandDefinitionBase>
            {
                { "guidgen", new GuidGenerator() }
            };
    }
}
