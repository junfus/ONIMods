using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Lighting;

namespace RotatableLight
{
    public sealed class RotatableLightPatches : KMod.UserMod2
    {
        public static ILightShape Semicircle;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary(false);
            new PLocalization().Register();
            Semicircle = new PLightManager().Register("LightShape.Semicircle", new PLightManager.CastLightDelegate(CastLightImpl.Semicircle));
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
            }
        }
    }
}
