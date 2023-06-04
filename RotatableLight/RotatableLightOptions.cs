using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace RotatableLight
{
    [JsonObject(MemberSerialization.OptIn)]
    [RestartRequired]
    public sealed class RotatableLightOptions : SingletonOptions<RotatableLightOptions>
    {
        [Option("SmoothLight", "Whether or not using smooth light.")]
        [JsonProperty]
        public bool SmoothLight { get; set; }

        [Option("Falloff", "Falloff parameter which affect the lux.")]
        [Limit(0.1f, 1f)]
        [JsonProperty]
        public float Falloff { get; set; }

        [Option("LightShape", "The shape of the light.")]
        [JsonProperty]
        public Shape Shape { get; set; }

        public RotatableLightOptions()
        {
            SmoothLight = false;
            Falloff = 0.5f;
            Shape = Shape.Cone;
        }
    }

    public enum Shape
    {
        Cone,
        Semicircle,
        Circle,
    }
}
