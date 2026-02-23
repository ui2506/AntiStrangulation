using HarmonyLib;
using PlayerRoles.PlayableScps.Scp3114;

namespace AntiStrangulation.Patches
{
    [HarmonyPatch(typeof(Scp3114Role), nameof(Scp3114Role.EnableSpawning), MethodType.Getter)]
    internal static class SpawnScpsPatch
    {
        private static bool Prefix(ref bool __result)
        {
            __result = !Plugin.PluginConfig.DisableAutoSpawn;
            return false;
        }
    }
}
