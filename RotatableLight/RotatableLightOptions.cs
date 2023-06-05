using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace RotatableLight
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class RotatableLightOptions : SingletonOptions<RotatableLightOptions>
    {
        [Option]
        [JsonProperty]
        public Shape Shape { get; set; }

        [Option]
        [JsonProperty]
        public bool SmoothLight { get; set; }

        [Option]
        [Limit(0.1f, 1f)]
        [JsonProperty]
        public float Falloff { get; set; }

        [Option]
        [JsonProperty]
        public bool OverrideGameLight { get; set; }

        public RotatableLightOptions()
        {
            // Localization
            LocString.CreateLocStringKeys(typeof(STRINGS.ROTATABLELIGHT));

            Shape = Shape.Cone;
            SmoothLight = false;
            Falloff = 0.5f;
            OverrideGameLight = false;
        }
    }

    public enum Shape
    {
        Cone,
        Semicircle,
        Circle,
    }
}
