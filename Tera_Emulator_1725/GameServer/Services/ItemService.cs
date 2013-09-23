using Communication;
using Communication.Interfaces;
using Data.Enums.Item;
using Data.Structures;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.Template.Item.CategorieStats;
using Data.Structures.World;
using Network;
using Network.Server;
using Utils;

namespace Tera.Services
{
    internal class ItemService : IItemService
    {
        public void ItemUse(Player player, int itemId, WorldPosition position)
        {
            StorageItem i = Global.StorageService.GetItemById(player.Inventory, itemId);

            if (i == null)
                return;

            if (!CanUseItem(player, i, null, true))
                return;

            if (i.ItemTemplate.Id == 98) //Campfire
            {
                if (MapService.TryPutCampfire(player, position))
                    Global.StorageService.RemoveItemById(player, player.Inventory, itemId, 1);

                return;
            }

            ItemTemplate template = i.ItemTemplate;

            SkillStat skillStat = template.CatigorieStat as SkillStat;
            if (skillStat == null && template.CombatItemType != CombatItemType.RECIPE)
                return;

            switch (template.CombatItemType)
            {
                case CombatItemType.SKILLBOOK:
                    Global.SkillsLearnService.UseSkillBook(player, template.Id);
                    break;
                case CombatItemType.IMMEDIATE:
                case CombatItemType.DISPOSAL:
                    {
                        Global.SkillEngine.UseSkill(
                            player.Connection,
                            new UseSkillArgs
                                {
                                    IsItemSkill = true,
                                    SkillId = skillStat.SkillId,
                                    StartPosition = position,
                                });

                        new SpItemCooldown(itemId, template.Cooltime).Send(player);
                    }
                    break;
                case CombatItemType.NO_COMBAT:
                    //new SpSystemNotice("Process use NO_COMBAT item").Send(player.Connection);
                    break;
                case CombatItemType.RECIPE:
                    Global.CraftService.AddRecipe(player, itemId);
                    SystemMessages.YouLearnedToMakeRecipe("@item:" + itemId);
                    //new SpSystemNotice("You learned new recipe").Send(player.Connection);
                    break;
            }

            int groupId = ItemTemplate.Factory(itemId).CoolTimeGroup;

            if (groupId != 0)
            {
                if (player.ItemCoodowns.ContainsKey(groupId))
                    player.ItemCoodowns[groupId] = Funcs.GetCurrentMilliseconds();
                else
                    player.ItemCoodowns.Add(groupId, Funcs.GetCurrentMilliseconds());
            }

            Global.StorageService.RemoveItemById(player, player.Inventory, itemId, 1);
        }

        public void GetItemInfo(Player player, long itemUid)
        {
            try
            {
                TeraObject itemObject = Uid.GetObject(itemUid) as TeraObject;

                if (itemObject == null || !(itemObject is StorageItem))
                    return;

                new SpItemInfo(((StorageItem) itemObject).ItemId,
                               itemUid,
                               "",
                               player.PlayerData.Name)
                    .Send(player);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
                //Nothing
            }
        }

        private bool CanUseItem(Player player, StorageItem item, Player secondPlayer = null, bool sendmessages = false)
        {
            int groupId = ItemTemplate.Factory(item.ItemId).CoolTimeGroup;

            if (item.ItemTemplate.RequiredUserStatus != null
                && !item.ItemTemplate.RequiredUserStatus.Contains(player.PlayerMode.ToString().ToUpper()))
                return false;

            if (player.PlayerLevel < item.ItemTemplate.Level)
            {
                if (sendmessages)
                    SystemMessages.YouMustBeAHigherLevelToUseThat.Send(player.Connection);

                return false;
            }

            if (item.ItemTemplate.RequiredSecondCharacter && secondPlayer == null)
                return false;

            if (groupId != 0 && player.ItemCoodowns.ContainsKey(groupId) && (Funcs.GetCurrentMilliseconds() - player.ItemCoodowns[groupId]) / 1000 < item.ItemTemplate.Cooltime)
            {
                //todo System message
                return false;
            }
            if(item.ItemTemplate.CombatItemType == CombatItemType.RECIPE && player.Recipes.Contains(item.ItemId))
            {
                if (!Data.Data.Recipes.ContainsKey(item.ItemId))
                {
                    Log.Warn("ItemService: Can't find recipe {0}", item.ItemId);
                    return false;
                }

                if (player.Recipes.Contains(item.ItemId))
                {
                    //todo System message
                    return false;
                }
                if(player.PlayerCraftStats.GetCraftSkills(Data.Data.Recipes[item.ItemId].CraftStat) < Data.Data.Recipes[item.ItemId].ReqMin)
                {
                    //todo System message
                    return false;
                }
            }

            return true;
        }

        public void Action()
        {
            
        }
    }
}
