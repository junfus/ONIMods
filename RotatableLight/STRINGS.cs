﻿namespace RotatableLight
{
    public class STRINGS
    {
        public class BUILDINGS
        {
            public class PREFABS
            {
                public class ROTATABLELIGHT
                {
                    public static LocString NAME = global::STRINGS.UI.FormatAsLink("Rotatable Light", RotatableLightConfig.ID);
                    public static LocString DESC = string.Concat(
                        "Light reduces Duplicant stress and is required to grow certain plants.",
                        "\n\nThe light is rotatable, and the light shape is configurable in mod setting.\n");
                    public static LocString EFFECT = string.Concat("Provides ",
                        global::STRINGS.UI.FormatAsLink("Light", "LIGHT"),
                        " when ",
                        global::STRINGS.UI.FormatAsLink("Powered", "POWER"),
                        ".\n\nIncreases Duplicant workspeed within light radius.",
                        "\n\nThe light is rotatable, and the shape is configurable in mod setting.");
                }
            }
        }

        public class ROTATABLELIGHT
        {
            public class OPTIONS
            {
                public class ROTATABLELIGHTOPTIONS
                {
                    public static LocString CATEGORY = "Rotatable Light Options";
                }

                public class SHAPE : ROTATABLELIGHTOPTIONS
                {
                    public static LocString NAME = "Light Shape";
                    public static LocString TOOLTIP = "";
                }

                public class LIGHTOPTIONS
                {
                    public static LocString CATEGORY = "Light Options";
                }

                public class SMOOTHLIGHT : LIGHTOPTIONS
                {
                    public static LocString NAME = "Smooth Light";
                    public static LocString TOOLTIP = "";
                }

                public class FALLOFF : LIGHTOPTIONS
                {
                    public static LocString NAME = "Falloff";
                    public static LocString TOOLTIP = "";
                }

                public class OVERRIDEGAMELIGHTSETTING
                {
                    public static LocString NAME = "Override Game Light Setting";
                    public static LocString TOOLTIP = "Override <b>Light Options</b> to default game light setting.";
                }

                public class CONE
                {
                    public static LocString NAME = "Cone";
                    public static LocString TOOLTIP = "";
                }

                public class CIRCLE
                {
                    public static LocString NAME = "Circle";
                    public static LocString TOOLTIP = "";
                }

                public class SEMICIRCLE
                {
                    public static LocString NAME = "Semicircle";
                    public static LocString TOOLTIP = "";
                }
            }
        }
    }
}
