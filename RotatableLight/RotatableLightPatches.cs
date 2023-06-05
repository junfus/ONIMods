using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Lighting;
using PeterHan.PLib.Options;
using UnityEngine;
using static LightGridManager;

namespace RotatableLight
{
    public sealed class RotatableLightPatches : KMod.UserMod2
    {
        public static ILightShape CustomShape;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new PLocalization().Register();
            new POptions().RegisterOptions(this, typeof(RotatableLightOptions));
            CustomShape = new PLightManager().Register("LightShape.CustomShape", new PLightManager.CastLightDelegate(CastLightImpl.CustomShape));
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        class Patch_GeneratedBuildings
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
            static void Prefix_LoadGeneratedBuildings()
            {
                ModUtil.AddBuildingToPlanScreen("Furniture", RotatableLightConfig.ID, "LightSource", CeilingLightConfig.ID);
            }
        }

        [HarmonyPatch(typeof(Db))]
        class Patch_Db
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(Db.Initialize))]
            static void Postfix_Initialize()
            {
                Db.Get().Techs.Get("FineArt").unlockedItemIDs.Add(RotatableLightConfig.ID);

                // Localization
                LocString.CreateLocStringKeys(typeof(STRINGS.ROTATABLELIGHT));
            }
        }

        [HarmonyPatch(typeof(LightGridEmitter))]
        class Patch_LightGridEmitter
        {
            [HarmonyPrefix]
            [HarmonyPatch("ComputeLux")]
            static bool Prefix_ComputeLux(int cell, LightGridEmitter.State ___state, ref int __result)
            {
                if (!RotatableLightOptions.Instance.OverrideGameLightSetting)
                {
                    return true;
                }

                if (RotatableLightOptions.Instance.SmoothLight)
                {
                    __result = Mathf.RoundToInt(___state.intensity * PLightManager.GetSmoothFalloff(RotatableLightOptions.Instance.Falloff, cell, ___state.origin));
                }
                else
                {
                    __result = Mathf.RoundToInt(___state.intensity * PLightManager.GetDefaultFalloff(RotatableLightOptions.Instance.Falloff, cell, ___state.origin));
                }

                return false;
            }
        }
    }
}
