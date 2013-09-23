using System.Collections.Generic;
using System.Globalization;
using Communication;
using Data.Enums;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.Quest.Tasks;
using Network;
using Network.Server;
using Tera.Controllers;
using Tera.Extensions;
using Tera.QuestEngine.Steps;

namespace Tera.QuestEngine
{
    class QuestProcessor
    {
        public Quest Quest;

        public List<QStepDefault> Steps = new List<QStepDefault>();

        public QuestProcessor(Quest quest)
        {
            Quest = quest;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            for (int i = 0; i < quest.Steps.Count; i++)
            {
                if (quest.Steps[i] is QTaskVisit)
                    Steps.Add(new QStepVisit((QTaskVisit)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskHunting)
                    Steps.Add(new QStepHunting((QTaskHunting)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskHuntingDelivery)
                    Steps.Add(new QStepHuntingDelivery((QTaskHuntingDelivery)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskPlaybackVideo)
                    Steps.Add(new QStepPlayCinematic((QTaskPlaybackVideo)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskConditions)
                    Steps.Add(new QStepCondition((QTaskConditions)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskItemThatStabbedPassed)
                    Steps.Add(new QStepItemThatStabbedPassed((QTaskItemThatStabbedPassed)quest.Steps[i]));
                else if (quest.Steps[i] is QTaskMovePC)
                    Steps.Add(new QStepMovePc((QTaskMovePC)quest.Steps[i]));
                else
                    Steps.Add(new QStepDefault());

                Steps[Steps.Count - 1].Quest = quest;
                Steps[Steps.Count - 1].Processor = this;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull
        }

        public QStepDefault GetNowStep(Player player)
        {
            if (!player.Quests.ContainsKey(Quest.QuestId) || player.Quests[Quest.QuestId].Status != QuestStatus.Start)
                return null;

            int step = player.Quests[Quest.QuestId].Step;

            if (step < 0)
                step = 0;

            if (step > Steps.Count - 1)
                step = Steps.Count - 1;

            return Steps[step];
        }

        public int TryGetRewardNpc(Player player)
        {
            if (!player.Quests.ContainsKey(Quest.QuestId))
                return 0;

            QuestData questData = player.Quests[Quest.QuestId];
            if (questData.Status != QuestStatus.Start)
                return 0;

            List<int> npcs = Steps[Steps.Count - 1].GetParticipantVillagers(player);

            if (questData.Step != Steps.Count - 1
                || npcs.Count == 0
                || questData.Counters[0] < npcs.Count - 1)
                return 0;

            return npcs[npcs.Count - 1];
        }

        public void UpdateAroundIcons(Player player)
        {
            player.VisibleNpcs.Each(npc => Global.QuestEngine.ShowIcon(player, npc, true));
        }

        public void StartQuest(Player player)
        {
            int nowQuests = 0;
            foreach (var questData in player.Quests.Values)
                if (questData.Status != QuestStatus.Complete)
                    nowQuests++;

            if (nowQuests > 30)
                return;

            QuestData data = new QuestData(Quest.QuestId);
            player.Quests.Add(Quest.QuestId, data);
            InitNowStep(player);

            new SpQuest(data, null, 1, GetNowStep(player).IsCountersComplete(player)).Send(player.Connection);

            SystemMessages.NewQuest("@quest:" + Quest.QuestId + "001").Send(player.Connection);

            UpdateAroundIcons(player);
        }

        public void ProcessTalk(Player player, DialogController dialog)
        {
            Steps[player.Quests[Quest.QuestId].Step].ProcessTalk(player, dialog, TryGetRewardNpc(player) != 0);
        }

        public void UpdateStepCounters(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];

            bool isCountersComplete = GetNowStep(player).IsCountersComplete(player);

            new SpQuest(questData, null, 1, isCountersComplete).Send(player.Connection);

            if (isCountersComplete)
                UpdateAroundIcons(player);
        }

        public void FinishStep(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];

            questData.Step++;
            questData.Counters = new List<int> {0};

            if (questData.Step == Steps.Count)
            {
                RewardQuest(player);
                return;
            }

            new SpQuest(questData, null, 1, GetNowStep(player).IsCountersComplete(player)).Send(player.Connection);

            InitNowStep(player);

            UpdateAroundIcons(player);
        }

        private void InitNowStep(Player player)
        {
            GetNowStep(player).Init(player);
        }

        private void RewardQuest(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];

            if (questData.Status == QuestStatus.Complete)
                return;

            if (!Reward(player))
            {
                questData.Step--;
                return;
            }

            questData.Status = QuestStatus.Complete;
            new SpQuest(questData).Send(player.Connection);

            SystemMessages.QuestCompleted("@quest:" + Quest.QuestId + "001").Send(player.Connection);

            new SpQuestComplite(Quest.QuestId).Send(player.Connection);

            UpdateAroundIcons(player);
        }

        private bool Reward(Player player)
        {
            QuestReward reward = Global.QuestEngine.GetRewardForPlayer(player, Quest);

            if (reward != null && reward.Items != null)
            {
                if (Global.StorageService.GetFreeSlots(player.Inventory).Count < reward.Items.Count)
                {
                    SystemMessages.InventoryIsFull.Send(player);
                    return false;
                }

                foreach (KeyValuePair<int, int> keyValuePair in reward.Items)
                    Global.StorageService.AddItem(player, player.Inventory, keyValuePair.Key, keyValuePair.Value);
            }

            if (Quest.RewardMoney > 0)
            {
                Global.StorageService.AddMoneys(player, player.Inventory, Quest.RewardMoney);
                SystemMessages.YouReceiveMoney(Quest.RewardMoney.ToString(CultureInfo.InvariantCulture)).Send(player.Connection);
            }

            if (Quest.RewardExp > 0)
                Global.PlayerService.AddExp(player, Quest.RewardExp, null);

            return true;
        }
    }
}