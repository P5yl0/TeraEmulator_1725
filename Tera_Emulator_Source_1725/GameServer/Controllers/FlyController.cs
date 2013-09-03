using System;
using System.Threading.Tasks;
using Communication.Logic;
using Data.Enums;
using Data.Enums.Pegasus;
using Data.Enums.Player;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Data.Structures.World.Pegasus;
using Network.Server;
using Utils;

namespace Tera.Controllers
{
    internal class FlyController : IController
    {
        protected int FlyTraceIndex;

        protected Player Player;

        protected bool IsActive = true;

        public FlyController(Player player, int flyTraceIndex)
        {
            Player = player;
            FlyTraceIndex = flyTraceIndex;
        }

        private async void StartFly()
        {
            try
            {
                if (!Data.Data.PegasusPaths.ContainsKey(FlyTraceIndex) ||
                    !Data.Data.PegasusPaths[FlyTraceIndex].ContainsKey(PType.high_after) ||
                    !Data.Data.PegasusPaths[FlyTraceIndex].ContainsKey(PType.normal))
                    return;

                Player.PlayerMode = PlayerMode.Flying;

                Communication.Global.VisibleService.Send(Player, new SpCharacterState(Player));
                Communication.Global.VisibleService.Send(Player, new SpPegasusInfo(Player));

                long lastSendTime = 0;

                for (int i = 0;
                     i < Data.Data.PegasusPaths[FlyTraceIndex][PType.normal].Stage.FlySteps.Count;
                     i++)
                {
                    if (!IsActive)
                        break;

                    var step = Data.Data.PegasusPaths[FlyTraceIndex][PType.normal].Stage.FlySteps[i];

                    int pauseTime = i == 0
                                        ? step.Time
                                        : step.Time -
                                          Data.Data.PegasusPaths[FlyTraceIndex][PType.normal].Stage.FlySteps[
                                              i - 1].Time;

                    await Task.Delay(pauseTime);

                    PlayerLogic.PlayerMoved(Player, Player.Position.X, Player.Position.Y, Player.Position.Z,
                        Player.Position.Heading, step.Loc.X, step.Loc.Y, step.Loc.Z, PlayerMoveType.Normal, 0, 0);

                    if (step.Time - lastSendTime > 3000 ||
                        Data.Data.PegasusPaths[FlyTraceIndex][PType.normal].Stage.FlySteps.Count <= i - 1 ||
                        i == 0)
                    {
                        lastSendTime = DateTime.Now.Ticks;
                        ExecuteStep(step, 0);
                    }

                }

                new SpPegasusFlight(Player, FlyTraceIndex, 2, 0).Send(Player.Connection);

                Player.Position = new WorldPosition
                                {
                                    X =
                                        Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                            0].Loc.X,
                                    Y =
                                        Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                            0].Loc.Y,
                                    Z =
                                        Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                            0].Loc.Z,
                                    MapId = Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.ContinentId
                                };

                Communication.Global.TeleportService.ForceTeleport(Player, Player.Position);
            }
            catch(Exception)
            {
                
            }
        }

        public async void EndFly(int mapId)
        {
            try
            {
                Communication.Global.VisibleService.Send(Player, new SpPegasusFlight(Player, FlyTraceIndex, 1, 0));
                Communication.Global.VisibleService.Send(Player, new SpPegasusInfo(Player));

                long lastSendTime = 0;

                for (int i = 0; i < Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps.Count; i++)
                {
                    if (!IsActive)
                        break;

                    var step = Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[i];

                    int pauseTime = i == 0
                                        ? step.Time
                                        : step.Time -
                                          Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                              i - 1].Time;

                    await Task.Delay(pauseTime);

                    PlayerLogic.PlayerMoved(Player, Player.Position.X, Player.Position.Y, Player.Position.Z,
                                            Player.Position.Heading, step.Loc.X, step.Loc.Y, step.Loc.Z,
                                            PlayerMoveType.Normal, 0, 0);

                    if (step.Time - lastSendTime > 3000 ||
                        Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps.Count <= i + 1 || i == 0)
                    {
                        lastSendTime = DateTime.Now.Ticks;
                        ExecuteStep(step, 1);
                    }
                }
            }
            catch (Exception)
            {

            }

            Player.PlayerMode = PlayerMode.Normal;

            new SpCharacterState(Player).Send(Player.Connection);
            new SpPegasusFinishFly(Player).Send(Player.Connection);

            Communication.Global.ControllerService.SetController(Player, new DefaultController());
        }

        private void ExecuteStep(FlyStep flyStep, int state)
        {
            //TODO other player visiblity
            if (Player.Instance != null)
            {
                Communication.Global.VisibleService.Send(Player,
                                                         new SpPegasusFlight(Player, FlyTraceIndex, state, flyStep.Time));
            }
        }

        public void Start(Player player)
        {
            try
            {
                StartFly();
            }
            catch (Exception )//e)
            {
                Log.Warn("FlyController: Cant fly to point with FlyTraceIndex {0}", FlyTraceIndex);
            }
        }

        public void Release()
        {
            try
            {
                int lastStep = Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps.Count - 1;

                Player.Position = new WorldPosition
                                      {
                                          X =
                                              Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                                  lastStep].Loc.X,
                                          Y =
                                              Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                                  lastStep].Loc.Y,
                                          Z =
                                              Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.FlySteps[
                                                  lastStep].Loc.Z,
                                          MapId =
                                              Data.Data.PegasusPaths[FlyTraceIndex][PType.high_after].Stage.ContinentId
                                      };


                IsActive = false;
            }
            catch(Exception)
            {
                
            }
        }

        public void Action()
        {

        }
    }
}
