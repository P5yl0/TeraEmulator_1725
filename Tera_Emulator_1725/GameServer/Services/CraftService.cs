using System.Collections.Generic;
using System.Linq;
using Communication.Interfaces;
using Data.Enums;
using Data.Enums.Craft;
using Data.Structures.Craft;
using Data.Structures.Player;
using Network.Server;
using Tera.Controllers;
using Utils;

namespace Tera.Services
{
    class CraftService : ICraftService
    {
        public void InitCraft(Player player, CraftStat craftStat)
        {
            new SpSystemWindow(SystemWindow.Hide).Send(player.Connection);
            new SpCraftWindow(craftStat).Send(player.Connection);

            UpdateCraftStats(player);
            UpdateCraftRecipes(player);
        }

        public void ProcessCraft(Player player, int recipeId)
        {
            if (!Data.Data.Recipes.ContainsKey(recipeId) || !player.Recipes.Contains(recipeId))
                return;

            Recipe r = Data.Data.Recipes[recipeId];

            if (!player.PlayerCraftStats.CraftSkillCollection.ContainsKey(r.CraftStat)) //hack prevent
                return;

            if (player.PlayerCraftStats.CraftSkillCollection[r.CraftStat] < r.ReqMin)
            {
                new SpCraftProgress(100).Send(player.Connection);
                return;
            }

            Communication.Global.ControllerService.SetController(player, new CraftController(player, r));
        }

        public void UpdateCraftRecipes(Player player)
        {
            new SpCharacterRecipes(player.Recipes.Select(recipe => Data.Data.Recipes[recipe]).ToList()).Send(player.Connection);
            new SpCharacterRecipes(null, true).Send(player.Connection);
        }

        public void AddRecipe(Player player, int recipeId)
        {
            if (!Data.Data.Recipes.ContainsKey(recipeId) || player.Recipes.Contains(recipeId))
                return;

            player.Recipes.Add(recipeId);
            UpdateCraftRecipes(player);
        }

        public void UpdateCraftStats(Player player)
        {
            new SpCharacterCraftStats(player).Send(player.Connection);
        }

        public void ProgressCraftStat(Player player, CraftStat craftStat)
        {
            if(craftStat.GetHashCode() > 6)
            {
                Log.Warn("CraftService: Try to progress wrong stat {0}", craftStat.ToString());
            }

            player.PlayerCraftStats.ProgressCraftStat(craftStat);

            List<int> toRemove = new List<int>(); 

            for (int i = 0; i < player.Recipes.Count; i++)
            {
               if(player.PlayerCraftStats.CraftSkillCollection[craftStat] > Data.Data.Recipes[player.Recipes[i]].ReqMax)
                   toRemove.Add(player.Recipes[i]);
            }

            foreach (int i in toRemove)
                player.Recipes.Remove(i);

            UpdateCraftRecipes(player);
        }

        public void Action()
        {
            
        }
    }
}
