using ECommons.Automation;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;


public class FindAlexTask(uint DutySelected) : IBaseTask // Currently needs work/doesn't work RN
{
    public unsafe bool? Run()
    {
        if (CorrectDuty())
        {
            return true;
        }
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            PluginLog.Information("Firing Pcall to select Duty");
            Callback.Fire(addon, true, 3, DutySelected);
            return true;
        }

        return false;
    }
}

