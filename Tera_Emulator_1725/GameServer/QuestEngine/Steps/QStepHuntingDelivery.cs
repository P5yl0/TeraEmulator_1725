using System;
using System.Collections.Generic;
using Communication;
using Data.Enums;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.Quest.Tasks;
using Data.Structures.World;

namespace Tera.QuestEngine.Steps
{
    class QStepHuntingDelivery : QStepDefault
    {
        public QTaskHuntingDelivery Task;

        public QStepHuntingDelivery(QTaskHuntingDelivery task)
        {
            Task = task;
        }

        public override void OnKillNpc(Player player, Npc npc)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            
            if (questData.Counters[0] == Task.Count
                || !Task.DroppedFrom.Contains(npc.NpcTemplate.FullId))
                return;

            questData.Counters[0]++;
            Processor.UpdateStepCounters(player);
        }

        public override List<int> GetParticipantVillagers(Player player)
        {
            if (!IsCountersComplete(player))
                return new List<int>();

            return new List<int> {Task.DeliverToFullId};
        }

        public override List<int> GetParticipantMonsters(Player player)
        {
            if (IsCountersComplete(player))
                return new List<int>();

            return new List<int>(Task.DroppedFrom);
        }

        public override void ProcessTalk(Player player, Controllers.DialogController dialog, bool isReward)
        {
            if (!IsCountersComplete(player))
                return;

            string title = Task.JournalText.Substring(0, Task.JournalText.IndexOf(":", StringComparison.OrdinalIgnoreCase) + 1);
            int titleId = int.Parse(Task.JournalText.Substring(Task.JournalText.IndexOf(":", StringComparison.OrdinalIgnoreCase) + 1)) - 1;
            title = title + titleId;

            if (dialog.Buttons[0].Text.Equals(title))
            {
                Processor.FinishStep(player);
            }
            else
            {
                dialog.Reset(3, 0, player.Quests[Quest.QuestId].Step + 1, 1 + (titleId % 1000) / 2, Quest.QuestId);
                dialog.Buttons.Add(new DialogButton(DialogIcon.DefaultQuest, title));
                dialog.SendDialog(Quest, Global.QuestEngine.GetRewardForPlayer(player, Quest));
            }
        }

        public override bool IsCountersComplete(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            return questData.Counters[0] == Task.Count;
        }
    }
}
