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

            switch (RotatableLightOptions.Instance.Shape)
            {
                case Shape.Circle:
                    CircleGenerator(arg, octants);
                    break;
                case Shape.Semicircle:
                    SemicircleGenerator(arg, octants);
                    break;
                case Shape.Cone:
                default:
                    ConeGenerator(arg, octants);
                    break;
            }
        }

        private static void ConeGenerator(LightingArgs arg, OctantBuilder octants)
        {
            Rotatable rotation = arg.Source.GetComponent<Rotatable>();
            switch (rotation?.GetOrientation())
            {
                case Orientation.R90:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
                    break;

                case Orientation.R180:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
                    break;

                case Orientation.R270:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
                    break;

                case Orientation.Neutral:
                default:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
                    break;
            }
        }

        private static void SemicircleGenerator(LightingArgs arg, OctantBuilder octants)
        {
            Rotatable rotation = arg.Source.GetComponent<Rotatable>();
            switch (rotation?.GetOrientation())
            {
                case Orientation.R90:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
                    break;

                case Orientation.R180:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
                    break;

                case Orientation.R270:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
                    break;

                case Orientation.Neutral:
                default:
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
                    break;
            }
        }

        private static void CircleGenerator(LightingArgs arg, OctantBuilder octants)
        {
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
            octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
        }
    }
}