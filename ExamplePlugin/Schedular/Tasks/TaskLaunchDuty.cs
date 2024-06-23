using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskLaunchDuty
{
    internal static unsafe void Enqueue()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Addon for 'Launch Duty' has been sucessfully found");
            if (EzThrottler.Throttle("Commencing the duty", 2000))
            {
                Callback.Fire(addon, true, 12, 0);
                PluginLog.Debug("Pressing the join button");
            }
        }
    }
}
