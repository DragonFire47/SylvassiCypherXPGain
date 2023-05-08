using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace SylvassiCypherXPGain
{
    internal class Command : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "CypherXPGain", "cxpg" };
        }

        public override string Description()
        {
            return "Controls subcommands. Subcommands: toggle, multiplier";
        }

        public override string[][] Arguments()
        {
            return new string[][] { new string[] { "toggle", "multiplier" } };
        }

        public override void Execute(string arguments)
        {
            string[] args = arguments.ToLower().Split(' ');
            if (args[0] == "toggle")
            {
                Mod.Enabled.Value = !Mod.Enabled.Value;
                Messaging.Notification($"Mod {(Mod.Enabled.Value ? "Enabled" : "Disabled")}");
            }
            else if (args[0] == "multiplier")
            {
                if (args.Length > 1 && float.TryParse(args[1], out float multiplier))
                {
                    Mod.Multiplier.Value = multiplier;
                }
                else
                {
                    Messaging.Notification("Failed to parse multiplier argument. Use a number, decimals are allowed. ex: /cxpg multiplier 0.5");
                }
            }
            else
            {
                Messaging.Notification("No valid subcommand recieved. Subcommands: toggle, multiplier");
            }
        }
    }
}
