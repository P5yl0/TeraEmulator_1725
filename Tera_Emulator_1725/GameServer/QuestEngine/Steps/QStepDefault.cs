using System.Collections.Generic;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Quest;
using Data.Structures.World.Continent;
using Tera.Controllers;

namespace Tera.QuestEngine.Steps
{
    class QStepDefault
    {
        public Quest Quest;

        public QuestProcessor Processor;

        public virtual void Init(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            questData.Counters = new List<int>(1) {0};
        }

        public virtual void OnKillNpc(Player player, Npc npc)
        {

        }

        public virtual void OnNewSkillLearned(Player player)
        {
            
        }

        public virtual void OnEnterZone(Player player, Section section)
        {

        }

        public virtual List<int> GetParticipantVillagers(Player player)
        {
            return new List<int>();
        }

        public virtual List<int> GetParticipantMonsters(Player player)
        {
            return new List<int>();
        }

        public virtual void ProcessTalk(Player player, DialogController dialog, bool isReward)
        {
            
        }

        public virtual bool IsCountersComplete(Player player)
        {
            return true;
        }
    }
}
