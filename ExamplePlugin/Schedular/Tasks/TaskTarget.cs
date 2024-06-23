using ECommons.GameFunctions;
using ECommons.Throttlers;
using ExamplePlugin.Util;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskTarget
{
    internal static unsafe void Enqueue(uint dataId)
    {
            var target = ObjectTable.FirstOrDefault(o => o.IsTargetable && o.IsHostile() && !o.IsDead && o.DataId == dataId);
            if (target != default)
            {
                TargetSystem.Instance()->Target = (GameObject*)target.Address;
            }
        }
}
