using Data.Structures.Player;
using Data.Structures.Quest.Tasks;

namespace Tera.QuestEngine.Steps
{
    class QStepCondition : QStepDefault
    {
        public QTaskConditions Task;

        public QStepCondition(QTaskConditions task)
        {
            Task = task;
        }

        public override void Init(Player player)
        {
            base.Init(player);

            OnNewSkillLearned(player); //Check for already learned skill
        }

        public override void OnNewSkillLearned(Player player)
        {
            if (Task.SkillId != 0 && player.Skills.Contains(Task.SkillId))
                Processor.FinishStep(player);
        }
    }
}
