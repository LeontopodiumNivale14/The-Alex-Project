using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskInteractObject
{
    internal static unsafe void Enqueue(uint dataId)
    {
        if (ObjectTable.TryGetFirst(e => e.DataId == dataId, out var obj))
        {
            if (TargetSystem.Instance()->Target == (GameObject*)obj.Address)
            {
                TargetSystem.Instance()->InteractWithObject((GameObject*)obj.Address, false);
            }

            if (EzThrottler.Throttle("Interact" + dataId))
            {
                TargetSystem.Instance()->Target = (GameObject*)obj.Address;
            }
        }
    }
}
