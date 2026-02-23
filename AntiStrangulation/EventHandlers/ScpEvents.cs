using LabApi.Events.Arguments.Scp3114Events;
using MEC;

namespace AntiStrangulation.EventHandlers
{
    internal sealed class ScpEvents
    {
        internal void Register()
        {
            if (Plugin.PluginConfig.DisableStrangulation)
                LabApi.Events.Handlers.Scp3114Events.StrangleStarting += StrangleStarting;

            if (Plugin.PluginConfig.RandomStopStrangulation && !Plugin.PluginConfig.DisableStrangulation)
            {
                LabApi.Events.Handlers.Scp3114Events.StrangleStarted += OnStrangleStarted;
                LabApi.Events.Handlers.Scp3114Events.StrangleAborted += OnStrangleAborted;
            }
        }

        internal void Unregister()
        {
            if (Plugin.PluginConfig.DisableStrangulation)
                LabApi.Events.Handlers.Scp3114Events.StrangleStarting -= StrangleStarting;

            if (Plugin.PluginConfig.RandomStopStrangulation && !Plugin.PluginConfig.DisableStrangulation)
            {
                LabApi.Events.Handlers.Scp3114Events.StrangleStarted -= OnStrangleStarted;
                LabApi.Events.Handlers.Scp3114Events.StrangleAborted -= OnStrangleAborted;
            }
        }

        private void OnStrangleAborted(Scp3114StrangleAbortedEventArgs ev)
        {
            if (Plugin.StopStrangleCoroutine.TryGetValue(ev.Player, out CoroutineHandle coroutine) && coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);
        }

        private void OnStrangleStarted(Scp3114StrangleStartedEventArgs ev)
        {
            if (Plugin.StopStrangleCoroutine.TryGetValue(ev.Player, out CoroutineHandle coroutine) && coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);

            Plugin.StopStrangleCoroutine[ev.Player] = Timing.RunCoroutine(Coroutines.StopStrangle(Plugin.Random.Next(1, 8), ev.Player, ev.Target));
        }

        private void StrangleStarting(Scp3114StrangleStartingEventArgs ev) => ev.IsAllowed = false;
    }
}
