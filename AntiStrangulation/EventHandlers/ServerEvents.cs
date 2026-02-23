namespace AntiStrangulation.EventHandlers
{
    internal sealed class ServerEvents
    {
        internal void Register() => LabApi.Events.Handlers.ServerEvents.RoundRestarted += OnRoundRestarted;

        internal void Unregister() => LabApi.Events.Handlers.ServerEvents.RoundRestarted -= OnRoundRestarted;

        private void OnRoundRestarted() => Plugin.StopStrangleCoroutine.Clear();
    }
}
