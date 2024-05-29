using System;
using System.Timers;
using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using ECommons.GameHelpers;
using ExamplePlugin.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using ExamplePlugin.Util;
using ECommons.GameFunctions;
using Dalamud.Game.ClientState;
using static System.Net.Mime.MediaTypeNames;
using ExamplePlugin.IPC;

namespace ExamplePlugin.Service;

public class ExampleService : IDisposable
{
    public string GetTargetName() => Svc.Targets.Target?.Name.TextValue ?? "";

    private readonly Timer updateTimer;
    private bool enabled; // private property representing the state of IsEnabled

    public bool IsEnabled // Public property which reacts to true/false
    {
        get => enabled;
        set
        {
            enabled = value;
            if (enabled)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
    }

    /**
     * Constructor for the ExampleService class.
     */
    public ExampleService()
    {
        updateTimer = new Timer();
        updateTimer.Elapsed += OnTimerUpdate;
        updateTimer.Interval = 1000; // You watcher Tickrate in milliseconds
        updateTimer.Enabled = false;
    }

    /**
     * This method is called when the watcher is disabled.
     */
    private void Disable()
    {
        PluginLog.Information("Ending watcher.");
        updateTimer.Enabled = false;
    }

    /**
     * This method is called when the watcher is enabled.
     */
    private void Enable()
    {
        PluginLog.Information("Watcher started.");
        updateTimer.Enabled = true;
    }

    /**
     * This method is called every second by the timer. Its your "Watcher" where you can decide what to do next if ideling.
     */
    private unsafe void OnTimerUpdate(object? sender, ElapsedEventArgs e)
    {
        PluginLog.Information("Watcher tick.");
        // Check if execute thread is busy
        if (!PluginService.Tasks.IsBusy)
        {
            // execute thread is idle, do something
            if (!InA4NZone())
            {
                if (!DutyOpen && !ContentFinderWindow) // checks to make sure that you've not queued a duty, while also making sure the window isn't open
                {
                    Enqueue(new OpenDutyFinderTask());
                    PluginLog.Information("set Unrestricted + Open Duty Finder Window");
                }
                else if (DutyOpen) // If the duty window is open, then will do this
                {
                    if (CorrectDuty())
                    {
                        Enqueue(new LaunchDutyTask());
                        PluginLog.Information("The correct duty has been selected!");
                    }
                    else if (!CorrectDuty())
                    {
                        if (TryGetAddonByName<AtkUnitBase>("ContentsFinder", out var addon) && IsAddonReady(addon))
                        {
                            for (uint  i = 0; i < 104; i++)
                            {
                                Enqueue(new FindAlexTask(i));
                                if (CorrectDuty())
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (ContentFinderWindow) // if a duty has been commenced, then will proceed to launch said duty
                {
                    Enqueue(new ConfirmDutyTask());
                    PluginLog.Information("Duty Confirm has been launched");
                }
            }
            else if (InA4NZone())
            {
                PluginLog.Information("Location is currently: A4N");
                if (!Svc.Condition[ConditionFlag.InCombat]) // checks to see if you're in combat
                {
                    if (GetTargetName() == "") // makes sure you have nothing targeted
                    {
                        if (ObjectTable.TryGetFirst(e => DataIDs.A4NIDs.Contains(e.DataId) && e.IsTargetable && !e.IsDead, out var npc))
                        {
                            Enqueue(new TargetTask(npc.DataId), 100);
                            Enqueue(new PathfindTask(npc.Position, true), 1000);
                            if (npc.Position.Distance(Player.GameObject->Position) <= 5f)
                            {
                                NavmeshIPC.PathStop();
                            }
                        }
                    }
                }
                else if (Svc.Condition[ConditionFlag.InCombat]) // makes sure you're IN combat
                {
                    if (GetTargetName() == "") // nothing targeted here
                    {
                        /*  this is kinda immportant for me to note here: 
                            This is getting all the current targets that are
                                1. Targetable (actually targetable)
                                2. Not Dead
                            and whatever comes up first in that list and matches what's in DataID's "A4NIDs" will immediately be put into a variable that can be used below
                        */
                        if (ObjectTable.TryGetFirst(e => DataIDs.A4NIDs.Contains(e.DataId) && e.IsTargetable && !e.IsDead, out var npc))
                        {
                            Enqueue(new TargetTask(npc.DataId), 100); // targeting task for A4N while in combat
                            Enqueue(new PathfindTask(npc.Position, true), 1000); // this is a problem
                            if (npc.Position.Distance(Player.GameObject->Position) <= 5f)
                            {
                                NavmeshIPC.PathStop();
                            }
                        }
                    }
                }
            }
        }
    }

    public void Dispose()
    {
        updateTimer.Dispose();
    }
}
