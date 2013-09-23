using System.Collections.Generic;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.Quest.Tasks;

namespace Tera.QuestEngine.Steps
{
    class QStepHunting : QStepDefault
    {
        public QTaskHunting Task;

        public QStepHunting(QTaskHunting task)
        {
            Task = task;
        }

        public override void Init(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            questData.Counters = new List<int>(Task.Targets.Count);
            for (int i = 0; i < Task.Targets.Count; i++)
                questData.Counters.Add(0);
        }

        public override void OnKillNpc(Player player, Npc npc)
        {
            QuestData questData = player.Quests[Quest.QuestId];

            for (int i = 0; i < Task.Targets.Count; i++)
            {
                if (Task.Targets[i].Key != npc.NpcTemplate.FullId)
                    continue;

                if (questData.Counters[i] == Task.Targets[i].Value)
                    return;

                questData.Counters[i]++;
                Processor.UpdateStepCounters(player);
            }

            if (IsCountersComplete(player))
                Processor.FinishStep(player);
            else
                Processor.UpdateStepCounters(player);
        }

        public override List<int> GetParticipantMonsters(Player player)
        {
            if (IsCountersComplete(player))
                return new List<int>();

            List<int> monsters = new List<int>();

            for (int i = 0; i < Task.Targets.Count; i++)
                monsters.Add(Task.Targets[i].Key);

            return monsters;
        }

        public override bool IsCountersComplete(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];

            for (int i = 0; i < Task.Targets.Count; i++)
                if (questData.Counters[i] != Task.Targets[i].Value)
                    return false;

            return true;
        }
    }
}
