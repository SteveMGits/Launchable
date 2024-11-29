using Avalonia;
using Avalonia.Controls;
using Launchable.Core;
using SharpHook;
using System;
using System.Threading;

namespace Launchable.UI;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    => BuildAvaloniaApp().Start(AppMain, args);

    // Application entry point. Avalonia is completely initialized.
    static void AppMain(Application app, string[] args)
    {
        // A cancellation token source that will be 
        // used to stop the main loop
        var cts = new CancellationTokenSource();

        // Do your startup code here
        CommandLibrary.HydrateCommands();

        // Start the main loop
        app.Run(cts.Token);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();


}
