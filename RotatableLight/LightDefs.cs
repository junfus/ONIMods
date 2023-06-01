﻿using PeterHan.PLib.Lighting;

namespace RotatableLight
{
    public static class LightDefs
    {
        public static void Semicircle(LightingArgs arg)
        {
            var octants = new OctantBuilder(arg.Brightness, arg.SourceCell);
            var rotation = arg.Source.GetComponent<Rotatable>();
            switch (rotation?.GetOrientation())
            {
                case Orientation.R90:
                    // Cone to the right
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
                    break;
                case Orientation.R180:
                    // Cone down
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
                    break;
                case Orientation.R270:
                    // Cone to the left
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.N_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_NE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
                    break;
                case Orientation.Neutral:
                default:
                    // Cone up
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.E_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SE);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.S_SW);
                    octants.AddOctant(arg.Range, DiscreteShadowCaster.Octant.W_SW);
                    break;
            }
        }
    }
}