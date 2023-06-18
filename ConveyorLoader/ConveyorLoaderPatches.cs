using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using System.Collections.Generic;

namespace ConveyorLoader
{
    public sealed class ConveyorLoaderPatches : KMod.UserMod2
    {
        public static HashedString PORT_ID = new HashedString("ConveyorLoaderOutput");

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new PLocalization().Register();
        }

        [HarmonyPatch(typeof(SolidConduitInboxConfig))]
        class Patch_SolidConduitInboxConfig
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(SolidConduitInboxConfig.CreateBuildingDef))]
            static void Postfix_CreateBuildingDef(BuildingDef __result)
            {
                __result.LogicOutputPorts = new List<LogicPorts.Port>()
                {
                    LogicPorts.Port.OutputPort(
                        PORT_ID,
                        new CellOffset(0, 0),
                        STRINGS.BUILDINGS.PREFABS.CONVEYORLOADEROUTPUT.LOGIC_PORT,
                        STRINGS.BUILDINGS.PREFABS.CONVEYORLOADEROUTPUT.LOGIC_PORT_ACTIVE,
                        STRINGS.BUILDINGS.PREFABS.CONVEYORLOADEROUTPUT.LOGIC_PORT_INACTIVE)
                };
            }
        }

        [HarmonyPatch(typeof(SolidConduitInbox))]
        class Patch_SolidConduitInbox
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(SolidConduitInbox.Sim1000ms))]
            static void PostFix_Sim1000ms(SolidConduitInbox __instance, Storage ___storage)
            {
                __instance.GetComponent<LogicPorts>().SendSignal(PORT_ID, ___storage.IsEmpty() ? 1 : 0);
            }
        }
    }
}
