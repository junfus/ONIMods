namespace RotatableLight
{
    public static class RotatableLightStrings
    {
        public static class BUILDINGS
        {
            public static class PREFABS
            {
                public static class ROTATABLELIGHT
                {
                    public static LocString NAME = STRINGS.UI.FormatAsLink("Rotatable Light", RotatableLightConfig.ID);
                    public static LocString DESC = string.Concat(
                        "Light reduces Duplicant stress and is required to grow certain plants.",
                        "\n\nThe light shape is semicircle and it is rotatable.");
                    public static LocString EFFECT = string.Concat("Provides ",
                        STRINGS.UI.FormatAsLink("Light", "LIGHT"),
                        " when ",
                        STRINGS.UI.FormatAsLink("Powered", "POWER"),
                        ".\n\nIncreases Duplicant workspeed within light radius.",
                        "\n\nThe light shape is semicircle and it is rotatable.");
                }
            }
        }
    }
}
