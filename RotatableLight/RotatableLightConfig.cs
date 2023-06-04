using TUNING;
using UnityEngine;

namespace RotatableLight
{
    internal class RotatableLightConfig : IBuildingConfig
    {
        public const string ID = "RotatableLight";

        public const int LUX = 1800;
        public const float RANGE = 8f;

        public override BuildingDef CreateBuildingDef()
        {
            LocString.CreateLocStringKeys(typeof(RotatableLightStrings.BUILDINGS));

            var width = 1;
            var height = 1;
            var hitpoints = 10;
            var construction_time = BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER1;
            float[] tieR1 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
            string[] allMetals = MATERIALS.ALL_METALS;
            var melting_point = 800f;
            var build_location_rule = BuildLocationRule.OnCeiling;
            var none = NOISE_POLLUTION.NONE;
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                ID,
                width,
                height,
                "ceilinglight_kanim",
                hitpoints,
                construction_time,
                tieR1,
                allMetals,
                melting_point,
                build_location_rule,
                BUILDINGS.DECOR.PENALTY.TIER5,
                none);
            buildingDef.RequiresPowerInput = true;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.EnergyConsumptionWhenActive = 15f;
            buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
            buildingDef.ViewMode = OverlayModes.Light.ID;
            buildingDef.AudioCategory = "Metal";
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
            lightShapePreview.lux = LUX;
            lightShapePreview.radius = RANGE;
            lightShapePreview.shape = RotatableLightPatches.CustomShape.KleiLightShape;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag) => go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource);

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LoopingSounds>();
            Light2D light2D = go.AddOrGet<Light2D>();
            light2D.overlayColour = LIGHT2D.CEILINGLIGHT_OVERLAYCOLOR;
            light2D.Color = LIGHT2D.CEILINGLIGHT_COLOR;
            light2D.Range = RANGE;
            light2D.Angle = 2.6f;
            light2D.Direction = LIGHT2D.CEILINGLIGHT_DIRECTION;
            light2D.Offset = LIGHT2D.CEILINGLIGHT_OFFSET;
            light2D.shape = RotatableLightPatches.CustomShape.KleiLightShape;
            light2D.drawOverlay = true;
            light2D.Lux = LUX;
            go.AddOrGetDef<LightController.Def>();
        }
    }
}
