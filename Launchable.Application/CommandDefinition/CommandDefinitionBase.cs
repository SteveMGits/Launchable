using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchable.Core.CommandDefinition;

public abstract class CommandDefinitionBase
{
    public required string CommandKey { get; set; }

    public IDictionary<string, string?> Options
    {
        get
        {
            return _options;
        }
        set
        {
            if (value is null)
                _options = new Dictionary<string, string?>();
            else
                _options = value;
        }
    }

    private IDictionary<string, string?> _options = new Dictionary<string, string?>();

    public abstract Task RunCommandAsync();
}
