using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskConfirmDuty
{
    public static unsafe void Enqueue()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinderConfirm", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Commencing the duty");
            if (EzThrottler.Throttle("Commencing the duty", 1000))
            {
                Callback.Fire(addon, true, 8);
                PluginLog.Debug("Pressing the commence button");
            }
        }
    }
}
