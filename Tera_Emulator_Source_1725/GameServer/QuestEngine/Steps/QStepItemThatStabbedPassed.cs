using System;
using System.Collections.Generic;
using Communication;
using Data.Enums;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.Quest.Tasks;
using Data.Structures.World;
using Tera.Controllers;

namespace Tera.QuestEngine.Steps
{
    class QStepItemThatStabbedPassed : QStepDefault
    {
        public QTaskItemThatStabbedPassed Task;

        public QStepItemThatStabbedPassed(QTaskItemThatStabbedPassed task)
        {
            Task = task;
        }

        public override void Init(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            questData.Counters = new List<int>(1) {Task.Count};
        }

        public override List<int> GetParticipantVillagers(Player player)
        {
            return new List<int> {Task.CpecifyTargetNpcFullId};
        }

        public override void ProcessTalk(Player player, DialogController dialog, bool isReward)
        {
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
    }
}
