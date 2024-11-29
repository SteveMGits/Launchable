using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Launchable.UI.Views;
using SharpHook;
using SharpHook.Native;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Launchable.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private KeyCode[] showShortcut = [KeyCode.VcLeftControl, KeyCode.VcLeftAlt, KeyCode.VcW];

    private List<KeyCode> pressedKeys = new List<KeyCode>();

    public override void OnFrameworkInitializationCompleted()
    {
        var mainWindow = new MainWindow();

        var hook = new TaskPoolGlobalHook();

        hook.KeyPressed += (object? sender, KeyboardHookEventArgs e) =>
        {
            pressedKeys.Add(e.Data.KeyCode);

            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i] != showShortcut[i])
                {
                    pressedKeys.Clear();
                    return;
                }
            }

            if (pressedKeys.Count == showShortcut.Length)
            {
                pressedKeys.Clear();
                Dispatcher.UIThread.Post(mainWindow.Show);
            }
        };

        var thread = new Thread(hook.Run)
        {
            IsBackground = true
        };
            
        thread.Start();


        base.OnFrameworkInitializationCompleted();
    }
}