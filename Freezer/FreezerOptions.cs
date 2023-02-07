using Newtonsoft.Json;
using PeterHan.PLib.Options;
namespace Freezer
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class FreezerOptions : SingletonOptions<FreezerOptions>
    {
        [Option("ActiveEnergy", "Energy consumption when active.")]
        [Limit(0, 200)]
        [JsonProperty]
        public int ActiveEnergy { get; set; }

        [Option("PowerSaver", "Energy consumption when power saver mode.")]
        [Limit(0, 100)]
        [JsonProperty]
        public int PowerSaver { get; set; }

        [Option("Capacity", "Capacity of Freezer(KG).")]
        [Limit(0, 2000)]
        [JsonProperty]
        public int Capacity { get; set; }

        [Option("Lowest temperature", "Lowest temeratur. Will also affect built-in refrigerator.")]
        [Limit(-50, 0)]
        [JsonProperty]
        public int LowestTemperature { get; set; }

        public FreezerOptions()
        {
            ActiveEnergy = 120;
            PowerSaver = 20;
            Capacity = 500;
            LowestTemperature = -20;
        }
    }
}
