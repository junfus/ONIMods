//using STRINGS;
//using System.Collections.Generic;
//using TUNING;
//using UnityEngine;

//namespace Freezer
//{
//    public class FreezerConfig : IBuildingConfig
//    {
//        public const string ID = "Freezer";
//        public const string DISPLAY_NAME = "Freezer";
//        public const string DESCRIPTION = "Food spoilage can be slowed by ambient conditions as well as by refrigerators.";
//        public static readonly string EFFECT = ("Stores " + UI.FormatAsLink("Food", "FOOD") + " at an ideal " + UI.FormatAsLink("Temperature", "HEAT") + " to prevent spoilage.");
//        public const string LOGIC_PORT = "Full/Not Full";
//        public static readonly string LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";
//        public static readonly string LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
//        public const string ANIM = "fridge_kanim";

//        public override BuildingDef CreateBuildingDef()
//        {
//            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
//            string[] allMetals = MATERIALS.ALL_METALS;
//            EffectorValues tieR0 = NOISE_POLLUTION.NOISY.TIER0;
//            EffectorValues tieR1 = TUNING.BUILDINGS.DECOR.BONUS.TIER1;
//            EffectorValues noise = tieR0;
//            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 2, 3, ANIM, 30, 30f, tieR4, allMetals, 800f, BuildLocationRule.OnFloor, tieR1, noise);
//            buildingDef.RequiresPowerInput = true;
//            buildingDef.AddLogicPowerPort = false;
//            buildingDef.EnergyConsumptionWhenActive = 120f;
//            buildingDef.SelfHeatKilowattsWhenActive = 0.125f;
//            buildingDef.ExhaustKilowattsWhenActive = 0.0f;
//            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
//            {
//                LogicPorts.Port.OutputPort(
//                    FilteredStorage.FULL_PORT_ID,
//                    new CellOffset(0, 1),
//                    LOGIC_PORT,
//                    LOGIC_PORT_ACTIVE,
//                    LOGIC_PORT_INACTIVE)
//            };
//            buildingDef.Floodable = false;
//            buildingDef.ViewMode = OverlayModes.Power.ID;
//            buildingDef.AudioCategory = "Metal";
//            SoundEventVolumeCache.instance.AddVolume(ANIM, "Refrigerator_open", NOISE_POLLUTION.NOISY.TIER1);
//            SoundEventVolumeCache.instance.AddVolume(ANIM, "Refrigerator_close", NOISE_POLLUTION.NOISY.TIER1);
//            return buildingDef;
//        }

//        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag) => go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Refrigerator);

//        public override void DoPostConfigureComplete(GameObject go)
//        {
//            Storage storage = go.AddOrGet<Storage>();
//            storage.showInUI = true;
//            storage.showDescriptor = true;
//            storage.storageFilters = STORAGEFILTERS.FOOD;
//            storage.allowItemRemoval = true;
//            storage.capacityKg = 500f;
//            storage.storageFullMargin = TUNING.STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
//            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
//            storage.showCapacityStatusItem = true;
//            Prioritizable.AddRef(go);
//            go.AddOrGet<TreeFilterable>();
//            go.AddOrGet<FoodStorage>();
//            go.AddOrGet<Refrigerator>();
//            RefrigeratorController.Def def = go.AddOrGetDef<RefrigeratorController.Def>();
//            def.powerSaverEnergyUsage = 20f;
//            def.coolingHeatKW = 0.375f;
//            def.steadyHeatKW = 0.0f;
//            go.AddOrGet<UserNameable>();
//            go.AddOrGet<DropAllWorkable>();
//            go.AddOrGetDef<RocketUsageRestriction.Def>().restrictOperational = false;
//            go.AddOrGetDef<StorageController.Def>();
//        }
//    }
//}
