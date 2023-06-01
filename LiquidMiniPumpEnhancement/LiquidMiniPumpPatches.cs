using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using UnityEngine;

namespace LiquidMiniPumpEnhancement
{
    public sealed class LiquidMiniPumpPatches : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new POptions().RegisterOptions(this, typeof(LiquidMiniPumpOptions));
        }

        [HarmonyPatch(typeof(LiquidMiniPumpConfig))]
        class Patch_LiquidMiniPumpConfig
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(LiquidMiniPumpConfig.CreateBuildingDef))]
            static void Postfix_CreateBuildingDef(BuildingDef __result) => __result.EnergyConsumptionWhenActive = (float)LiquidMiniPumpOptions.Instance.Watts;

            [HarmonyPostfix]
            [HarmonyPatch(nameof(LiquidMiniPumpConfig.DoPostConfigureComplete))]
            static void Postfix_DoPostConfigureComplete(GameObject go) => go.AddOrGet<ElementConsumer>().consumptionRate = (float)LiquidMiniPumpOptions.Instance.Consumption;
        }
    }
}
