using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using ECommons.Automation;

namespace ExamplePlugin.Service;

/**
 * This "PluginService" class is used to store all the services that are used in the plugin.
 * Its reflection magic an manual setting. Makes it more easy to use all services in your plugin.
 */
public class PluginService
{
    public static Plugin P = null!;

    [PluginService]
    public static DalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService]
    public static IChatGui ChatGui { get; private set; } = null!;
    [PluginService]
    public static IClientState ClientState { get; private set; } = null!;
    [PluginService]
    public static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService]
    public static ICondition Condition { get; private set; } = null!;
    [PluginService]
    public static IDataManager DataManager { get; private set; } = null!;
    [PluginService]
    public static IFlyTextGui FlyTextGui { get; private set; } = null!;
    [PluginService]
    public static IFramework Framework { get; private set; } = null!;
    [PluginService]
    public static IGameGui GameGui { get; private set; } = null!;
    [PluginService]
    public static IGameNetwork GameNetwork { get; private set; } = null!;
    [PluginService]
    public static IObjectTable ObjectTable { get; private set; } = null!;
    [PluginService]
    public static IPartyList PartyList { get; private set; } = null!;
    [PluginService]
    public static ITargetManager TargetManager { get; private set; } = null!;
    [PluginService]
    public static IPluginLog PluginLog { get; private set; } = null!;
    public static Configuration Config { get; set; } = null!;
    public static TaskManager TaskManager { get; set; } = null!;
    public static ExampleService Example { get; set; } = null!;
}
