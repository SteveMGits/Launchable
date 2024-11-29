using Avalonia.Controls;
using Avalonia.Input;
using Launchable.Core;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Launchable.UI.Views;

public partial class MainWindow : Window
{
    private bool shouldShow = false;


    public MainWindow()
    {
        InitializeComponent();

        Closing += HandleClosing;
    }

    public void KeyHandler(object sender, KeyEventArgs args)
    {
        if (args.Key == Key.Enter)
        {
            var command = MainInput.Text;

            if (string.IsNullOrWhiteSpace(command))
                return;

            if(CommandLibrary.Commands.TryGetValue(command, out var commandDefinition))
            {
                Task.Run(() => commandDefinition.RunCommandAsync(this));
                MainInput.Text = "";
                Hide();
            }
            else
            {
                // SJM TODO - how to handle
            }
        }
    }

    private void HandleClosing(object? sender, WindowClosingEventArgs args)
    {
        Hide();
        args.Cancel = true;
    }
}
