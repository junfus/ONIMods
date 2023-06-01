using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using STRINGS;
using UnityEngine;

namespace Freezer
{
    public class FreezerPatches : KMod.UserMod2
    {
        public const float ABSOLUTE_ZERO = -273.15f;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new POptions().RegisterOptions(this, typeof(FreezerOptions));
        }

        //[HarmonyPatch(typeof(GeneratedBuildings))]
        //class Patch_GeneratedBuildings
        //{
        //    [HarmonyPrefix]
        //    [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        //    static void Prefix_LoadGeneratedBuildings()
        //    {
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.NAME", UI.FormatAsLink(FreezerConfig.DISPLAY_NAME, FreezerConfig.ID));
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.DESC", FreezerConfig.DESCRIPTION);
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.EFFECT", FreezerConfig.EFFECT);
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.LOGIC_PORT", FreezerConfig.LOGIC_PORT);
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.LOGIC_PORT_ACTIVE", FreezerConfig.LOGIC_PORT_ACTIVE);
        //        Strings.Add($"STRINGS.BUILDINGS.PREFABS.{FreezerConfig.ID.ToUpperInvariant()}.LOGIC_PORT_INACTIVE", FreezerConfig.LOGIC_PORT_INACTIVE);

        //        ModUtil.AddBuildingToPlanScreen("Food", FreezerConfig.ID);
        //    }
        //}

        //[HarmonyPatch(typeof(Db))]
        //class Patch_Db
        //{
        //    [HarmonyPostfix]
        //    [HarmonyPatch(nameof(Db.Initialize))]
        //    static void Postfix_Initialize()
        //    {
        //        Db.Get().Techs.Get("FinerDining").unlockedItemIDs.Add(FreezerConfig.ID);
        //    }
        //}

        [HarmonyPatch(typeof(Rottable))]
        class Patch_Rottable
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(Rottable.AtmosphereQuality))]
            static void Postfix_AtmosphereQuality(ref Rottable.RotAtmosphereQuality __result) => __result = Rottable.RotAtmosphereQuality.Sterilizing;
        }

        //[HarmonyPatch(typeof(FreezerConfig))]
        //class Patch_FreezerConfig
        [HarmonyPatch(typeof(RefrigeratorConfig))]
        class Patch_RefrigeratorConfig
        {
            [HarmonyPostfix]
            //[HarmonyPatch(nameof(FreezerConfig.CreateBuildingDef))]
            [HarmonyPatch(nameof(RefrigeratorConfig.CreateBuildingDef))]
            static void Postfix_CreateBuildingDef(BuildingDef __result) => __result.EnergyConsumptionWhenActive = FreezerOptions.Instance.ActiveEnergy;

            [HarmonyPostfix]
            //[HarmonyPatch(nameof(FreezerConfig.DoPostConfigureComplete))]
            [HarmonyPatch(nameof(RefrigeratorConfig.DoPostConfigureComplete))]
            static void Postfix_DoPostConfigureComplete(GameObject go)
            {
                go.AddOrGet<Storage>().capacityKg = FreezerOptions.Instance.Capacity;
                go.AddOrGetDef<RefrigeratorController.Def>().powerSaverEnergyUsage = FreezerOptions.Instance.PowerSaver;
            }
        }

        [HarmonyPatch(typeof(RefrigeratorController))]
        class Patch_RefrigeratorController
        {
            [HarmonyPatch(typeof(RefrigeratorController.Def))]
            class Patch_Def
            {
                [HarmonyPostfix]
                [HarmonyPatch(MethodType.Constructor)]
                static void Postfix_Ctor(ref float ___simulatedInternalTemperature, ref float ___activeCoolingStopBuffer)
                {
                    ___simulatedInternalTemperature = FreezerOptions.Instance.LowestTemperature - ABSOLUTE_ZERO;
                }
            }
        }
    }
}
