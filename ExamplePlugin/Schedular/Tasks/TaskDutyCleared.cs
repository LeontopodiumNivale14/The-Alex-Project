using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Schedular.Tasks;

internal class TTaskDutyCleared
{
    internal static unsafe void Enqueue()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Addon is ready for the Clear Duty task");
            if (EzThrottler.Throttle("Duty Counter + 1", 2000))
            {
                var dutySelectText = ((AddonContentsFinder*)addon)->SelectedDutyTextNodeSpan[0].Value->NodeText.ToString();
                var dutyGoalText = "Alexander - The Burden of the Father";

                if (dutySelectText != dutyGoalText || dutySelectText != "")
                {
                    Callback.Fire(addon, true, 12, 1);
                }
            }
        }
    }
}
