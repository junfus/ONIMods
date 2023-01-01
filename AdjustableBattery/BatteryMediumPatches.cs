using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System.Collections.Generic;
using System.Reflection.Emit;
using TUNING;

namespace AdjustableBattery
{
    public sealed class AdjustableBatteryLoad : KMod.UserMod2
    {
        public const int SecondsPerCycle = 600;
        public const float DefaultCapacity = 40000f;
        public const float DefaultJoulesLost = 3.33333325f;
        public const string ID = "BatteryMedium";

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(BatteryMediumOptions));
        }

        [HarmonyPatch(typeof(BatteryMediumConfig))]
        [HarmonyPatch(nameof(BatteryMediumConfig.DoPostConfigureComplete))]
        class BatteryMediumConfigPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                float capacity = (float)BatteryMediumOptions.Instance.Capacity * 1000;
                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ldc_R4)
                    {
                        float val = (float)instruction.operand;
                        switch (val)
                        {
                            case DefaultCapacity:
                                instruction.operand = capacity;
                                break;
                            case DefaultJoulesLost:
                                float actualLost = capacity * (BatteryMediumOptions.Instance.JoulesLostPercentage / 100f) / SecondsPerCycle;
                                instruction.operand = actualLost;
                                break;
                            default:
                                break;
                        }
                    }

                    yield return instruction;
                }
            }
        }

        [HarmonyPatch(typeof(BatteryMediumConfig))]
        [HarmonyPatch(nameof(BatteryMediumConfig.CreateBuildingDef))]
        class BatteryMediumConfigCtorPatch
        {
            static bool Prefix(ref BuildingDef __result)
            {
                float capacity = (float)BatteryMediumOptions.Instance.Capacity * 1000;
                float fixedMass = BatteryMediumOptions.Instance.MoreMass ?
                    capacity / DefaultCapacity * BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0] :
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0];

                __result = BuildingTemplates.CreateBuildingDef(
                    "BatteryMedium",
                    2,
                    2,
                    "batterymed_kanim",
                    30,
                    60f,
                    new float[1] { fixedMass },
                    MATERIALS.ALL_METALS,
                    800f,
                    BuildLocationRule.OnFloor,
                    BUILDINGS.DECOR.PENALTY.TIER2,
                    NOISE_POLLUTION.NOISY.TIER1);

                __result.ExhaustKilowattsWhenActive = 0.25f;
                __result.SelfHeatKilowattsWhenActive = 1f;
                __result.Entombable = false;
                __result.ViewMode = OverlayModes.Power.ID;
                __result.AudioCategory = "Metal";
                __result.RequiresPowerOutput = true;
                __result.UseWhitePowerOutputConnectorColour = true;

                return false;
            }
        }
    }
}
