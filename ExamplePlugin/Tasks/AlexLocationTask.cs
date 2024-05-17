using ECommons.Automation;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;
// Still a WIP here
/* 
public class AlexLocationTask : IBaseTask // Currently needs work/doesn't work RN
{
    public unsafe bool? Run()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            Callback.Fire(addon, true, 3, DutySelected);
        }
            if (!CorrectDuty())
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
*/
