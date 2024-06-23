using ECommons.Automation;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskFindAlex
{
    internal static unsafe void Enqueue(uint DutySelected)
    {
        if (!CorrectDuty())
        {
            if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
            {
                PluginLog.Information("Firing Pcall to select Duty");
                Callback.Fire(addon, true, 3, DutySelected);
            }
        }
    }
}

