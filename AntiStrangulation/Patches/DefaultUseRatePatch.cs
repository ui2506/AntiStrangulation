using HarmonyLib;
using LabApi.Features.Console;
using PlayerRoles.FirstPersonControl;
using System;
using System.Reflection;

namespace AntiStrangulation.Patches
{
    [HarmonyPatch(typeof(FpcStateProcessor), MethodType.Constructor, new Type[] { typeof(ReferenceHub), typeof(FirstPersonMovementModule), typeof(float), typeof(float), typeof(UnityEngine.AnimationCurve) })]
    internal static class FpcStateProcessorCtorPatch
    {
        private static void Postfix(FpcStateProcessor __instance, ReferenceHub hub)
        {
            if (hub == null || hub.roleManager.CurrentRole == null)
                return;

            if (hub.roleManager.CurrentRole.RoleTypeId == PlayerRoles.RoleTypeId.Scp3114)
            {
                float newValue = Plugin.PluginConfig.StaminaBalance;

                FieldInfo field = typeof(FpcStateProcessor).GetField("_useRate", BindingFlags.Instance | BindingFlags.NonPublic);

                if (field != null)
                {
                    field.SetValue(__instance, newValue);
                }
                else
                {
                    Logger.Error("[FpcStateProcessorCtorPatch] Не найдено поле _useRate");
                }
            }
        }
    }
}
