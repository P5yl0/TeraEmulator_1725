using System.Collections.Generic;
using Communication.Interfaces;
using Data.Enums;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.Quest.Enums;
using Data.Structures.World;
using Data.Structures.World.Continent;
using Network.Server;
using Tera.Controllers;
using Tera.QuestEngine.Steps;
using Utils;

namespace Tera.QuestEngine
{
    class QuestEngine : IQuestEngine
    {
        public static Dictionary<int, QuestProcessor> Quests = new Dictionary<int, QuestProcessor>();

        public static Dictionary<int, List<Quest>> AutoStartQuests = new Dictionary<int, List<Quest>>();

        public static Dictionary<int, List<Quest>> NpcStartQuests = new Dictionary<int, List<Quest>>();

        public void Init()
        {
            int valid = 0;

            foreach (var quest in Data.Data.Quests.Values)
            {
                //For debug breakpoint
                if (quest.QuestId == 1306)
                {
                    //int x = 0;
                }

                QuestProcessor processor = new QuestProcessor(quest);
                Quests.Add(quest.QuestId, processor);

                if (quest.StartNpc.Length > 0)
                {
                    int fullId = int.Parse(quest.StartNpc);
                    if (!NpcStartQuests.ContainsKey(fullId))
                        NpcStartQuests.Add(fullId, new List<Quest>());
                    NpcStartQuests[fullId].Add(quest);
                }
                else
                {
                    if (!AutoStartQuests.ContainsKey(quest.NeedLevel))
                        AutoStartQuests.Add(quest.NeedLevel, new List<Quest>());

                    AutoStartQuests[quest.NeedLevel].Add(quest);
                }

                valid++;

                foreach (var step in processor.Steps)
                {
                    if (step.GetType().Name.Equals("QStepDefault"))
                    {
                        valid--;
                        break;
                    }
                }
            }

            Log.Info("QuestEngine: Valid quests: {0} / {1}", valid, Quests.Count);
        }

        public void ResendQuestData(Player player)
        {
            List<int> completed = new List<int>();

            foreach (var questData in player.Quests.Values)
            {
                if (questData.Status != QuestStatus.Complete)
                {
                    new SpQuest(questData,
                                null,
                                1,
                                Quests[questData.QuestId].GetNowStep(player).IsCountersComplete(player))
                        .Send(player.Connection);

                    continue;
                }

                if (Data.Data.Quests[questData.QuestId].Type != QuestType.Mission)
                    continue;
                
                completed.Add(questData.QuestId);
            }

            PlayerLevelUp(player);

            new SpComplitedQuests(completed).Send(player.Connection);
        }

        public void PlayerLevelUp(Player player)
        {
            for (int i = 1; i <= player.PlayerLevel; i++)
            {
                if (AutoStartQuests.ContainsKey(i))
                {
                    for (int j = 0; j < AutoStartQuests[i].Count; j++)
                    {
                        if (!player.Quests.ContainsKey(AutoStartQuests[i][j].QuestId))
                        {
                            Quests[AutoStartQuests[i][j].QuestId].StartQuest(player);
                        }
                    }
                }
            }
        }

        public void ShowIcon(Player player, Npc npc, bool force = false)
        {
            if (!npc.NpcTemplate.IsVillager)
                return;

            QuestType questType = QuestType.Guild;
            QuestStatusIcon icon = QuestStatusIcon.None;

            //Rewards
            {
                bool isReward = false;

                foreach (var data in player.Quests)
                {
                    if (npc.NpcTemplate.FullId == Quests[data.Key].TryGetRewardNpc(player))
                    {
                        if (Data.Data.Quests[data.Key].Type < questType)
                        {
                            questType = Data.Data.Quests[data.Key].Type;
                            isReward = true;

                            if (questType == 0)
                                break;
                        }
                    }
                }

                if (isReward)
                {
                    switch (questType)
                    {
                        case QuestType.Mission:
                            icon = QuestStatusIcon.RewardMissionQuest;
                            break;
                        case QuestType.Normal:
                            icon = QuestStatusIcon.RewardNormalQuest;
                            break;
                        case QuestType.Guild:
                            icon = QuestStatusIcon.RewardGuildQuest;
                            break;
                    }

                    new SpNpcIcon(npc, icon, 1).Send(player.Connection);
                    return;
                }
            }

            //Talk
            {
                bool isTalk = false;

                foreach (var data in player.Quests)
                {
                    QStepDefault step = Quests[data.Key].GetNowStep(player);

                    if (step == null)
                        continue;

                    List<int> villagers = step.GetParticipantVillagers(player);

                    if (villagers.Count > 0 && npc.NpcTemplate.FullId == villagers[0])
                    {
                        if (Data.Data.Quests[data.Key].Type < questType)
                        {
                            questType = Data.Data.Quests[data.Key].Type;
                            isTalk = true;

                            if (questType == 0)
                                break;
                        }
                    }
                }

                if (isTalk)
                {
                    switch (questType)
                    {
                        case QuestType.Mission:
                            icon = QuestStatusIcon.ProgressMissionQuest;
                            break;
                        case QuestType.Normal:
                            icon = QuestStatusIcon.ProgressNormalQuest;
                            break;
                        case QuestType.Guild:
                            icon = QuestStatusIcon.ProgressGuildQuest;
                            break;
                    }

                    new SpNpcIcon(npc, icon, 1).Send(player.Connection);
                    return;
                }
            }

            //Start
            if (NpcStartQuests.ContainsKey(npc.NpcTemplate.FullId))
            {
                bool isStart = false;

                for (int i = 0; i < NpcStartQuests[npc.NpcTemplate.FullId].Count; i++)
                {
                    Quest quest = NpcStartQuests[npc.NpcTemplate.FullId][i];

                    if (player.Quests.ContainsKey(quest.QuestId))
                        continue;

                    if (!quest.NeedClasses.Contains(player.PlayerData.Class))
                        continue;

                    if (quest.NeedLevel > player.PlayerLevel)
                        continue;

                    isStart = true;

                    if (quest.Type < questType)
                        questType = quest.Type;

                    if (questType == 0)
                        break;
                }

                if (isStart)
                {
                    switch (questType)
                    {
                        case QuestType.Mission:
                            icon = QuestStatusIcon.MissionQuest;
                            break;
                        case QuestType.Normal:
                            icon = QuestStatusIcon.NormalQuest;
                            break;
                        case QuestType.Guild:
                            icon = QuestStatusIcon.GuildQuest;
                            break;
                    }
                }
            }

            if (icon != QuestStatusIcon.None)
                new SpNpcIcon(npc, icon, 1).Send(player.Connection);
            else if (force)
                new SpNpcIcon(npc, 0, 0).Send(player.Connection);
        }

        public void AddButtonsToDialog(Player player, Npc npc, List<DialogButton> buttons)
        {
            foreach (var data in player.Quests)
            {
                //Reward
                if (npc.NpcTemplate.FullId == Quests[data.Key].TryGetRewardNpc(player))
                {
                    DialogIcon icon = DialogIcon.CenteredGray;

                    switch (Data.Data.Quests[data.Key].Type)
                    {
                        case QuestType.Mission:
                            icon = DialogIcon.MissionQuestReward;
                            break;
                        case QuestType.Normal:
                            icon = DialogIcon.NormalQuestReward;
                            break;
                        case QuestType.Guild:
                            icon = DialogIcon.GuildQuestReward;
                            break;
                    }

                    buttons.Add(new DialogButton(icon, Data.Data.Quests[data.Key].QuestTitle));
                }
                else
                {
                    //Talk
                    QStepDefault step = Quests[data.Key].GetNowStep(player);

                    if (step == null)
                        continue;

                    List<int> villagers = step.GetParticipantVillagers(player);

                    if (villagers.Count > 0 && npc.NpcTemplate.FullId == villagers[0])
                    {
                        DialogIcon icon = DialogIcon.CenteredGray;

                        switch (Data.Data.Quests[data.Key].Type)
                        {
                            case QuestType.Mission:
                                icon = DialogIcon.MissionQuestProgress;
                                break;
                            case QuestType.Normal:
                                icon = DialogIcon.NormalQuestProgress;
                                break;
                            case QuestType.Guild:
                                icon = DialogIcon.GuildQuestProgress;
                                break;
                        }

                        buttons.Add(new DialogButton(icon, Data.Data.Quests[data.Key].QuestTitle));
                    }
                }
            }

            //Start
            if (NpcStartQuests.ContainsKey(npc.NpcTemplate.FullId))
            {
                for (int i = 0; i < NpcStartQuests[npc.NpcTemplate.FullId].Count; i++)
                {
                    Quest quest = NpcStartQuests[npc.NpcTemplate.FullId][i];

                    if (player.Quests.ContainsKey(quest.QuestId))
                        continue;

                    if (!quest.NeedClasses.Contains(player.PlayerData.Class))
                        continue;

                    if (quest.NeedLevel > player.PlayerLevel)
                        continue;

                    DialogIcon icon = DialogIcon.CenteredGray;

                    switch (quest.Type)
                    {
                        case QuestType.Mission:
                            icon = DialogIcon.MissionQuestStart;
                            break;
                        case QuestType.Normal:
                            icon = DialogIcon.NormalQuestStart;
                            break;
                        case QuestType.Guild:
                            icon = DialogIcon.GuildQuestStart;
                            break;
                    }

                    buttons.Add(new DialogButton(icon, quest.QuestTitle));
                }
            }
        }

        public void ProcessDialog(object dialogController, int questId, int action)
        {
            if (questId == 0)
            {
                EndDialog((DialogController) dialogController, (DialogQuestString) action);
                return;
            }

            if (!Quests.ContainsKey(questId))
                return;

            DialogController dialog = (DialogController) dialogController;
            Player player = dialog.Player;
            Quest quest = Data.Data.Quests[questId];

            if (!player.Quests.ContainsKey(questId))
            {
                dialog.Reset(2, 0, 0, 2, questId);
                dialog.Buttons.Add(new DialogButton(DialogIcon.DefaultQuest, DialogQuestString.Accept));
                dialog.SendDialog(quest, GetRewardForPlayer(player, quest));
            }
            else
                Quests[questId].ProcessTalk(player, dialog);
        }

        public QuestReward GetRewardForPlayer(Player player, Quest quest)
        {
            if (quest.QuestRewardType == QuestRewardType.Allpay)
                return quest.Rewards.Count > 0 ? quest.Rewards[0] : null;

            if (quest.QuestRewardType == QuestRewardType.Class)
                for (int i = 0; i < quest.Rewards.Count; i++)
                {
                    if (quest.Rewards[i].PlayerClass == player.PlayerData.Class)
                        return quest.Rewards[i];
                }

            //todo Switchable reward

            return null;
        }

        public void OnPlayerKillNpc(Player player, Npc npc)
        {
            foreach (var data in player.Quests)
                if (data.Value.Status == QuestStatus.Start)
                    Quests[data.Key].GetNowStep(player).OnKillNpc(player, npc);
        }

        public void OnPlayerLearnSkill(Player player)
        {
            foreach (var data in player.Quests)
                if (data.Value.Status == QuestStatus.Start)
                    Quests[data.Key].GetNowStep(player).OnNewSkillLearned(player);
        }

        public void OnPlayerEnterZone(Player player, Section section)
        {
            foreach (var data in player.Quests)
                if (data.Value.Status == QuestStatus.Start)
                    Quests[data.Key].GetNowStep(player).OnEnterZone(player, section);
        }

        private static void EndDialog(DialogController dialog, DialogQuestString action)
        {
            Player player = dialog.Player;
            int questId = dialog.DialogId;

            if (action == DialogQuestString.Decline)
                return;

            if (!player.Quests.ContainsKey(questId))
            {
                Quests[questId].StartQuest(player);
                new SpNpcTalk(dialog.Npc, dialog.JournalId).Send(player.Connection);
                return;
            }

            Quests[questId].ProcessTalk(player, dialog);
        }

        public void Action()
        {
            
        }
    }
}