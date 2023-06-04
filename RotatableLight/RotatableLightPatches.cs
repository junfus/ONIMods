using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Lighting;
using PeterHan.PLib.Options;

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
            }
        }
    }
}
