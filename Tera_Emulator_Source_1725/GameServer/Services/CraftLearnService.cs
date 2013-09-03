using Communication.Interfaces;
using Data.Enums.Craft;
using Data.Enums.Gather;
using Data.Structures.Player;
using Network.Server;

namespace Tera.Services
{
    class CraftLearnService : ICraftLearnService
    {
        public void ProcessCraftStat(Player player, CraftStat craftStat)
        {
            player.PlayerCraftStats.ProgressCraftStat(craftStat);
            new SpCharacterCraftStats(player).Send(player.Connection);
        }

        public void ProcessGatherStat(Player player, TypeName typeName)
        {
            player.PlayerCraftStats.ProgressGatherStat(typeName);
            new SpCharacterGatherstats(player.PlayerCraftStats).Send(player.Connection);
        }
    }
}
