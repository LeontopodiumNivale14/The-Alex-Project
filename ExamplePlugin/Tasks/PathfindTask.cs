using System.Linq;
using System.Numerics;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using ExamplePlugin.IPC;

namespace ExamplePlugin.Tasks;

public class PathfindTask(Vector3 targetPosition, bool sprint = false, float toleranceDistance = 3f) 
{
    public unsafe bool? Run()
    {
        if (targetPosition.Distance(Player.GameObject->Position) <= toleranceDistance)
        {
            PluginLog.Information("Distance between target is less than the tolerance");
            NavmeshIPC.PathStop();
            return true;
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
        
        if (NavmeshIPC.PathfindInProgress() || NavmeshIPC.PathIsRunning() || IsMoving()) return false;

        NavmeshIPC.PathfindAndMoveTo(targetPosition, false);
        NavmeshIPC.PathSetAlignCamera(false);
        
        return false;
    }
}
