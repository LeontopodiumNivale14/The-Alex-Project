using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons;
using ECommons.Automation;
using ECommons.DalamudServices;
using ECommons.ExcelServices.TerritoryEnumeration;
using ECommons.GameFunctions;
using ECommons.GameHelpers;
using ECommons.Reflection;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System.Linq;
using System.Numerics;

namespace ExamplePlugin.Util;

/**
 * Utility class for common methods used like everywhere
 */
public static class Utils
{
    public static float SprintCD => Player.Status.FirstOrDefault(s => s.StatusId == 50)?.RemainingTime ?? 0;
    public static unsafe bool DutyOpen => (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon));
    public static unsafe bool ContentFinderWindow => (TryGetAddonByName<AtkUnitBase>("ContentsFinderConfirm", out var addon) && IsAddonReady(addon));

    public static bool NotBusy()
    {
        return Player.Available
               && Player.Object.CastActionId == 0
               && !IsOccupied()
               && !Svc.Condition[ConditionFlag.Jumping]
               && Player.Object.IsTargetable;
    }

    public static unsafe bool CorrectDuty() // first actual function I made that returns a true/false statement in C#... man this was a pain to learn about xD
    {

        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
        {
            var mainAddon = ((AddonContentsFinder*)addon)->SelectedDutyTextNodeSpan[0].Value->NodeText.ToString();
            var AlexText = "Alexander - The Burden of the Father";
            return mainAddon == AlexText;
        }
        return false;
    }

    public static unsafe bool AlexSelected()
    {
        if (TryGetAddonByName<AtkUnitBase>("JournalDetail", out var addon) && IsAddonReady(addon))
        {
            var mainAddon = ((AddonJournalDetail*)addon)->DutyLevelTextNode->NodeText.ToString();
            var AlexText = "Alexander - The Burden of the Father";
            return mainAddon == AlexText;
        }
        return false;
    }

    public static unsafe bool InA4NZone()
    {
        return Player.Territory == DataIDs.A4NMapId;
    }

    public static bool PluginInstalled(string name)
    {
        return DalamudReflector.TryGetDalamudPlugin(name, out _, false, true);
    }

    public static unsafe bool IsMoving()
    {
        return AgentMap.Instance()->IsPlayerMoving == 1;
    }

    public static float Distance(this Vector3 v, Vector3 v2)
    {
        return new Vector2(v.X - v2.X, v.Z - v2.Z).Length();
    }

    /*
    public static unsafe bool DoesObjectExist(string name)
    {
        return GetGameObjectFromName(name) != null;
    }
    */
}
