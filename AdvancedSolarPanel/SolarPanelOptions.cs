using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdvancedSolarPanel
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class SolarPanelOptions : SingletonOptions<SolarPanelOptions>
    {
        [Option]
        [Limit(380, 4000)]
        [JsonProperty]
        public int Watts { get; set; }

        [Option]
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
