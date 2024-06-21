using ECommons.GameFunctions;
using ECommons.Throttlers;
using ExamplePlugin.Util;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons;

namespace ExamplePlugin.Tasks;

public class TargetTask(uint dataId)
{
    public unsafe bool? Run()
    {
        var target = ObjectTable.FirstOrDefault(o => o.IsTargetable && o.IsHostile() && !o.IsDead && o.DataId == dataId);
        if (target != default)
        {
            TargetSystem.Instance()->Target = (GameObject*)target.Address;
            return true;
        }
        return false;
    }
}
