using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AdvancedSolarPanel
{
    public sealed class AdvancedSolarPanelLoad : KMod.UserMod2
    {
        public const float DefaultPower = 380f;
        public const float DefaultRate = 0.00053f;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(SolarPanelOptions));
        }

        [HarmonyPatch(typeof(SolarPanelConfig))]
        [HarmonyPatch(nameof(SolarPanelConfig.CreateBuildingDef))]
        class SolarPanelConfigPatches
        {
            static void Postfix(BuildingDef __result)
            {
                __result.GeneratorWattageRating = (float)SolarPanelOptions.Instance.Watts;
                __result.GeneratorBaseCapacity = (float)SolarPanelOptions.Instance.Watts;
            }
        }

        [HarmonyPatch(typeof(SolarPanel))]
        [HarmonyPatch(nameof(SolarPanel.EnergySim200ms))]
        class SolarPanelPatches
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ldc_R4)
                    {
                        float val = (float)instruction.operand;
                        switch (val)
                        {
                            case DefaultPower:
                                instruction.operand = (float)SolarPanelOptions.Instance.Watts;
                                break;
                            case DefaultRate:
                                instruction.operand = (float)(DefaultRate * SolarPanelOptions.Instance.Efficiency);
                                break;
                            default:
                                break;
                        }
                    }

                    yield return instruction;
                }
            }
        }
    }
}
