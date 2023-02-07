using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
//using System.Collections.Generic;
//using System.Reflection.Emit;
using UnityEngine;

namespace AdvancedMiniLiquidPump
{
    public sealed class AdvancedLiquidMiniPumpLoad : KMod.UserMod2
    {
        public const float DefaultConsumption = 1f;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(LiquidMiniPumpOptions));
        }

        [HarmonyPatch(typeof(LiquidMiniPumpConfig))]
        [HarmonyPatch(nameof(LiquidMiniPumpConfig.CreateBuildingDef))]
        class LiquidMiniPumpConfig_CreateBuildingDef_Patch
        {
            static void Postfix(BuildingDef __result) => __result.EnergyConsumptionWhenActive = (float)LiquidMiniPumpOptions.Instance.Watts;
        }

        [HarmonyPatch(typeof(LiquidMiniPumpConfig))]
        [HarmonyPatch(nameof(LiquidMiniPumpConfig.DoPostConfigureComplete))]
        class LiquidMiniPumpConfig_DoPostConfigureComplete_Patch
        {
            static void Postfix(GameObject go) => go.AddOrGet<ElementConsumer>().consumptionRate = (float)LiquidMiniPumpOptions.Instance.Consumption;

            //static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            //{
            //    foreach (var instruction in instructions)
            //    {
            //        if (instruction.opcode == OpCodes.Ldc_R4)
            //        {
            //            float val = (float)instruction.operand;
            //            switch (val)
            //            {
            //                case DefaultConsumption:
            //                    instruction.operand = (float)LiquidMiniPumpOptions.Instance.Consumption;
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
