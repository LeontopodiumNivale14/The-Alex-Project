
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Tasks;

public class OpenDutyFinderTask // If the duty finder is not currently open, then will open it, and proceed to fire off the unsync button at the same time
{
    public static bool DutyFinderOpen;
    public static unsafe bool? Run()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            return true;
        }

        if (EzThrottler.Throttle("Open Duty Finder", 5000)) 
        { // Throttle to prevent spamming
            AgentContentsFinder.Instance()->AgentInterface.Show(); // Opens the duty finder
            ContentsFinder.Instance()->IsUnrestrictedParty = true; // Sets the DF to unsync
            DutyFinderOpen = true;
        }
        AgentContentsFinder.Instance()->OpenRegularDuty(115);

        return false;
    }
}
