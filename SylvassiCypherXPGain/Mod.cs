using PulsarModLoader;

namespace SylvassiCypherXPGain
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.0.1";

        public override string Author => "Dragon";

        public override string Name => "SylvassiCypherXPGain";

        public override string LongDescription => base.LongDescription;

        public override bool CanBeDisabled()
        {
            return true;
        }

        public static SaveValue<float> Multiplier = new SaveValue<float>("Multiplier", 1f);

        public static SaveValue<bool> Enabled = new SaveValue<bool>("Enabled", true);

        public override void Disable()
        {
            Enabled.Value = false;
        }

        public override void Enable()
        {
            Enabled.Value|= true;
        }

        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }

        public override bool IsEnabled()
        {
            return Enabled.Value;
        }
    }
}
