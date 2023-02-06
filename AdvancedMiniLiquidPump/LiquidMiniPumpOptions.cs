using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdvancedMiniLiquidPump
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class LiquidMiniPumpOptions : SingletonOptions<LiquidMiniPumpOptions>
    {
        [Option("Watts", "Watts. Default is 60w.")]
        [Limit(1, 120)]
        [JsonProperty]
        public int Watts { get; set; }

        [Option("Consumption", "Consumption. Default is 1kg/s.")]
        [Limit(1, 10)]
        [JsonProperty]
        public int Consumption { get; set; }

        public LiquidMiniPumpOptions()
        {
            Watts = 60;
            Consumption = 1;
        }
    }
}
