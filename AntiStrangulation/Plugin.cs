using AntiStrangulation.EventHandlers;
using HarmonyLib;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using MEC;
using System;
using System.Collections.Generic;

namespace AntiStrangulation
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "AntiStrangulation";
        public override string Description { get; } = "AntiStrangulation";
        public override string Author { get; } = "ui-2506";
        public override Version Version { get; } = new Version(2, 5, 0);
        public override Version RequiredApiVersion { get; } = new Version(1, 1, 4);

        internal static readonly Dictionary<Player, CoroutineHandle> StopStrangleCoroutine = new Dictionary<Player, CoroutineHandle>();

        internal static Config PluginConfig { get; private set; }
        internal static Random Random { get; private set; }

        private Harmony _harmony;
        private ScpEvents _scpEvents;

        public override void Enable()
        {
            PluginConfig = Config;
            Random = new Random(DateTime.Now.Second);

            _harmony = new Harmony(Name);
            _harmony.PatchAll();

            if (Config.RandomStopStrangulation)
            {
                _scpEvents = new ScpEvents();
                _scpEvents.Register();
            }
        }

        public override void Disable()
        {
            if (Config.RandomStopStrangulation)
            {
                _scpEvents.Unregister();
                _scpEvents = null;
            }

            _harmony.UnpatchAll();
            _harmony = null;

            PluginConfig = null;
            Random = null;
        }
    }
}
