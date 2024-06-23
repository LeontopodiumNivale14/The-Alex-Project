using System.Linq;
using System.Numerics;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using ExamplePlugin.IPC;

namespace ExamplePlugin.Schedular.Tasks;

internal class TaskPathfind
{
    internal static unsafe void Enqueue(Vector3 targetPosition, bool sprint = false, float toleranceDistance = 3f)
    {
        NavmeshIPC.PathfindAndMoveTo(targetPosition, false);
        NavmeshIPC.PathSetAlignCamera(false);

        if (targetPosition.Distance(Player.GameObject->Position) <= toleranceDistance)
        {
            PluginLog.Information("Distance between target is less than the tolerance");
            NavmeshIPC.PathStop();
        }

        if (sprint && Player.Status.All(e => e.StatusId != 50) && SprintCD == 0
                   && ActionManager.Instance()->GetActionStatus(ActionType.GeneralAction, 4) == 0)
        {
            PluginLog.Information("Sprint is off CD");
            if (EzThrottler.Throttle("Sprint"))
            {
                ActionManager.Instance()->UseAction(ActionType.GeneralAction, 4);
            }
        }
    }
}
