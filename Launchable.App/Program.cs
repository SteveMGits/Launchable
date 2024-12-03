using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Launchable.Core;
using SharpHook;
using System;
using System.Threading;

namespace Launchable.UI;

class Program
{
    // This method is needed for IDE previewer infrastructure
    public static AppBuilder BuildAvaloniaApp()
      => AppBuilder.Configure<App>().UsePlatformDetect();

    // The entry point. Things aren't ready yet, so at this point
    // you shouldn't use any Avalonia types or anything that expects
    // a SynchronizationContext to be ready
    public static int Main(string[] args)
    {
        CommandLibrary.HydrateCommands();
        return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}
