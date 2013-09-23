using System;
using System.Threading.Tasks;
using Communication;
using Data.Enums.Gather;
using Data.Interfaces;
using Data.Structures.Gather;
using Data.Structures.Player;
using Network;
using Network.Server;
using Tera.AiEngine;
using Utils;

namespace Tera.Controllers
{
    class GatherableController : IController
    {
        protected Player Player;
        protected Gather Gather;
        protected bool IsActive = true;

        protected const byte StatUpChance = 30;

        /*from CollectionCommonData in DC*/
        protected const byte HardProbOver = 0;
        protected const byte HardProbUnder = 50;
        protected const byte NormalProbOver = 50;
        protected const byte NormalProbUnder = 75;
        protected const byte EasyProbOver = 75;
        protected const byte EasyProbUnder = 101;

        public GatherableController(Player player, Gather gather)
        {
            Gather = gather;
            Player = player;
        }

        public void Start(Player player)
        {
            if(!CanPlayerUseGather())
                return;

            StartGathering();
        }

        protected async void StartGathering()
        {
            Global.VisibleService.Send(Player, new SpGatherStart(Player, Gather));

            byte state = 0;
            Gather.IsInProcess = true;

            while (state < 100 && IsActive)
            {
                state += (byte)new Random().Next(0, 100 - Gather.GatherTemplate().Grade * 10);

                if (state > 100)
                    state = 100;

                new SpGatherProgress(state).Send(Player.Connection);

                await Task.Delay(state < 100 ? new Random().Next(2000, 4000) : 1000);
            }

            if (IsActive)
                CompleteGather();
        }

        protected void CompleteGather()
        {
            Gather.IsInProcess = false;
            byte chance = 0;
            bool isQuest = Gather.GatherTemplate().TypeName == TypeName.Quest;

            if (!isQuest)
            {
                switch (Gather.GatherTemplate().Grade)
                {
                    case 1:
                        chance = Player.PlayerCraftStats.GetGatherStat(Gather.GatherTemplate().TypeName) -
                                 Gather.GatherTemplate().NeededProficiency > 50
                                     ? EasyProbUnder
                                     : EasyProbOver;
                        break;
                    case 2:
                        chance = Player.PlayerCraftStats.GetGatherStat(Gather.GatherTemplate().TypeName) -
                                 Gather.GatherTemplate().NeededProficiency > 50
                                     ? NormalProbUnder
                                     : NormalProbOver;
                        break;
                    case 3:
                        chance = Player.PlayerCraftStats.GetGatherStat(Gather.GatherTemplate().TypeName) -
                                 Gather.GatherTemplate().NeededProficiency > 50
                                     ? HardProbUnder
                                     : HardProbOver;
                        break;

                }
            }
            else
                chance = 100;

            if(Funcs.IsLuck(chance) && IsActive)
            {
                Global.VisibleService.Send(Player, new SpGatherEnd(Player, Gather, GatherEndCode.Normal));

                if (!isQuest)
                {
                    if (Funcs.IsLuck(StatUpChance))
                        Global.CraftLearnService.ProcessGatherStat(Player, Gather.GatherTemplate().TypeName);

                    int itemid = 1001;

                    //todo gather datas and buffs

                    #region CollectItemIdSwitch

                    if (Gather.Id >= 7 && Gather.Id <= 12)
                        itemid = 1005;
                    else if (Gather.Id >= 13 && Gather.Id <= 21)
                        itemid = 1009;
                    else if (Gather.Id >= 22 && Gather.Id <= 30)
                        itemid = 1013;
                    else if (Gather.Id >= 31 && Gather.Id <= 48)
                        itemid = 1017;
                    else if (Gather.Id >= 101 && Gather.Id <= 106)
                        itemid = 1002;
                    else if (Gather.Id >= 107 && Gather.Id <= 112)
                        itemid = 1006;
                    else if (Gather.Id >= 113 && Gather.Id <= 121)
                        itemid = 1010;
                    else if (Gather.Id >= 122 && Gather.Id <= 130)
                        itemid = 1014;
                    else if (Gather.Id >= 131 && Gather.Id <= 148)
                        itemid = 1018;
                    else if (Gather.Id >= 301 && Gather.Id <= 306)
                        itemid = 1003;
                    else if (Gather.Id >= 307 && Gather.Id <= 312)
                        itemid = 1007;
                    else if (Gather.Id >= 313 && Gather.Id <= 321)
                        itemid = 1011;
                    else if (Gather.Id >= 322 && Gather.Id <= 330)
                        itemid = 1015;
                    else if (Gather.Id >= 331 && Gather.Id <= 348)
                        itemid = 1019;

                    #endregion

                    Global.StorageService.AddItem(Player, Player.Inventory, itemid, 1);
                }
                else
                {
                    //TODO: QuestEngine implementation
                }

                ((GatherAi)Gather.Ai).ProcessGather();

            }
            else
                Global.VisibleService.Send(Player, new SpGatherEnd(Player, Gather, GatherEndCode.Failed));
        }

        public void Release()
        {
            IsActive = false;
        }

        public void Action()
        {
            if (!IsActive)
            {
                Gather.IsInProcess = false;
                CompleteGather();
                Global.ControllerService.SetController(Player, new DefaultController());
            }
        }

        private bool CanPlayerUseGather(bool sendMsg = true)
        {
            if(Gather.IsInProcess)
            {
                if (sendMsg)
                    SystemMessages.AnotherPlayerIsAlreadyGatheringThat.Send(Player.Connection);
                return false;
            }

            //if(Player.GatherStats.GetValueByType(Gather.GatherTemplate().TypeName) < Gather.GatherTemplate().NeededProficiency)
            //{
            //    if (sendMsg)
            //        SystemMessages.AnotherPlayerIsAlreadyGatheringThat.Send(Player.Connection);
            //    return false;
            //}

            return true;
        }
    }
}
