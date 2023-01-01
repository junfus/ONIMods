using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AdvancedMiniLiquidPump
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class LiquidMiniPumpOptions : SingletonOptions<LiquidMiniPumpOptions>
    {
        [Option("Consumption", "Consumption. Default is 1.")]
        [Limit(1, 10)]
        [JsonProperty]
        public int Consumption { get; set; }

        public LiquidMiniPumpOptions()
        {
            Consumption = 1;
        }
    }
}
