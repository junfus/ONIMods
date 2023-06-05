using PeterHan.PLib.Lighting;

namespace RotatableLight
{
    public static class CastLightImpl
    {
        public static void CustomShape(LightingArgs arg)
        {
            OctantBuilder octants = new OctantBuilder(arg.Brightness, arg.SourceCell)
            {
                SmoothLight = RotatableLightOptions.Instance.SmoothLight,
                Falloff = RotatableLightOptions.Instance.Falloff
            };

            int range = arg.Range;
            Rotatable rotation = arg.Source.GetComponent<Rotatable>();
            switch (RotatableLightOptions.Instance.Shape)
            {
                case Shape.Circle:
                    CircleGenerator(octants, range);
                    break;
                case Shape.Semicircle:
                    SemicircleGenerator(octants, range, rotation);
                    break;
                case Shape.Cone:
                default:
                    ConeGenerator(octants, range, rotation);
                    break;
            }
        }

        private static void ConeGenerator(OctantBuilder octants, int range, Rotatable rotation = null)
        {
            switch (rotation?.GetOrientation())
            {
                case Orientation.R90:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_SW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_NW);
                    break;

                case Orientation.R180:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NE);
                    break;

                case Orientation.R270:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_NE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_SE);
                    break;

                case Orientation.Neutral:
                default:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SW);
                    break;
            }
        }

        private static void SemicircleGenerator(OctantBuilder octants, int range, Rotatable rotation = null)
        {
            switch (rotation?.GetOrientation())
            {
                case Orientation.R90:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_SW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NW);
                    break;

                case Orientation.R180:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_NE);
                    break;

                case Orientation.R270:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_NE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SE);
                    break;

                case Orientation.Neutral:
                default:
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SE);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(range, DiscreteShadowCaster.Octant.W_SW);
                    break;
            }
        }

        private static void CircleGenerator(OctantBuilder octants, int range)
        {
            octants.AddOctant(range, DiscreteShadowCaster.Octant.W_SW);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.W_NW);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NW);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.N_NE);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.E_NE);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.E_SE);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SE);
            octants.AddOctant(range, DiscreteShadowCaster.Octant.S_SW);
        }
    }
}