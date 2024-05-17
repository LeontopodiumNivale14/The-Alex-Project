using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;

public class ConfirmDutyTask : IBaseTask // If the duty finder is not currently open, then will open it, and proceed to fire off the unsync button at the same time
{
    public unsafe bool? Run()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinderConfirm", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Commencing the duty");
            if (EzThrottler.Throttle("Commencing the duty", 1000))
            {
                Callback.Fire(addon, true, 8);
                PluginLog.Debug("Pressing the commence button");
                return true;
            }
        }
        return false;
    }
}
