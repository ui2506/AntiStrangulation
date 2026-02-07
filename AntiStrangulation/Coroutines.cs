using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles.PlayableScps.Scp3114;
using System.Collections.Generic;

namespace AntiStrangulation
{
    internal static class Coroutines
    {
        internal static IEnumerator<float> StopStrangle(float time, Player scp, Player target)
        {
            yield return Timing.WaitForSeconds(time);

            if (target == null || scp == null)
                yield break;

            if (scp.RoleBase is Scp3114Role scpRole && scpRole.SubroutineModule.TryGetSubroutine<Scp3114Slap>(out var subroutine))
            {
                ReferenceHub targetHub = subroutine._strangle.SyncTarget?.Target;

                if (targetHub != null && targetHub == target.ReferenceHub)
                {
                    subroutine._strangle.OnPlayerRemoved(targetHub);
                }
            }

            yield break;
        }
    }
}
