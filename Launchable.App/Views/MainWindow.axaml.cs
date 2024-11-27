using Avalonia.Controls;
using Avalonia.Input;
using Launchable.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Launchable.UI.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
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
                Task.Run(commandDefinition.RunCommandAsync);
            }
            else
            {
                // SJM TODO - how to handle
            }
        }
    }
}