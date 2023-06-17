using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Options;
using TUNING;
using UnityEngine;

namespace AdjustableBattery
{
    public sealed class BatteryPatches : KMod.UserMod2
    {
        public const int SecondsPerCycle = 600;
        public const float DefaultCapacity = 10000f;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new PLocalization().Register();
            new POptions().RegisterOptions(this, typeof(BatteryOptions));
        }

        [HarmonyPatch(typeof(Db))]
        class Patch_Db
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(Db.Initialize))]
            static void Postfix_Initialize()
            {
                // Localization
                LocString.CreateLocStringKeys(typeof(STRINGS.ADJUSTABLEBATTERY));
            }
        }

        [HarmonyPatch(typeof(BatteryConfig))]
        class Patch_BatteryConfig
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(BatteryConfig.CreateBuildingDef))]
            static void Postfix_CreateBuildingDef(BuildingDef __result)
            {
                if (BatteryOptions.Instance.MoreMass)
                {
                    float capacity = BatteryOptions.Instance.Capacity * 1000f;
                    __result.Mass = new float[1] { capacity / DefaultCapacity * BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] };
                }

                if (!BatteryOptions.Instance.SelfHeat)
                {
                    __result.ExhaustKilowattsWhenActive = 0f;
                    __result.SelfHeatKilowattsWhenActive = 0f;
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch(nameof(BatteryConfig.DoPostConfigureComplete))]
            static void Postfix_DoPostConfigureComplete(GameObject go)
            {
                Battery battery = go.AddOrGet<Battery>();
                battery.capacity = BatteryOptions.Instance.Capacity * 1000f;
                battery.joulesLostPerSecond = battery.capacity * (BatteryOptions.Instance.JoulesLostPercentage / 100) / SecondsPerCycle;
            }
        }
    }
}
