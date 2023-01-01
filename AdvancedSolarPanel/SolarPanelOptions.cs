using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdvancedSolarPanel
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class SolarPanelOptions : SingletonOptions<SolarPanelOptions>
    {
        [Option("Wattage", "Maximum wattage that Solar Panel can provide.")]
        [Limit(380, 4000)]
        [JsonProperty]
        public int Watts { get; set; }

        [Option("Efficiency", "Default is 1.")]
        [Limit(1, 10)]
        [JsonProperty]
        public int Efficiency { get; set; }

        public SolarPanelOptions()
        {
            Watts = 380;
            Efficiency = 1;
        }
    }
}
