using HarmonyLib;
using PlayerRoles;
using PlayerRoles.RoleAssign;

namespace AntiStrangulation.Patches
{
    [HarmonyPatch(typeof(ScpSpawner), nameof(ScpSpawner.AssignScp))]
    internal static class ScpSpawnerPatch
    {
        private static bool Prefix(RoleTypeId scp)
        {
            if (Plugin.PluginConfig.DisableAutoSpawn && scp == RoleTypeId.Scp3114)
                return false;

            return true;
        }
    }
}
