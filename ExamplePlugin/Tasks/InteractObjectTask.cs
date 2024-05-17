using ECommons.Throttlers;
using ExamplePlugin.Tasks.Base;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;

namespace ExamplePlugin.Tasks;

public class InteractObjectTask(uint dataId) : IBaseTask
{
    public unsafe bool? Run()
    {
        if (ObjectTable.TryGetFirst(e => e.DataId == dataId, out var obj))
        {
            if (TargetSystem.Instance()->Target == (GameObject*)obj.Address)
            {
                TargetSystem.Instance()->InteractWithObject((GameObject*)obj.Address, false);
                return true;
            }

            if (EzThrottler.Throttle("Interact" + dataId))
            {
                TargetSystem.Instance()->Target = (GameObject*)obj.Address;
                return false;
            }
        }

        return false;
    }
}
