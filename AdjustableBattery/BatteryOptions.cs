using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdjustableBattery
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class BatteryOptions : SingletonOptions<BatteryOptions>
    {
        [Option]
        [Limit(10, 400)]
        [JsonProperty]
        public int Capacity { get; set; }

        [Option]
        [Limit(0, 20)]
        [JsonProperty]
        public float JoulesLostPercentage { get; set; }

        [Option]
        [JsonProperty]
        public bool MoreMass { get; set; }

        [Option]
        [JsonProperty]
        public bool SelfHeat { get; set; }

        public BatteryOptions()
        {
            Capacity = 10;
            JoulesLostPercentage = 10f;
            MoreMass = false;
            SelfHeat = true;
        }
    }
}
