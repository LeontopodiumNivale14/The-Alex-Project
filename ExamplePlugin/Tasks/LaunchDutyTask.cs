using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;

public class LaunchDutyTask : IBaseTask // If the duty finder is not currently open, then will open it, and proceed to fire off the unsync button at the same time
{
    public unsafe bool? Run()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Addon for 'Launch Duty' has been sucessfully found");
            if (EzThrottler.Throttle("Commencing the duty", 2000))
            {
                Callback.Fire(addon, true, 12, 0);
                PluginLog.Debug("Pressing the join button");
                return true;
            }
        }
        return false;
    }
}
