using Data.Structures.Quest.Tasks;
using Network.Server;

namespace Tera.QuestEngine.Steps
{
    class QStepPlayCinematic : QStepDefault
    {
        protected QTaskPlaybackVideo  QTaskPlaybackVideo;
 
        public QStepPlayCinematic(QTaskPlaybackVideo qTaskPlaybackVideo)
        {
            QTaskPlaybackVideo = qTaskPlaybackVideo;
        }

        public override void Init(Data.Structures.Player.Player player)
        {
            base.Init(player);
            new SpQuestMovie(QTaskPlaybackVideo.MovieId).Send(player.Connection);
            Processor.FinishStep(player);
        }
    }
}
