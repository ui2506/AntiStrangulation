using HarmonyLib;
using PlayerRoles.PlayableScps.Scp3114;
using static PlayerRoles.PlayableScps.Scp3114.Scp3114Strangle;

namespace AntiStrangulation.Patches
{
    [HarmonyPatch(typeof(Scp3114Strangle), nameof(Scp3114Strangle.ProcessAttackRequest))]

    internal static class ProcessAttackRequestPatch
    {
        private static bool Prefix(ref StrangleTarget? __result)
        {
            if (!Plugin.PluginConfig.DisableStrangulation)
                return true;
            
            __result = null;
            return false;
        }
    }
}