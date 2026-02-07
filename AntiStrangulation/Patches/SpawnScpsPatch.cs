using HarmonyLib;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerRoles.PlayableScps;
using PlayerRoles.RoleAssign;
using System.Collections.Generic;
using System.Linq;

namespace AntiStrangulation.Patches
{
    [HarmonyPatch(typeof(ScpSpawner), nameof(ScpSpawner.SpawnScps))]
    internal static class SpawnScpsPatch
    {
        private static bool Prefix(int targetScpNumber)
        {
            if (Plugin.PluginConfig.DisableAutoSpawn || PlayerRoleLoader.AllRoles.Any(x => x.Key == RoleTypeId.Scp3114 && x.Value is ISpawnableScp))
                return true;

            ScpSpawner.EnqueuedScps.Clear();

            int scpRoleCount = PlayerRoleLoader.AllRoles.Count(x => x.Value is ISpawnableScp);

            if (Player.List.Count(x => !x.IsHost) / 5 > scpRoleCount)
                ScpSpawner.EnqueuedScps.Add(RoleTypeId.Scp3114);

            for (int i = 0; i < targetScpNumber; i++)
            {
                RoleTypeId nextRole = Plugin.Random.Next(0, ScpSpawner.MaxSpawnableScps) == 0 && !ScpSpawner.EnqueuedScps.Contains(RoleTypeId.Scp3114)
                    ? RoleTypeId.Scp3114
                    : ScpSpawner.NextScp;

                if (!ScpSpawner.EnqueuedScps.Contains(nextRole) && ScpSpawner.EnqueuedScps.Count < ScpSpawner.MaxSpawnableScps)
                    ScpSpawner.EnqueuedScps.Add(nextRole);
            }

            List<ReferenceHub> chosenPlayers = ScpPlayerPicker.ChoosePlayers(targetScpNumber);

            foreach (RoleTypeId role in ScpSpawner.EnqueuedScps.ToArray())
            {
                Logger.Debug(role.ToString(), Plugin.PluginConfig.Debug);

                ScpSpawner.EnqueuedScps.Remove(role);
                ScpSpawner.AssignScp(chosenPlayers, role, ScpSpawner.EnqueuedScps);
            }

            return false;
        }
    }
}
