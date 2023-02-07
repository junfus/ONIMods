using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
//using System.Collections.Generic;
//using System.Reflection.Emit;
using TUNING;
using UnityEngine;

namespace AdjustableBattery
{
    public sealed class AdjustableBatteryLoad : KMod.UserMod2
    {
        public const int SecondsPerCycle = 600;
        public const float DefaultCapacity = 40000f;
        //public const float DefaultJoulesLost = 3.33333325f;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(BatteryMediumOptions));
        }

        [HarmonyPatch(typeof(BatteryMediumConfig))]
        class Patch_BatteryMediumConfig
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(BatteryMediumConfig.CreateBuildingDef))]
            static void Postfix_CreateBuildingDef(BuildingDef __result)
            {
                if (BatteryMediumOptions.Instance.MoreMass)
                {
                    float capacity = (float)BatteryMediumOptions.Instance.Capacity * 1000;
                    __result.Mass = new float[1] { capacity / DefaultCapacity * BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0] };
                }

                if (!BatteryMediumOptions.Instance.SelfHeat)
                {
                    __result.ExhaustKilowattsWhenActive = 0f;
                    __result.SelfHeatKilowattsWhenActive = 0f;
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch(nameof(BatteryMediumConfig.DoPostConfigureComplete))]
            static void Postfix_DoPostConfigureComplete(GameObject go)
            {
                Battery battery = go.AddOrGet<Battery>();
                battery.capacity = (float)BatteryMediumOptions.Instance.Capacity * 1000;
                battery.joulesLostPerSecond = battery.capacity * (BatteryMediumOptions.Instance.JoulesLostPercentage / 100f) / SecondsPerCycle;
            }

            //[HarmonyTranspiler]
            //[HarmonyPatch(nameof(BatteryMediumConfig.DoPostConfigureComplete))]
            //static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            //{
            //    float capacity = (float)BatteryMediumOptions.Instance.Capacity * 1000;
            //    float actualLost = capacity * (BatteryMediumOptions.Instance.JoulesLostPercentage / 100f) / SecondsPerCycle;

            //    foreach (var instruction in instructions)
            //    {
            //        if (instruction.opcode == OpCodes.Ldc_R4)
            //        {
            //            float val = (float)instruction.operand;
            //            switch (val)
            //            {
            //                case DefaultCapacity:
            //                    instruction.operand = capacity;
            //                    break;
            //                case DefaultJoulesLost:
            //                    instruction.operand = actualLost;
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }

            //        yield return instruction;
            //    }
            //}
        }
    }
}
