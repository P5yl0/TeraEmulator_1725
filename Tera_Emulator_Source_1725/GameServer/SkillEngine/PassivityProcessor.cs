using Data.Enums;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using Data.Structures.World;
using Utils;

namespace Tera.SkillEngine
{
    class PassivityProcessor
    {
        enum Condition
        {
            Equip,
            BattleEnter,
            Attack,
            Attacked,
            Heal,
            Healed
        }

        public static void OnBattleEnter(Player player)
        {
            Apply(player, Condition.BattleEnter);
        }

        public static void OnAttack(Player player, AttackResult attack)
        {
            
        }

        public static void OnAttacked(Player player, AttackResult attack)
        {
            Apply(player, Condition.Attacked, attack);
        }

        public static void OnHeal(Player player, AttackResult heal)
        {
            Apply(player, Condition.Heal, heal);
        }

        public static void OnHealed(Player player, AttackResult heal)
        {
            Apply(player, Condition.Healed, heal);
        }

        private static void Apply(Player player, Condition condition, AttackResult result = null)
        {
            foreach (var passivity in player.GameStats.Passivities)
                ApplyPassivity(player, condition, passivity, result);
        }

        private static void ApplyPassivity(Player player, Condition condition, Passivity passivity, AttackResult result)
        {
            if(!CheckCondition(condition, passivity))
                return;

            if(passivity.Probability >= 0.001f && 1.0f - passivity.Probability > 0.001f)
                if(Funcs.Random().Next(0, 100) > passivity.Probability*100)
                    return;
                
            switch (passivity.Type)
            {
                // Damage block
                case 109:
                    if (result.AttackType != AttackType.Normal || !result.Target.EffectsImpact.IsBlockFrontAttacks)
                        return;

                    if (result.AngleDif < 135 || passivity.Value < result.Damage)
                        return;

                    result.Damage = 0;
                    result.AttackType = AttackType.Block;
                    break;

                // Heal modifier
                case 168:
                case 169:
                    switch (passivity.Method)
                    {
                        case 2:
                            result.HpDiff += (int)passivity.Value;
                            break;
                        case 3:
                            result.HpDiff = (int)passivity.Value * result.HpDiff;
                            break;
                    }
                    break;

                // Do more damage when attack ...
                case 152:
                    if (passivity.MobSize != NpcSize.All)
                    {
                        if (result.Target is Player && passivity.MobSize != NpcSize.Player)
                            return;

                        if (result.Target is Npc && ((Npc)result.Target).NpcTemplate.Size != passivity.MobSize)
                            return;
                    }

                    switch (passivity.Method)
                    {
                        case 2:
                            result.Damage += (int)passivity.Value;
                            break;
                        case 3:
                            result.Damage = (int)(passivity.Value * result.Damage);
                            break;
                    }
                    break;

                // Chance to regenerate MP when combat starts.
                case 207:
                    Communication.Global.FeedbackService.MpChanged(player, (int)passivity.Value, player);
                    break;
            }
        }

        private static bool CheckCondition(Condition condition, Passivity passivity)
        {
            switch (passivity.Type)
            {
                // Damage block
                case 109:
                    return condition == Condition.Attacked;

                // Heal modifier
                case 168:
                    return condition == Condition.Heal;

                // Receive more healing
                case 169:
                    return condition == Condition.Healed;

                // Chance to regenerate MP when combat starts.
                case 207:
                    return condition == Condition.BattleEnter;

                // Do more damage when attack ...
                case 152:
                    return condition == Condition.Attack;

                default:
                    return false;
            }
        }
    }
}
