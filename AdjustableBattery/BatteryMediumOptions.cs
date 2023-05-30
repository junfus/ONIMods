using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdjustableBattery
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class BatteryMediumOptions : SingletonOptions<BatteryMediumOptions>
    {
        [Option("Capacity (Kilojoules)", "Battery capacity. Default is 40kj.")]
        [Limit(40, 400)]
        [JsonProperty]
        public int Capacity { get; set; }

        [Option("JoulesLost (Thousandth)", "Thousandth of joules lost. Default is 50‰")]
        [Limit(0, 100)]
        [JsonProperty]
        public int JoulesLostPercentage { get; set; }

        [Option("MoreMass", "Default is false")]
        [JsonProperty]
        public bool MoreMass { get; set; }

        [Option("SelfHeat", "Default is true")]
        [JsonProperty]
        public bool SelfHeat { get; set; }

        public BatteryMediumOptions()
        {
            Capacity = 40;
            JoulesLostPercentage = 50;
            MoreMass = false;
            SelfHeat = true;
        }
    }
}
