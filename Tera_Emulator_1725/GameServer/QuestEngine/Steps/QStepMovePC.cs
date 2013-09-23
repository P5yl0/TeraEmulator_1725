using System.Collections.Generic;
using Data.Structures.Player;
using Data.Structures.Quest.Tasks;
using Data.Structures.World.Continent;
using Utils;

namespace Tera.QuestEngine.Steps
{
    class QStepMovePc : QStepDefault
    {
        protected static Dictionary<int, int> TaskAreaIdSectionNameIdMap;

        static QStepMovePc()
        {
            TaskAreaIdSectionNameIdMap = new Dictionary<int, int>
                {
                    //Shrine of Yurian
                    {21300020, 13024},

                    //Tainted Gorge Bridge
                    {21300011, 13009},
                };
        }

        protected QTaskMovePC Task;

        public QStepMovePc(QTaskMovePC task)
        {
            Task = task;
        }

        public override void OnEnterZone(Player player, Section section)
        {
            if(section == null)
            {
                Log.Warn("QStepMovePc: Warning, current player section is NULL!");
                return;
            }

            if (Task.TargetArea == null
                || Task.TargetArea.Length < 2
                || !TaskAreaIdSectionNameIdMap.ContainsKey(Task.TargetArea[1]))
                return;

            if (TaskAreaIdSectionNameIdMap[Task.TargetArea[1]] == section.NameId)
                Processor.FinishStep(player);
        }
    }
}
