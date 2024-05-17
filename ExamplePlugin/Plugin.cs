using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using ECommons;
using ECommons.Automation;
using ExamplePlugin.Service;
using ExamplePlugin.Windows;
using ECommons.Reflection;
using ExamplePlugin.IPC;

namespace ExamplePlugin;

/**
 * Main plugin class and entry point.
 */
public sealed class Plugin : IDalamudPlugin
{
    public readonly WindowSystem WindowSystem = new("SamplePlugin");
    private readonly MainWindow mainWindow;
    private readonly ExampleService exampleService;

    /**
     * Plugin constructor. First method that is called when the plugin is loaded.
     */
    public Plugin(DalamudPluginInterface? pluginInterface)
    {
        pluginInterface?.Create<PluginService>();
        P = this;

        ECommonsMain.Init(pluginInterface, this, Module.ObjectFunctions);
        ECommonsMain.Init(pluginInterface, this, Module.DalamudReflector);
        DalamudReflector.RegisterOnInstalledPluginsChangedEvents(() =>
        {
            if (PluginInstalled(NavmeshIPC.Name))
                NavmeshIPC.Init();
        });

        Config = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        mainWindow = new MainWindow();
        WindowSystem.AddWindow(mainWindow);
        
        exampleService = new ExampleService();
        Example = exampleService;
        
        PluginService.TaskManager = new TaskManager();

        EzCmd.Add("/example", OnCommand,"It's great!");

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenMainUi += ShowMainWindow;
    }

    /**
     * Dispose method. Called when the plugin is unloaded.
     */
    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();
        mainWindow.Dispose();
        exampleService.Dispose();
        ECommonsMain.Dispose();
    }

    private void OnCommand(string command, string args)
    {
        ShowMainWindow();
    }

    private void DrawUI() => WindowSystem.Draw();

    public void ShowMainWindow()
    {
        mainWindow.IsOpen = true;
    }
}
