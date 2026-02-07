using LabApi.Events.Arguments.Scp3114Events;
using MEC;

namespace AntiStrangulation.EventHandlers
{
    internal sealed class ScpEvents
    {
        internal void Register()
        {
            if (!Plugin.PluginConfig.RandomStopStrangulation)
                return;
            
            LabApi.Events.Handlers.Scp3114Events.StrangleStarted += OnStrangleStarted;
            LabApi.Events.Handlers.Scp3114Events.StrangleAborted += OnStrangleAborted;
        }

        internal void Unregister()
        {
            if (!Plugin.PluginConfig.RandomStopStrangulation)
                return;

            LabApi.Events.Handlers.Scp3114Events.StrangleStarted -= OnStrangleStarted;
            LabApi.Events.Handlers.Scp3114Events.StrangleAborted -= OnStrangleAborted;
        }

        private void OnStrangleAborted(Scp3114StrangleAbortedEventArgs ev)
        {
            if (Plugin.StopStrangleCoroutine.TryGetValue(ev.Player, out CoroutineHandle coroutine) && coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);
        }

        private void OnStrangleStarted(Scp3114StrangleStartedEventArgs ev)
        {
            int stopAfter = Plugin.Random.Next(1, 8);

            if (Plugin.StopStrangleCoroutine.TryGetValue(ev.Player, out CoroutineHandle coroutine) && coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);

            Plugin.StopStrangleCoroutine[ev.Player] = Timing.RunCoroutine(Coroutines.StopStrangle(stopAfter, ev.Player, ev.Target));
        }
    }
}
