using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Enums;
using Data.Enums.Craft;
using Data.Interfaces;
using Data.Structures.Craft;
using Data.Structures.Player;
using Network;
using Network.Server;
using Utils;

namespace Tera.Controllers
{
    internal class CraftController : IController
    {
        protected Recipe Recipe;
        protected Player Player;
        protected bool IsActive = true;
        protected const byte LuckChance = 75;
        protected const byte ProgressStatChance = 25;

        public CraftController(Player player, Recipe recipe)
        {
            Player = player;
            Recipe = recipe;
        }


        protected async void Progress()
        {
            if (
                Recipe.NeededItems.Any(
                    neededItem =>
                    !Communication.Global.StorageService.ContainsItem(Player.Inventory, neededItem.Key, neededItem.Value)))
            //hack prevent
            {
                SystemMessages.YouDontHaveEnoughCraftingMaterials.Send(Player.Connection);
                return;
            }

            foreach (KeyValuePair<int, int> needItem in Recipe.NeededItems)
                Communication.Global.StorageService.RemoveItemById(Player, Player.Inventory, needItem.Key, needItem.Value);

            PlayerEmotion playerEmotion;

            switch (Recipe.CraftStat)
            {
                case CraftStat.Alchemy:
                    playerEmotion = PlayerEmotion.CraftAlchemy;
                    break;
                case CraftStat.Armorsmithing:
                case CraftStat.Weaponsmithing:
                    playerEmotion = PlayerEmotion.CraftWeaponsmithing;
                    break;
                case CraftStat.Tailoring:
                case CraftStat.Leatherworking:
                    playerEmotion = PlayerEmotion.CraftTailoring;
                    break;
                default:
                    playerEmotion = PlayerEmotion.None;
                    break;
            }

            new SpCraftInitBar().Send(Player.Connection);

            new SpCharacterEmotions(Player, playerEmotion).Send(Player.Connection);

            byte state = 0;

            while (state < 100 && IsActive)
            {
                state += (byte)new Random().Next(0, 100 - Recipe.Level * 10);

                new SpCraftProgress(state, 1, 0, 0, 0).Send(Player.Connection);

                await Task.Delay(state < 100 ? new Random().Next(2000, 4000) : 1000);
            }

            CompleteCraft();
        }

        public void CompleteCraft()
        {
            if (Funcs.IsLuck(LuckChance) && IsActive) //todo Craft chance
            {
                if (Funcs.IsLuck(Recipe.CriticalChancePercent) && Recipe.CriticalChancePercent != 0)
                {
                    SystemMessages.CraftedItem("@item:" + Recipe.CriticalResultItem.Key).Send(Player.Connection);
                    Communication.Global.StorageService.AddItem(Player, Player.Inventory, Recipe.CriticalResultItem.Key,
                                                                          Recipe.CriticalResultItem.Value);
                }
                else
                {
                    SystemMessages.CraftedItem("@item:" + Recipe.ResultItem.Key).Send(Player.Connection);
                    Communication.Global.StorageService.AddItem(Player, Player.Inventory, Recipe.ResultItem.Key,
                                                                          Recipe.ResultItem.Value);
                }

                if (Funcs.IsLuck(ProgressStatChance))
                    Communication.Global.CraftLearnService.ProcessCraftStat(Player, Recipe.CraftStat);

                new SpCraftProgress(100).Send(Player.Connection);

                new SpCharacterEmotions(Player, PlayerEmotion.CraftSuccess).Send(Player.Connection);
            }
            else
            {
                new SpCraftProgress(100).Send(Player.Connection);
                SystemMessages.FailedToCraftItem("@item:" + Recipe.ResultItem.Key).Send(Player.Connection);
            }
        }

        public void Start(Player player)
        {
            Progress();
        }

        public void Release()
        {
            IsActive = false;
        }

        public void Action()
        {
        }
    }
}
