using Communication;
using Communication.Logic;
using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.World;
using Network.Server;
using Tera.Controllers;
using Utils;

namespace Tera.AiEngine
{
    class PlayerAi : DefaultAi
    {
        public Campfire UsedCampfire = null;

        public long NextRegenUts;

        public long NextDistressUts;

        public long LastBattleUts;

        public override void Init(Creature creature)
        {
            base.Init(creature);

            NextRegenUts = Funcs.GetCurrentMilliseconds() + 1000;
            NextDistressUts = Funcs.GetCurrentMilliseconds() + 60000;
            LastBattleUts = 0;
        }

        public override void OnAttack(Creature target)
        {
            if (Player.Controller is DeathController)
                return;

            if (!(Player.Controller is BattleController))
                Global.ControllerService.SetController(Player, new BattleController());
            
            ((BattleController) Player.Controller).AddTarget(target);
        }

        public override void OnAttacked(Creature attacker, int damage)
        {
            if (Player.Controller is DeathController)
                return;

            if (!(Player.Controller is BattleController))
                Global.ControllerService.SetController(Player, new BattleController());
            
            ((BattleController) Player.Controller).AddTarget(attacker);

        }

        public override void Action()
        {
            if (Player.Controller is DeathController)
                return;

            if (UsedCampfire != null)
            {
                if (UsedCampfire.Instance != Player.Instance ||
                    UsedCampfire.Position.FastDistanceTo(Player.Position) > (UsedCampfire.Type == 4 ? 200 : 100))
                {
                    UsedCampfire = null;

                    if (Player.PlayerMode == PlayerMode.Relax)
                    {
                        Player.PlayerMode = PlayerMode.Normal;
                        Global.VisibleService.Send(Player, new SpCharacterState(Player));
                    }
                }
                else if (Player.PlayerMode == PlayerMode.Normal)
                {
                    Player.PlayerMode = PlayerMode.Relax;
                    Global.VisibleService.Send(Player, new SpCharacterState(Player));
                }
            }

            long now = Funcs.GetCurrentMilliseconds();

            if (Player.Controller is BattleController)
                LastBattleUts = now;

            while (now >= NextRegenUts)
            {
                Regenerate();
                NextRegenUts += Player.Controller is BattleController ? 5000 : 2000;
            }

            while (now > NextDistressUts)
            {
                int timeout = (Player.LifeStats.Stamina > 100) ? 90000 : 60000;

                if(Player.Controller is BattleController)
                    timeout -= 30000;

                Distress();
                NextDistressUts += timeout;
            }
        }

        protected void Regenerate()
        {
            if (UsedCampfire != null)
                CreatureLogic.StaminaChanged(Player, Player.LifeStats.PlusStamina(UsedCampfire.Type == 4 ? 2 : 1));

            if (Player.LifeStats.Hp < Player.MaxHp)
                CreatureLogic.HpChanged(Player, Player.LifeStats.PlusHp(UsedCampfire != null
                                                                            ? Player.MaxHp/100
                                                                            : Player.MaxHp/200));

            switch (Player.PlayerData.Class)
            {
                case PlayerClass.Slayer:
                case PlayerClass.Berserker:
                    if (LastBattleUts + 5000 < Funcs.GetCurrentMilliseconds() && Player.LifeStats.Mp > 0)
                        CreatureLogic.MpChanged(Player, Player.LifeStats.MinusMp(Player.MaxHp / 90));
                    break;

                default:
                    if (Player.LifeStats.Mp < Player.MaxMp)
                        CreatureLogic.MpChanged(Player, Player.LifeStats.PlusMp(UsedCampfire != null
                                                                            ? Player.MaxMp * Player.GameStats.NaturalMpRegen / 350
                                                                            : Player.MaxMp * Player.GameStats.NaturalMpRegen / 700));
                    break;
            }
        }

        protected void Distress()
        {
            if (Player.LifeStats.Stamina <= 0 || UsedCampfire != null)
                return;

            CreatureLogic.StaminaChanged(Player, Player.LifeStats.MinusStamina(1));
        }

        public override void DistanceToCreatureRecalculated(Creature creature, double distance)
        {
            if (creature is Campfire)
                DistanceToCampfireRecalculated(creature as Campfire, distance);
        }

        private void DistanceToCampfireRecalculated(Campfire campfire, double distance)
        {
            if (distance <= (campfire.Type == 4 ? 200 : 100))
                UsedCampfire = campfire;
        }
    }
}
