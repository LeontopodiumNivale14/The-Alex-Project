using System;
using ExamplePlugin.Service;
using ExamplePlugin.Tasks;

namespace ExamplePlugin.Util;

/**
 * Utility class for making enqueuing tasks easier. Nothing more than delegates to TaskManager methods.
 */
public static class TaskManagerUtil
{
    public static void Enqueue(IBaseTask task, int timeLimitMs = 10000, string? name = null)
    {
        PluginService.Tasks.Enqueue(task.Run, timeLimitMs, name);
    }
    
    public static void Enqueue(IBaseTask task) => Enqueue(task, 10000);
    public static void Enqueue(IBaseTask task, string? name = null) => Enqueue(task, 10000, name);
    
    public static void Enqueue(Func<bool?> task, int timeLimitMs = 10000, string? name = null)
    {
        PluginService.Tasks.Enqueue(task, timeLimitMs, name);
    }
    
    public static void Enqueue(Func<bool?> task) => Enqueue(task, 10000);
    public static void Enqueue(Func<bool?> task, string? name = null) => Enqueue(task, 10000, name);

    public static void EnqueueWait(int delayMS)
    {
        PluginService.Tasks.DelayNext(delayMS);
    }
    
    public static void EnqueueImmediate(IBaseTask task, int timeLimitMs = 10000, string? name = null)
    {
        PluginService.Tasks.EnqueueImmediate(task.Run, timeLimitMs, name);
    }
    
    public static void EnqueueImmediate(IBaseTask task, string? name = null) => Enqueue(task, 10000, name);
    public static void EnqueueImmediate(IBaseTask task) => Enqueue(task, 10000);
    
    public static void EnqueueImmediate(Func<bool?> task, int timeLimitMs = 10000, string? name = null)
    {
        PluginService.Tasks.EnqueueImmediate(task, timeLimitMs, name);
    }
    
    public static void EnqueueImmediate(Func<bool?> task, string? name = null) => Enqueue(task, 10000, name);
    public static void EnqueueImmediate(Func<bool?> task) => Enqueue(task, 10000);
    
    public static void EnqueueWaitImmediate(int delayMS)
    {
        PluginService.Tasks.DelayNextImmediate(delayMS);
    }
}
