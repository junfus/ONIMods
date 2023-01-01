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

        [Option("JoulesLost (Percentage)", "Percentage of joules lost. Default is 5%")]
        [Limit(0, 10)]
        [JsonProperty]
        public int JoulesLostPercentage { get; set; }

        [Option("MoreMass", "Default is false")]
        [JsonProperty]
        public bool MoreMass { get; set; }

        public BatteryMediumOptions()
        {
            Capacity = 40;
            JoulesLostPercentage = 5;
            MoreMass = false;
        }
    }
}
