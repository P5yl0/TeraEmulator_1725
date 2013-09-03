using System;
using System.Collections.Generic;
using Communication;
using Data.Enums;
using Data.Enums.Craft;
using Data.Enums.Item;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.World;
using Data.Structures.World.Requests;
using Network.Server;
using Tera.Extensions;

namespace Tera.Controllers
{
    internal class DialogController : DefaultController
    {
        public static List<PlayerClass> MagicClasses = new List<PlayerClass>
            {
                PlayerClass.Mystic,
                PlayerClass.Priest,
                PlayerClass.Sorcerer
            };

        public static List<PlayerClass> TacticClasses = new List<PlayerClass>
            {
                PlayerClass.Archer,
                PlayerClass.Warrior,
                PlayerClass.Berserker,
                PlayerClass.Lancer,
                PlayerClass.Slayer
            };

        public Npc Npc;
        protected int Uid = (int) DateTime.Now.Ticks;
        public string JournalId;

        public int Stage = 1;
        public int Special1; //NpcType or 0
        public int Special2; //QuestStep or 0
        public int Page = 1;
        public int DialogId; //NpcId or QuestId

        public List<DialogButton> Buttons;

        public void SendDialog(Quest quest = null, QuestReward reward = null)
        {
            new SpShowDialog(Npc, Stage, Buttons, DialogId, Special1, Special2, Page, Uid, quest, reward).Send(Player.Connection);

            Global.VisibleService.Send(Player, new SpCharacterEmotions(Player, PlayerEmotion.Dialog));
            Global.VisibleService.Send(Npc, new SpNpcEmotion(Npc, Player, new Random().Next(1, 3)));
        }

        public void Reset(int stage, int npcType, int questStep, int page, int dialogId)
        {
            Stage = stage;
            Special1 = npcType;
            Special2 = questStep;
            Page = page;
            DialogId = dialogId;

            Buttons = new List<DialogButton>();
        }

        public DialogController(Player player, Npc npc)
        {
            Npc = npc;
            Player = player;

            Special1 = npc.NpcTemplate.HuntingZoneId;
            DialogId = npc.NpcTemplate.Id;
            Buttons = new List<DialogButton>();

            Global.QuestEngine.AddButtonsToDialog(player, npc, Buttons);

            if (Npc.SpawnTemplate.FullId == 6301151)
            {
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.ArmorsmithDesign));
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.WeaponsmithMaterials));
            }
            else if (Npc.SpawnTemplate.FullId == 6301152)
            {
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.TailoringDesign));
            }
            else if (Npc.SpawnTemplate.FullId == 6301153)
            {
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.AlchemyMaterials));
            }
            else if(Data.Data.StaticTeleports.ContainsKey(Npc.SpawnTemplate.FullId))
            {
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.Teleport));
            }
            else
            {
                switch (Npc.NpcTemplate.Title)
                {
                    case NpcTitle.Guard:
                        //case NpcTitle.Monster:
                        break;

                        /*case NpcTitle.Dummy:
                    new SpShowDialog(Npc, Npc.NpcTemplate.Id, Npc.NpcTemplate.Type).Send(Player.Connection);
                    return;*/

                    case NpcTitle.Merchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.Shop));
                        break;

                    case NpcTitle.MagicInstructor:
                        if (MagicClasses.Contains(Player.PlayerData.Class))
                            Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.LearnSkills));
                        break;

                    case NpcTitle.TacticsInstructor:
                        if (TacticClasses.Contains(Player.PlayerData.Class))
                            Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.LearnSkills));
                        break;

                        /*
            case NpcTitle.ArmorMerchant:
                Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.BuyArmor));
                break;
*/
                    case NpcTitle.CrystalMerchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.BuyCrystals));
                        break;

                    case NpcTitle.Banker:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.Bank));
                        break;

                    case NpcTitle.WeaponMerchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.BuyWeapons));
                        break;

                    case NpcTitle.FlightMaster:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.FlightPoints));
                        break;

                    case NpcTitle.GuildManager:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.CreateGuild));
                        break;

                    case NpcTitle.DyeMerchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.DyeShop));
                        break;

                    case NpcTitle.PetMerchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.ShopForPets));
                        break;

                    case NpcTitle.GuildEmblems:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.GuildEmblem));
                        break;

                    case NpcTitle.BadgeMerchant:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.RedeemTrialBadges));
                        // Badges O_________o
                        break;

                    case NpcTitle.TradeBroker:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.ViewBrokerage));
                        break;

                    case NpcTitle.WeaponsmithingMaterials:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.WeaponsmithMaterials));
                        break;

                    case NpcTitle.ArmorsmithingMaterials:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.ArmorsmithMaterials));
                        break;

                    case NpcTitle.LeatherworkingMaterials:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.LeatherworkMaterials));
                        break;

                    case NpcTitle.TailoringMaterials:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.TailoringMaterials));
                        break;

                    case NpcTitle.WeaponsmithingDesigns:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.WeaponsmithDesign));
                        break;

                    case NpcTitle.ArmorsmithingDesigns:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.ArmorsmithDesign));
                        break;

                    case NpcTitle.LeatherworkingPatterns:
                        Buttons.Add(new DialogButton(DialogIcon.CenteredGray, DialogNpcString.LeatherworkPatterns));
                        break;
                }
            }

            SendDialog();

            //new SpNpcTalk(npc, "@npc:" + npc.NpcTemplate).Send(player.Connection);
        }

        public void Progress(int selectedIndex, int dialogUid)
        {
            if (Uid != dialogUid)
                return; //hack prevent

            DialogButton button = Buttons[selectedIndex - 1];
            string cmd = button.Text.Substring(0, button.Text.IndexOf(':')); //JournalId
            int value = int.Parse(button.Text.Substring(button.Text.IndexOf(':') + 1));

            if (cmd.Equals("@quest"))
            {
                JournalId = button.Text;
                Global.QuestEngine.ProcessDialog(this, value/1000, value%1000);
                return;
            }

            if (cmd.Equals("@npc"))
            {
                if (Npc.SpawnTemplate.FullId == 6301151)
                {
                    Global.CraftService.InitCraft(Player,
                                            selectedIndex == 1
                                                ? CraftStat.Armorsmithing
                                                : CraftStat.Weaponsmithing);
                }
                else if (Npc.SpawnTemplate.FullId == 6301152)
                {
                    Global.CraftService.InitCraft(Player, CraftStat.Tailoring);
                }
                else if (Npc.SpawnTemplate.FullId == 6301153)
                {
                    Global.CraftService.InitCraft(Player, CraftStat.Alchemy);
                }
                else if (Data.Data.StaticTeleports.ContainsKey(Npc.SpawnTemplate.FullId))
                {
                    Global.TeleportService.ForceTeleport(Player, Data.Data.StaticTeleports[Npc.SpawnTemplate.FullId]);
                }
                else
                {
                    switch (Npc.NpcTemplate.Title)
                    {
                        case NpcTitle.Guard:
                            //case NpcTitle.Monster:
                            return;

                        case NpcTitle.MagicInstructor:
                        case NpcTitle.TacticsInstructor:
                            Global.FeedbackService.SendLearSkillsDialog(Player);
                            break;

                        case NpcTitle.Banker:
                            Global.StorageService.ShowPlayerStorage(Player, StorageType.CharacterWarehouse, false);
                            break;
                        case NpcTitle.FlightMaster:
                            new SpFlightPoints(Data.Data.FlyTeleports[Player.Position.MapId].Select(ft => ft.Id < 70)).Send(
                                Player.Connection);
                            new SpShowWindow(new EmptyRequest(Player, RequestType.TeleportWindow)).Send(
                                Player.Connection);
                            break;
                        case NpcTitle.GuildEmblems:
                            break;
                        case NpcTitle.GuildManager:
                            // Code goes here
                            new SpCanSendRequest(5).Send(Player.Connection);
                            new SpSystemWindow(SystemWindow.LegionWindow).Send(Player.Connection);
                            break;
                        case NpcTitle.TradeBroker:
                            break;


                        case NpcTitle.Merchant:
                            //case NpcTitle.ArmorMerchant:
                        case NpcTitle.CrystalMerchant:
                        case NpcTitle.WeaponMerchant:
                        case NpcTitle.DyeMerchant:
                        case NpcTitle.PetMerchant:
                        case NpcTitle.BadgeMerchant:
                        case NpcTitle.WeaponsmithingMaterials:
                        case NpcTitle.ArmorsmithingMaterials:
                        case NpcTitle.LeatherworkingMaterials:
                        case NpcTitle.TailoringMaterials:
                        case NpcTitle.WeaponsmithingDesigns:
                        case NpcTitle.ArmorsmithingDesigns:
                        case NpcTitle.LeatherworkingPatterns:
                            if (Data.Data.Tradelists.ContainsKey(Npc.NpcTemplate.FullId))
                                Global.ControllerService.SetController(Player,
                                                                       new TradeController(Player,
                                                                                           Data.Data.
                                                                                               Tradelists
                                                                                               [
                                                                                                   Npc.
                                                                                                       NpcTemplate
                                                                                                       .FullId
                                                                                               ]));
                            break;


                    }
                }
            }
        }
    }
}
