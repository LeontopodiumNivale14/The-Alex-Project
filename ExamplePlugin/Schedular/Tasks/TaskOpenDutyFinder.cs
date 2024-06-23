
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskOpenDutyFinder
{
    internal static unsafe void Enqueue()
    {
        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && !IsAddonReady(addon))
        {
            if (EzThrottler.Throttle("Open Duty Finder", 5000)) // Time set to make sure it doesn't spam it opening/closing
            { // Throttle to prevent spamming
                AgentContentsFinder.Instance()->AgentInterface.Show(); // Opens the duty finder
                ContentsFinder.Instance()->IsUnrestrictedParty = true; // Sets the DF to unsync
            }
            AgentContentsFinder.Instance()->OpenRegularDuty(115);
        }
    }
}
