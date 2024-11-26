using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Threading.Tasks;

namespace Launchable.App.Views;

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


        }
    }
}