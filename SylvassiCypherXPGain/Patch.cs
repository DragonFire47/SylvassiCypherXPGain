using HarmonyLib;
using PulsarModLoader.Patches;
using PulsarModLoader.Utilities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SylvassiCypherXPGain
{
    [HarmonyPatch(typeof(PLSylvassiCypher), "Update")]
    internal class Patch
    {
        static void PatchMethod(PLSylvassiCypher instance)
        {
            int TurnsLeft = 1 + (((int)AccessTools.Method(typeof(PLSylvassiCypher), "GetTurnsLeft").Invoke(instance, new object[0]) - (int)AccessTools.Method(typeof(PLSylvassiCypher), "GetTotalTurnsLeftFromOffsets").Invoke(instance, new object[0])) / 2);
            int XPGain = (int)(TurnsLeft * Mod.Multiplier);
            Messaging.Notification($"You have been granted {XPGain} XP for completing this cypher with {TurnsLeft} attempts remaining.", PhotonTargets.All, default, default, true);
            PLServer.Instance.CurrentCrewXP += XPGain;
        }


        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> TargetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Stfld, AccessTools.Field(typeof(PLSylvassiCypher), "CurrentState")),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call)
            };
            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch), "PatchMethod")),
            };
            return HarmonyHelpers.PatchBySequence(instructions, TargetSequence, InjectedSequence, HarmonyHelpers.PatchMode.AFTER, HarmonyHelpers.CheckMode.NONNULL, true);
        }
    }
}
