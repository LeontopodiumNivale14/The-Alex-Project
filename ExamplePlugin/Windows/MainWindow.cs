using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ExamplePlugin.Service;
using ImGuiNET;

namespace ExamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow()
        : base("My Amazing Window##With a hidden ID", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Text($"Enable me here:");
        var enabled = Example.IsEnabled;
        if (ImGui.Checkbox("Enable", ref enabled))
        {
            Example.IsEnabled = enabled;
        }

        ImGui.Text($"Activation Button:");
        var buttonpress = Example.IsEnabled;
        if (ImGui.Button("Blahhh"))
        {
            // on click
        }
    }
}
