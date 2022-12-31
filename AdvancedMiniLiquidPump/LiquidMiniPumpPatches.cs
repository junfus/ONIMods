using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        [HarmonyPatch(nameof(LiquidMiniPumpConfig.DoPostConfigureComplete))]
        class LiquidMiniPumpConfigPatches
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
                            case DefaultConsumption:
                                instruction.operand = (float)LiquidMiniPumpOptions.Instance.Consumption;
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
