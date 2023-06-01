using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Lighting;
using PeterHan.PLib.PatchManager;
using STRINGS;

namespace RotatableLight
{
    public sealed class RotatableLightPatches : KMod.UserMod2
    {
        public static ILightShape Semicircle;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);

            new PPatchManager(harmony).RegisterPatchClass(typeof(RotatableLightPatches));
            PLightManager plightManager = new PLightManager();
            RotatableLightPatches.Semicircle = plightManager.Register("LightShape.Semicircle", new PLightManager.CastLightDelegate(LightDefs.Semicircle));
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        class Patch_GeneratedBuildings
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
            static void Prefix_LoadGeneratedBuildings()
            {
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RotatableLightConfig.ID.ToUpperInvariant()}.NAME", UI.FormatAsLink(RotatableLightConfig.DISPLAYNAME, RotatableLightConfig.ID));
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RotatableLightConfig.ID.ToUpperInvariant()}.DESC", RotatableLightConfig.DESCRIPTION);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RotatableLightConfig.ID.ToUpperInvariant()}.EFFECT", RotatableLightConfig.EFFECT);

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
            }
        }
    }
}
