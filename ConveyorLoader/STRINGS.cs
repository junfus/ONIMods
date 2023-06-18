namespace ConveyorLoader
{
    public class STRINGS
    {
        public class BUILDINGS
        {
            public class PREFABS
            {
                public class CONVEYORLOADEROUTPUT
                {
                    public static LocString LOGIC_PORT = "Empty/Not Empty";
                    public static LocString LOGIC_PORT_ACTIVE = string.Concat(
                        "Sends a ",
                        global::STRINGS.UI.FormatAsAutomationState("Green Signal", global::STRINGS.UI.AutomationState.Active),
                        " when empty");
                    public static LocString LOGIC_PORT_INACTIVE = string.Concat(
                        "Otherwise, sends a ",
                        global::STRINGS.UI.FormatAsAutomationState("Red Signal", global::STRINGS.UI.AutomationState.Standby));
                }
            }
        }
    }
}
