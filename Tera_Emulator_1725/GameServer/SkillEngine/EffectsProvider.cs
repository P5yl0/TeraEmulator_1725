using Data.Structures.World;
using Tera.SkillEngine.Effects;

namespace Tera.SkillEngine
{
    class EffectsProvider
    {
        protected static void Add(Abnormal abnormal, EfDefault effect, int effectIndex)
        {
            abnormal.Effects.Add(effect);

            effect.Creature = abnormal.Creature;
            effect.Abnormality = abnormal.Abnormality;
            effect.Effect = abnormal.Abnormality.Effects[effectIndex];

            effect.Init();
        }

        public static void ProvideEffects(Abnormal abnormal)
        {
            switch (abnormal.Abnormality.Id)
            {
                //###NotShow###Mana extracted from a Magic Stone quickly restores your MP.
                case 200500: //###NotShow###Mana extracted from a Magic Stone quickly restores your MP.
                case 800700: //###NotShow###Mana extracted from a Magic Stone quickly restores your MP.
                    break;

                //[Test]
                case 4731: //[Test]
                case 11100: //[Test]
                case 11101: //[Test]
                case 11102: //[Test]
                case 11103: //[Test]
                case 11104: //[Test]
                case 11105: //[Test]
                case 11110: //[Test]
                case 11111: //[Test]
                case 11112: //[Test]
                case 11113: //[Test]
                case 11114: //[Test]
                case 11115: //[Test]
                case 11120: //[Test]
                case 11121: //[Test]
                case 11122: //[Test]
                case 11123: //[Test]
                case 11124: //[Test]
                case 11125: //[Test]
                case 11200: //[Test]
                case 11201: //[Test]
                case 11202: //[Test]
                case 11203: //[Test]
                case 11204: //[Test]
                case 11205: //[Test]
                case 11210: //[Test]
                case 11211: //[Test]
                case 11212: //[Test]
                case 11213: //[Test]
                case 11214: //[Test]
                case 11215: //[Test]
                case 11220: //[Test]
                case 11221: //[Test]
                case 11222: //[Test]
                case 11223: //[Test]
                case 11224: //[Test]
                case 11225: //[Test]
                case 28101: //[Test]
                case 501150: //[Test]
                case 801550: //[Test]
                case 900500: //[Test]
                case 900501: //[Test]
                case 47600200: //[Test]
                case 49400600: //[Test]
                case 99001300: //[Test]
                case 99001301: //[Test]
                case 99001302: //[Test]
                case 99001303: //[Test]
                case 99001310: //[Test]
                case 99001320: //[Test]
                case 99001330: //[Test]
                case 99001340: //[Test]
                case 99001350: //[Test]
                case 99001360: //[Test]
                case 99001370: //[Test]
                case 99001810: //[Test]
                case 99002100: //[Test]
                case 99002110: //[Test]
                case 99002120: //[Test]
                case 99002130: //[Test]
                case 99002140: //[Test]
                case 99002150: //[Test]
                case 99002160: //[Test]
                case 99002170: //[Test]
                case 99002180: //[Test]
                case 99002800: //[Test]
                case 99002810: //[Test]
                case 99002820: //[Test]
                case 99002830: //[Test]
                    break;

                //[VAL] of total HP every [VAL].
                case 47600300: //$H_W_GOOD3%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //[VAL] the crit chance.
                case 21170: //$H_W_GOODDoubles$COLOR_END the crit chance.
                case 23180: //$H_W_GOODDoubles$COLOR_END the crit chance.
                    break;

                //100 kills. Crit chance increased by [VAL].
                case 47650200: //100 kills. Crit chance increased by $H_W_GOOD$value$COLOR_END.
                    break;

                //100% resist to all harmful effect and take no damage from all attacks.
                case 47600100: //100% resist to all harmful effect and take no damage from all attacks.
                    break;

                //100% resist to all harmful effects and increase endurance by [VAL].
                case 47662800: //100% resist to all harmful effects and increase endurance by $H_W_GOOD$value2$COLOR_END.
                    break;

                //100% resistance to all harmful effects.
                case 99002000: //100% resistance to all harmful effects.
                case 99002010: //100% resistance to all harmful effects.
                case 99002020: //100% resistance to all harmful effects.
                case 99002030: //100% resistance to all harmful effects.
                case 99002040: //100% resistance to all harmful effects.
                case 99002050: //100% resistance to all harmful effects.
                case 99002060: //100% resistance to all harmful effects.
                case 99002070: //100% resistance to all harmful effects.
                case 99002080: //100% resistance to all harmful effects.
                    break;

                //100% resistant to all harmful effects. Endurance increases by [VAL].
                case 99002200: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002210: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002220: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002230: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002240: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002250: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002260: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                case 99002270: //100% resistant to all harmful effects. Endurance increases by $H_W_GOOD$value2$COLOR_END.
                    break;

                //150 kills. Decreased cooldown for ranged skills by [VAL] while in effect.
                case 47650300: //150 kills. Decreased cooldown for ranged skills by $H_W_GOOD$value$COLOR_END while in effect.
                    break;

                //250 kills. Immune to all harmful effects. Increases balance.
                case 47650500: //250 kills.$BRImmune to all harmful effects.$BRIncreases balance.
                    break;

                //250 kills. Stunned. Cannot move.
                case 47650510: //250 kills. Stunned. Cannot move.
                    break;

                //50 Kills. Increased power by [VAL].
                case 47650100: //50 Kills. Increased power by $H_W_GOOD$value$COLOR_END.
                    break;

                //A blinding flash makes it hard to see things around you! White Flashes cannot be removed.
                case 47660900: //A blinding flash makes it hard to see things around you! White Flashes cannot be removed.
                    break;

                //A nexus implodes upon you! In [VAL], you will die.
                case 476806: //A nexus implodes upon you! In $H_W_BAD$time$COLOR_END, you will die.
                    break;

                //A reward for waiting patiently in the server queue. XP earned through monster kills increases by [VAL].
                case 999997: //A reward for waiting patiently in the server queue.$BRXP earned through monster kills increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //A shield is created due to insanity and makes you 100% resistant to any harmful effect.
                case 47670800: //A shield is created due to insanity and makes you 100% resistant to any harmful effect.
                    break;

                //A strong shock decreases HP by [VAL] every [VAL]. Stuns you and immobilizes you. Strong Shock cannot be removed.
                case 476618: //A strong shock decreases HP by $H_W_BAD60%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. Stuns you and immobilizes you. Strong Shock cannot be removed.
                    break;

                //A strong shock decreases HP by [VAL] of total HP every [VAL]. Stuns you and immobilizes you. Strong Shock cannot be removed.
                case 476628: //A strong shock decreases HP by $H_W_BAD60%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END. Stuns you and immobilizes you. Strong Shock cannot be removed.
                    break;

                //Absorbs all damage.
                case 47641100: //Absorbs all damage.
                case 47641300: //Absorbs all damage.
                    break;

                //Absorbs up to [VAL] damage.
                case 10400: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10401: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10402: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10403: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10404: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10405: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10500: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10501: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10502: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10503: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10504: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10505: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10510: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10511: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10512: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10513: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10514: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10515: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10520: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10521: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10522: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10523: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10524: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10525: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10600: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10601: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10602: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10603: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10604: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10605: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10610: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10611: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10612: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10613: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10614: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10615: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10620: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10621: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10622: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10623: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10624: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 10625: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001400: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001410: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001420: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001430: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001440: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001450: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001460: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                case 99001470: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per 100 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                case 201304: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRCosts 1 MP per 100 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per 14 points of damage. Ends when you reach maximum damage or run out of MP.
                case 25150: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Costs 1 MP per 14 points of damage. Ends when you reach maximum damage or run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per 20 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                case 201300: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRCosts 1 MP per 20 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per 33 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                case 201301: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRCosts 1 MP per 33 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per 50 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                case 201302: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRCosts 1 MP per 50 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                case 201303: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRCosts 1 MP per 50 damage absorbed. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per maximum of 14 points of damage. Ends when maximum damage is reached or you run out of MP.
                case 500600: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Costs 1 MP per maximum of 14 points of damage. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. Costs 1 MP per maximum of 33 points of damage. Ends when maximum damage is reached or you run out of MP.
                case 500601: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Costs 1 MP per maximum of 33 points of damage. Ends when maximum damage is reached or you run out of MP.
                    break;

                //Absorbs up to [VAL] damage. If you can't destroy it in time, all PCs will die and Kelsaik will be rampant!
                case 47501500: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. If you can't destroy it in time, all PCs will die and Kelsaik will be rampant!
                case 47501600: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. If you can't destroy it in time, all PCs will die and Kelsaik will be rampant!
                    break;

                //Absorbs up to [VAL] damage. Immune to knockdown and immobilization. Ends when maximum damage is reached.
                case 800300: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Immune to knockdown and immobilization. Ends when maximum damage is reached.
                case 800301: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Immune to knockdown and immobilization. Ends when maximum damage is reached.
                case 800302: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Immune to knockdown and immobilization. Ends when maximum damage is reached.
                case 800303: //Absorbs up to $H_W_GOOD$value$COLOR_END damage. Immune to knockdown and immobilization. Ends when maximum damage is reached.
                    break;

                //Absorbs up to [VAL] damage. Immune to knockdown and immobilize while in effect.
                case 2755: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRImmune to knockdown and immobilize while in effect.
                case 2756: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRImmune to knockdown and immobilize while in effect.
                case 2757: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRImmune to knockdown and immobilize while in effect.
                case 2758: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRImmune to knockdown and immobilize while in effect.
                case 2759: //Absorbs up to $H_W_GOOD$value$COLOR_END damage.$BRImmune to knockdown and immobilize while in effect.
                    break;

                //aggro dump. No debuff tooltip display (isShow=False). If this tooltip displays, BTS is required!
                case 501501: //aggro dump. No debuff tooltip display (isShow=False). If this tooltip displays, BTS is required!
                    break;

                //All other party members within 10m of you can't use any skills.
                case 47611200: //All other party members within 10m of you can't use any skills.
                    break;

                //All resistances decrease by [VAL].
                case 15010: //All resistances decrease by $H_W_BAD$value$COLOR_END.
                    break;

                //An electric current ran through the whole body with its shocks! Decreases HP by [VAL] every [VAL] and stuns you and party members within 12 m for a certain period of time! Shocking Electricity cannot be removed.
                case 47660200: //An electric current ran through the whole body with its shocks! Decreases HP by $H_W_BAD3%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END and stuns you and party members within 12 m for a certain period of time! Shocking Electricity cannot be removed.
                    break;

                //As you've become stronger due to the Manpower of the Black Warp, you have a better chance of getting items.
                case 47680900: //As you've become stronger due to the Manpower of the Black Warp, you have a better chance of getting items.
                    break;

                //Attack speed increases by [VAL].
                case 2725: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 2726: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 2727: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 2728: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 2729: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 15050: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 21010: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 21070: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 23020: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 200700: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 400500: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                case 400501: //Attack speed increases by $H_W_GOOD$value$COLOR_END.
                    Add(abnormal, new EfAttackSpeedInc(), 0);
                    break;

                //Because Dimensional Group Dance is stacked, you are forced to dance! Oh geez, you cannot stop!
                case 476814: //Because Dimensional Group Dance is stacked, you are forced to dance! Oh geez, you cannot stop!
                    break;

                //Bedlam! Your movement keys are reversed. This effect cannot be removed.
                case 476110: //Bedlam! Your movement keys are reversed. This effect cannot be removed.
                    break;

                //Can revive on the spot.
                case 47651000: //Can revive on the spot.
                    break;

                //Cannot move or use evasive skills while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501320: //Cannot move or use evasive skills while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501321: //Cannot move or use evasive skills while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501322: //Cannot move or use evasive skills while in effect. Effect ends if you are damaged for certain amount or knocked down.
                    break;

                //Cannot move while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501300: //Cannot move while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501301: //Cannot move while in effect. Effect ends if you are damaged for certain amount or knocked down.
                case 501302: //Cannot move while in effect. Effect ends if you are damaged for certain amount or knocked down.
                    break;

                //Cannot move while in effect. Effect ends when knocked down.
                case 21100: //Cannot move while in effect. Effect ends when knocked down.
                    break;

                //Cannot move. Effect ends when knocked down.
                case 300200: //Cannot move. Effect ends when knocked down.
                    break;

                //Cannot trigger any new skills while in effect. Effect cannot be removed
                case 476112: //Cannot trigger any new skills while in effect. Effect cannot be removed
                    break;

                //Cannot use any skill while in effect. Distortion Effect cannot be removed.
                case 47650600: //Cannot use any skill while in effect. Distortion Effect cannot be removed.
                    break;

                //Cannot use evading or movement skills while in effect.
                case 2905: //Cannot use evading or movement skills while in effect.
                    break;

                //Cannot use lock on skills while in effect.
                case 2890: //Cannot use lock on skills while in effect.
                    break;

                //Cannot use skills.
                case 47500300: //Cannot use skills.
                    break;

                //Chances of being immobilized or knocked down decrease.
                case 90400: //Chances of being immobilized or knocked down decrease.
                    break;

                //Chewy and tasty; restores HP by [VAL].
                case 4760: //Chewy and tasty; restores HP by $H_W_GOOD7122$COLOR_END.
                    break;

                //Circumvents crystal breakage upon death, consuming one effect. The effect stacks up to 99 times.
                case 4600: //Circumvents crystal breakage upon death, consuming one effect. The effect stacks up to 99 times.
                    break;

                //Combat XP bonus of [VAL].
                case 1100: //Combat XP bonus of $H_W_GOOD$value$COLOR_END.
                    break;

                //Combat XP increases by [VAL]. Effect persists through death.
                case 1101: //Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                case 1102: //Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                case 1103: //Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                case 1104: //Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                    break;

                //Confused! Your movement keys are reversed. Bedlam cannot be removed.
                case 47610200: //Confused! Your movement keys are reversed.$BRBedlam cannot be removed.
                    break;

                //Consumes [VAL] of total HP unless the effect is removed.
                case 49400900: //Consumes $H_W_BAD$value$COLOR_END of total HP unless the effect is removed.
                    break;

                //Cooldown of all skills increases by $value while in effect.
                case 300900: //Cooldown of all skills increases by $value while in effect.
                    break;

                //Crafting speed increases by [VAL].
                case 1640: //Crafting speed increases by $H_W_GOOD$value$COLOR_END.
                case 1641: //Crafting speed increases by $H_W_GOOD$value$COLOR_END.
                case 1642: //Crafting speed increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Crit chance increases by [VAL] while critical resistance decreases by [VAL].
                case 100800: //Crit chance increases by $H_W_GOOD$value$COLOR_END while critical resistance decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Crit chance increases by [VAL] while within 20m of caster.
                case 700630: //Crit chance increases by $H_W_GOODtwo times$COLOR_END while within 20m of caster.
                    break;

                //Crit chance increases by [VAL].
                case 2680: //Crit chance increases by $H_W_GOOD$value$COLOR_END.
                case 2681: //Crit chance increases by $H_W_GOOD$value$COLOR_END.
                case 2682: //Crit chance increases by $H_W_GOOD$value$COLOR_END.
                case 2683: //Crit chance increases by $H_W_GOOD$value$COLOR_END.
                case 2684: //Crit chance increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                case 2695: //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                case 2696: //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                case 2697: //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                case 2698: //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                case 2699: //Crit damage increases by $H_W_GOOD+$value$COLOR_END.
                    break;

                //Critical damage increased [VAL].
                case 10200: //Critical damage increased $H_W_GOOD0.18$COLOR_END.
                case 10201: //Critical damage increased $H_W_GOOD0.22$COLOR_END.
                case 10202: //Critical damage increased $H_W_GOOD0.26$COLOR_END.
                case 10203: //Critical damage increased $H_W_GOOD0.28$COLOR_END.
                case 10204: //Critical damage increased $H_W_GOOD0.32$COLOR_END.
                case 10205: //Critical damage increased $H_W_GOOD0.36$COLOR_END.
                case 10210: //Critical damage increased $H_W_GOOD0.22$COLOR_END.
                case 10211: //Critical damage increased $H_W_GOOD0.26$COLOR_END.
                case 10212: //Critical damage increased $H_W_GOOD0.3$COLOR_END.
                case 10213: //Critical damage increased $H_W_GOOD0.36$COLOR_END.
                case 10214: //Critical damage increased $H_W_GOOD0.4$COLOR_END.
                case 10215: //Critical damage increased $H_W_GOOD0.44$COLOR_END.
                case 10220: //Critical damage increased $H_W_GOOD0.2$COLOR_END.
                case 10221: //Critical damage increased $H_W_GOOD0.24$COLOR_END.
                case 10222: //Critical damage increased $H_W_GOOD0.28$COLOR_END.
                case 10223: //Critical damage increased $H_W_GOOD0.32$COLOR_END.
                case 10224: //Critical damage increased $H_W_GOOD0.36$COLOR_END.
                case 10225: //Critical damage increased $H_W_GOOD0.4$COLOR_END.
                    break;

                //Critical damage increases [VAL] with wrath.
                case 15090: //Critical damage increases $H_W_GOOD$value$COLOR_END with wrath.
                    break;

                //Damage absolute invincibility
                case 6: //Damage absolute invincibility
                    break;

                //Damage half invincibility
                case 7: //Damage half invincibility
                    break;

                //Damage of skills increases by [VAL].
                case 15110: //Damage of skills increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Damage received from ranged attacks decreases by [VAL]. Resistances increase by [VAL].
                case 801000: //Damage received from ranged attacks decreases by $H_W_GOOD$value2$COLOR_END. Resistances increase by $H_W_GOOD$value$COLOR_END.
                    break;

                //Debuff absolute invincibility
                case 8: //Debuff absolute invincibility
                    break;

                //Debuff half invincibility
                case 9: //Debuff half invincibility
                    break;

                //Decrease endurance by [VAL].
                case 101200: //Decrease endurance by $H_W_BAD$value$COLOR_END.
                    break;

                //Decrease HP by [VAL] every [VAL] by deadly poison.
                case 15100: //Decrease HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END by deadly poison.
                    break;

                //Decreased power and endurance by [VAL] and [VAL], respectively. Speed decreased by [VAL].
                case 47641200: //Decreased power and endurance by $H_W_BAD$value$COLOR_END and $H_W_BAD$value2$COLOR_END, respectively. Speed decreased by $H_W_BAD$value3$COLOR_END.
                    break;

                //Decreases aggro by [VAL]. Subtlety cannot be removed.
                case 47611700: //Decreases aggro by $H_W_GOOD$value$COLOR_END. Subtlety cannot be removed.
                    break;

                //Decreases all resistances.
                case 47500150: //Decreases all resistances.
                    break;

                //Decreases attack speed by $value3, but increases Power by $value. Increases skill damage by $value2 in case of an additional attack on other PCs. While the pose is kept, MP decreases by $value4 every $tickInterval4.
                case 601100: //Decreases attack speed by $value3, but increases Power by $value. Increases skill damage by $value2 in case of an additional attack on other PCs. While the pose is kept, MP decreases by $value4 every $tickInterval4.
                    break;

                //Decreases attack speed by [VAL].
                case 21090: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 21150: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 22100: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 23100: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 27060: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 28080: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                case 701601: //Decreases attack speed by $H_W_BAD$value$COLOR_END.
                    break;

                //Decreases endurance by  [VAL], increases Power by [VAL].
                case 476221: //Decreases endurance by  $H_W_Bad$value$COLOR_END, increases Power by $H_W_GOOD$value2$COLOR_END.
                case 476222: //Decreases endurance by  $H_W_Bad$value$COLOR_END, increases Power by $H_W_GOOD$value2$COLOR_END.
                case 476223: //Decreases endurance by  $H_W_Bad$value$COLOR_END, increases Power by $H_W_GOOD$value2$COLOR_END.
                case 47621500: //Decreases endurance by  $H_W_Bad$value$COLOR_END, increases Power by $H_W_GOOD$value2$COLOR_END.
                    break;

                //Decreases endurance by  [VAL], increases Power by [VAL]. Double-edged Strike V cannot be removed.
                case 476224: //Decreases endurance by  $H_W_Bad$value$COLOR_END, increases Power by $H_W_GOOD$value2$COLOR_END. Double-edged Strike V cannot be removed.
                    break;

                //Decreases endurance by [VAL] but increase healing rate by [VAL]. Inner Strength cannot be removed.
                case 47611800: //Decreases endurance by $H_W_BAD$value$COLOR_END but increase healing rate by $H_W_GOOD$value2$COLOR_END. Inner Strength cannot be removed.
                    break;

                //Decreases endurance by [VAL], but grants 100% resistance to all harmful effects. Tombward cannot be removed.
                case 47613300: //Decreases endurance by $H_W_GOOD$value$COLOR_END, but grants 100% resistance to all harmful effects. Tombward cannot be removed.
                    break;

                //Decreases Endurance by [VAL].
                case 28090: //Decreases Endurance by $H_W_BAD$value$COLOR_END.
                    break;

                //Decreases endurance of the party members within 20m radius excluding you.
                case 47610900: //Decreases endurance of the party members within 20m radius excluding you.
                    break;

                //Decreases HP by [VAL] every [VAL].
                case 800900: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800901: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800902: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800903: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800904: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800905: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 800906: //Decreases HP by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 47620400: //Decreases HP by $H_W_BAD2%$COLOR_END every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //Decreases HP by [VAL] every [VAL]. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by [VAL].
                case 800921: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                case 800922: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                case 800923: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                case 800924: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                case 800925: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                case 800926: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Additionally increases the cooldown of all close-range attack skills used while this effect lasts, by $H_W_BAD$value$COLOR_END.
                    break;

                //Decreases HP by [VAL] every [VAL]. Also, increases the cooldown for your close-range skills by [VAL] while in effect.
                case 800920: //Decreases HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Also, increases the cooldown for your close-range skills by $H_W_BAD$value$COLOR_END while in effect.
                    break;

                //Decreases HP by [VAL] every [VAL]. Kills the target on the spot when the duration runs out.
                case 99004900: //Decreases HP by $H_W_BAD30%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. Kills the target on the spot when the duration runs out.
                    break;

                //Decreases HP by [VAL] of total HP every [VAL].
                case 47640400: //Decreases HP by $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Decreases HP by [VAL] when debuff ends.
                case 301002: //Decreases HP by $H_W_BAD$value$COLOR_END when debuff ends.
                case 301020: //Decreases HP by $H_W_BAD$value$COLOR_END when debuff ends.
                    break;

                //Decreases maximum HP by [VAL] and endurance by [VAL]. Also, decreases Power by [VAL].
                case 47621800: //Decreases maximum HP by $H_W_BAD$value$COLOR_END and endurance by $H_W_BAD$value2$COLOR_END. Also, decreases Power by $H_W_BAD$value3$COLOR_END.
                    break;

                //Decreases movement speed and Power by [VAL] and H_W_BAD$value3$COLOR_END, respectively.
                case 476211: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                case 476212: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                case 476213: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                case 476214: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                case 476215: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                case 47621100: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively.
                    break;

                //Decreases movement speed and Power by [VAL] and H_W_BAD$value3$COLOR_END, respectively. Orcan Sloth cannot be removed.
                case 476216: //Decreases movement speed and Power by $H_W_BAD$value$COLOR_END and H_W_BAD$value3$COLOR_END, respectively. Orcan Sloth cannot be removed.
                    break;

                //Decreases movement speed by $value3, but increases Power by $value. Increases skill damage by $value2 in case of an additional attack on other PCs. While the pose is kept, MP decreases by $value4 every $tickInterval4.
                case 601110: //Decreases movement speed by $value3, but increases Power by $value. Increases skill damage by $value2 in case of an additional attack on other PCs. While the pose is kept, MP decreases by $value4 every $tickInterval4.
                    break;

                //Decreases movement speed by [VAL], but enemies will not attack unless you attack them. Effect ends if you use another skill or item, or enter combat.
                case 90500: //Decreases movement speed by $H_W_BAD$value2$COLOR_END, but enemies will not attack unless you attack them. Effect ends if you use another skill or item, or enter combat.
                    break;

                //Decreases movement speed by [VAL], cannot use all skills while in effect.
                case 47600500: //Decreases movement speed by $H_W_BAD$value$COLOR_END, cannot use all skills while in effect.
                    break;

                //Decreases movement speed by [VAL], decreases HP by [VAL] every [VAL].
                case 47622300: //Decreases movement speed by $H_W_BAD$value$COLOR_END, decreases HP by $H_W_BAD0.1%$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END.
                    break;

                //Decreases movement speed by [VAL], increases skill cooldown by [VAL]. Malaise of Despair cannot be removed.
                case 47500700: //Decreases movement speed by $H_W_BAD$value2$COLOR_END, increases skill cooldown by $H_W_BAD$value$COLOR_END.$BRMalaise of Despair cannot be removed.
                    break;

                //Decreases movement speed by [VAL].
                case 22030: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 22090: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 25120: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 26010: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 26210: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 27010: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 28070: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 601300: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                case 47622000: //Decreases movement speed by $H_W_BAD$value3$COLOR_END.
                case 49300100: //Decreases movement speed by $H_W_BAD$value$COLOR_END.
                    break;

                //Decreases movement speed by [VAL]. Loses [VAL] of current HP when the countdown runs out.
                case 49300200: //Decreases movement speed by $H_W_BAD$value2$COLOR_END.$BRLoses $H_W_BAD$value$COLOR_END of current HP when the countdown runs out.
                    break;

                //Decreases movement speed by [VAL]. The effect stacks up to 4 times.
                case 501400: //Decreases movement speed by $H_W_BAD$value$COLOR_END. The effect stacks up to 4 times.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Arun's Tears.
                case 27050: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Arun's Tears.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Arun's Vitae.
                case 27040: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Arun's Vitae.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Dash.
                case 24030: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Dash.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Flame Pillar.
                case 25040: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Flame Pillar.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Flatten.
                case 24020: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Flatten.
                case 24050: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Flatten.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Focus Heal.
                case 28040: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Focus Heal.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Leaping Strike.
                case 21030: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Leaping Strike.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Poison Arrow.
                case 26050: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Poison Arrow.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Poison Blade.
                case 21050: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Poison Blade.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Rain of Blows.
                case 21020: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Rain of Blows.
                case 21060: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Rain of Blows.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Shield Counter.
                case 22020: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Shield Counter.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Volley of Curses.
                case 27030: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Volley of Curses.
                    break;

                //Decreases MP consumption for the next chain skill by [VAL]: Web Arrow.
                case 26040: //Decreases MP consumption for the next chain skill by $H_W_GOOD$value$COLOR_END: Web Arrow.
                    break;

                //Decreases Power by [VAL], attack speed by [VAL], and movement speed by [VAL]. The Sap of Dagon cannot be removed.
                case 47670400: //Decreases Power by $H_W_BAD$value$COLOR_END, attack speed by $H_W_BAD$value2$COLOR_END, and movement speed by $H_W_BAD$value3$COLOR_END. The Sap of Dagon cannot be removed.
                    break;

                //Decreases Power by [VAL], endurance by [VAL], and movement speed by [VAL]. Cannot be purified or cleansed.
                case 1800: //Decreases Power by $H_W_BAD$value$COLOR_END, endurance by $H_W_BAD$value2$COLOR_END, and movement speed by $H_W_BAD$value3$COLOR_END.$BRCannot be purified or cleansed.
                    break;

                //Decreases power by [VAL]. Effect persists even after death.
                case 999995: //Decreases power by $H_W_BAD$value$COLOR_END. Effect persists even after death.
                    break;

                //Decreases resistance to all debuffs by [VAL].
                case 701603: //Decreases resistance to all debuffs by $H_W_BAD$value$COLOR_END.
                    break;

                //Decreases resistance to knockdown/immobilization. Ends when knocked down/immobilized.
                case 47612800: //Decreases resistance to knockdown/immobilization. Ends when knocked down/immobilized.
                    break;

                //Decreases skill damage by [VAL]. Effect continues as long as you're near a Harbinger of Futility.
                case 475151: //Decreases skill damage by $H_W_BAD$value$COLOR_END. Effect continues as long as you're near a Harbinger of Futility.
                    break;

                //Decreases the cooldown of all skills by [VAL] every time it is stacked but decreases HP by [VAL] of total HP every [VAL]. Dimensional Time Distortion can be stacked up to 5 times and cannot be removed.
                case 47681100: //Decreases the cooldown of all skills by $H_W_GOOD$value$COLOR_END every time it is stacked but decreases HP by $H_W_BAD0.8%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END. Dimensional Time Distortion can be stacked up to 5 times and cannot be removed.
                    break;

                //Decreases the MP of party members within 2m, excluding you. The Purification of Dagon cannot be removed.
                case 47670500: //Decreases the MP of party members within 2m, excluding you. The Purification of Dagon cannot be removed.
                    break;

                //Deserters cannot enter a Battleground.
                case 999998: //Deserters cannot enter a Battleground.
                    break;

                //Divine blessing makes you 100% resistant to any harmful effect. Divine blessing cannot be removed.
                case 47670700: //Divine blessing makes you 100% resistant to any harmful effect. Divine blessing cannot be removed.
                    break;

                //Divine powers are weakening the evil magic! Movement speed reduced by [VAL].
                case 4720: //Divine powers are weakening the evil magic! Movement speed reduced by $H_W_BAD$value$COLOR_END.
                    break;

                //Dizziness decreases movement speed by [VAL].
                case 47641400: //Dizziness decreases movement speed by $H_W_BAD$value$COLOR_END.
                    break;

                //Do [VAL] more damage against other players. Effect persists through death.
                case 1350: //Do $H_W_GOOD$value$COLOR_END more damage against other players.$BREffect persists through death.
                    break;

                //Do not gain aggro while in effect.
                case 15150: //Do not gain aggro while in effect.
                    break;

                //Don't worry. Be happy! Blow something up!
                case 4400: //Don't worry. Be happy! Blow something up!
                    break;

                //Drains your Power and endurance by [VAL]. Plague persists through death, and can be cured only by Holy Potion of Protection.
                case 99002900: //Drains your Power and endurance by $H_W_BAD$value$COLOR_END. Plague persists through death, and can be cured only by Holy Potion of Protection.
                    break;

                //Due to bursting rage, Power increases by [VAL], but endurance decreases by [VAL].
                case 4767: //Due to bursting rage, Power increases by $H_W_GOOD$value$COLOR_END, but endurance decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Due to the heart beating caused by the letter, stamina does not decrease while the effect lasts.
                case 1132: //Due to the heart beating caused by the letter, stamina does not decrease while the effect lasts.
                    break;

                //Electricity recharges. Increases Power by [VAL]. Electricity Recharge can be stacked up to 4 times.
                case 47661100: //Electricity recharges. Increases Power by $H_W_GOOD$value$COLOR_END. Electricity Recharge can be stacked up to 4 times.
                    break;

                //Electricity recharges. Increases Power by [VAL]. Electricity Recharge can be stacked up to 4 times. Electricity Recharge First Stage cannot be removed.
                case 476611: //Electricity recharges. Increases Power by $H_W_GOOD$value$COLOR_END. Electricity Recharge can be stacked up to 4 times. Electricity Recharge First Stage cannot be removed.
                    break;

                //Electricity recharges. Increases Power by [VAL]. Electricity Recharge can be stacked up to 4 times. Electricity Recharge Second Stage cannot be removed.
                case 476612: //Electricity recharges. Increases Power by $H_W_GOOD$value$COLOR_END. Electricity Recharge can be stacked up to 4 times. Electricity Recharge Second Stage cannot be removed.
                    break;

                //Electricity recharges. Increases Power by [VAL]. Electricity Recharge can be stacked up to 4 times. Electricity Recharge Third Stage cannot be removed.
                case 476613: //Electricity recharges. Increases Power by $H_W_GOOD$value$COLOR_END. Electricity Recharge can be stacked up to 4 times. Electricity Recharge Third Stage cannot be removed.
                    break;

                //Endurance decreases by [VAL] (cumulative up to 3 hits).
                case 200300: //Endurance decreases by $H_W_BAD$value$COLOR_END (cumulative up to 3 hits).
                case 200301: //Endurance decreases by $H_W_BAD$value$COLOR_END (cumulative up to 3 hits).
                    break;

                //Endurance decreases by [VAL].
                case 99000700: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000710: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000720: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000730: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000740: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000750: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000760: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000770: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                case 99000780: //Endurance decreases by $H_W_BAD$value$COLOR_END.
                    break;

                //Endurance decreases by [VAL]. The effect stacks up to 13 times.
                case 101300: //Endurance decreases by $H_W_BAD0.85%$COLOR_END. The effect stacks up to 13 times.
                    break;

                //Endurance increases by  [VAL].
                case 47612400: //Endurance increases by  $H_W_GOOD$value$COLOR_END.
                    break;

                //Endurance increases by [VAL] while within 20m of the caster.
                case 200230: //Endurance increases by $H_W_GOOD$value$COLOR_END while within 20m of the caster.
                case 200231: //Endurance increases by $H_W_GOOD$value$COLOR_END while within 20m of the caster.
                    break;

                //Endurance increases by [VAL].
                case 1300: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 800500: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 800501: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000200: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000210: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000220: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000230: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000240: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000250: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000260: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99000270: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                case 99001600: //Endurance increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Essence of a fast cat in a drinkable form! Increases movement speed while not in combat by [VAL].
                case 4761: //Essence of a fast cat in a drinkable form! Increases movement speed while not in combat by $H_W_BAD$value$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Extremely old jerky restores MP by [VAL]. While the effect lasts, you remain stunned and cannot move.
                case 4763: //Extremely old jerky restores MP by $H_W_GOOD10684$COLOR_END. While the effect lasts, you remain stunned and cannot move.
                    break;

                //Flee in terror from Thulsa! Panic cannot be removed. Ends if target is knocked down.
                case 47610300: //Flee in terror from Thulsa! Panic cannot be removed. Ends if target is knocked down.
                    break;

                //Flee in terror! Cowardice cannot be removed. Ends when knocked down.
                case 47611500: //Flee in terror! Cowardice cannot be removed. Ends when knocked down.
                    break;

                //Flee in terror! Horde's Howl cannot be removed but ends when knocked down.
                case 47621300: //Flee in terror! Horde's Howl cannot be removed but ends when knocked down.
                    break;

                //Flee in terror! Killian's terror cannot be removed. Effect ends when knocked down.
                case 47600600: //Flee in terror! Killian's terror cannot be removed. Effect ends when knocked down.
                    break;

                //Flee in terror! Panic cannot be removed. Ends if target is knocked down.
                case 476111: //Flee in terror! Panic cannot be removed. Ends if target is knocked down.
                    break;

                //Flee with pain! Also loses [VAL] of total HP every [VAL]. Curse of pain cannot be removed. Effect ends when knocked down.
                case 47600400: //Flee with pain! Also loses $H_W_BAD8%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.$BRCurse of pain cannot be removed. Effect ends when knocked down.
                    break;

                //Flees and loses [VAL] of total HP every [VAL].
                case 47300400: //Flees and loses $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //For AI verification
                case 47662110: //For AI verification
                    break;

                //Full electricity recharge complete. Increases Power by [VAL] and attack speed by [VAL]. Full Recharge cannot be removed.
                case 476614: //Full electricity recharge complete. Increases Power by $H_W_GOOD$value$COLOR_END and attack speed by $H_W_GOOD$value2$COLOR_END. Full Recharge cannot be removed.
                    break;

                //Great vigor increases Power by [VAL]. The Vigor of the Comrade cannot be removed.
                case 47670300: //Great vigor increases Power by $H_W_GOOD$value$COLOR_END. The Vigor of the Comrade cannot be removed.
                    break;

                //Greatly reduce damage from other players, and chances of being immobilized or knocked down.
                case 999996: //Greatly reduce damage from other players, and chances of being immobilized or knocked down.
                    break;

                //Heals [VAL] HP every [VAL].
                case 1440: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1441: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1442: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1443: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1444: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1445: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1450: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1451: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1452: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1453: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1454: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1455: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10700: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10701: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10702: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10703: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10704: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10705: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10710: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10711: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10712: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10713: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10714: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10715: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10720: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10721: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10722: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10723: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10724: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10725: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10800: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10801: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10802: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10803: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10804: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10805: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701500: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701501: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701502: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701503: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701504: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701505: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701506: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701507: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701508: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701509: //Heals $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99004510: //Heals $H_W_GOOD1312$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of HP and restores [VAL] of MP every [VAL].
                case 2560: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2561: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2562: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2563: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2564: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2565: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2566: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2567: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2575: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2576: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2577: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2578: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2579: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2580: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2581: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2582: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2650: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2651: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2652: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2653: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2654: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2655: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2656: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2657: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2665: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2666: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2667: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2668: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2669: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2670: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2671: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2672: //Heals $H_W_GOOD$value$COLOR_END of HP and restores $H_W_GOOD$value2$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    Add(abnormal, new EfRegenMp(), 1);
                    break;

                //Heals [VAL] of HP every [VAL].
                case 2500: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2501: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2502: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2503: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2504: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2505: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2506: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2507: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2515: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2516: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2517: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2518: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2519: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2520: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2521: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2522: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2590: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2591: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2592: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2593: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2594: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2595: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2596: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2597: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2605: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2606: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2607: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2608: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2609: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2610: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2611: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2612: //Heals $H_W_GOOD$value$COLOR_END of HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP every [VAL].
                case 1150: //Heals $H_W_GOOD1%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1460: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1461: //Heals $H_W_GOOD4%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1462: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1490: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1491: //Heals $H_W_GOOD6%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 1610: //Heals $H_W_GOOD2%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 90101: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801100: //Heals $H_W_GOOD1.8%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 47612500: //Heals $H_W_GOOD3%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000300: //Heals $H_W_GOOD3%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000310: //Heals $H_W_GOOD4%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000320: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000330: //Heals $H_W_GOOD6%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000340: //Heals $H_W_GOOD7%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000350: //Heals $H_W_GOOD8%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000360: //Heals $H_W_GOOD9%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 99000370: //Heals $H_W_GOOD10%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP every [VAL]. Ends when you enter combat.
                case 26140: //Heals $H_W_GOOD2%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Ends when you enter combat.
                case 26141: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Ends when you enter combat.
                case 28100: //Heals $H_W_GOOD5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Ends when you enter combat.
                case 28110: //Heals $H_W_GOOD15%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Ends when you enter combat.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP every [VAL]. Heals total of 25% HP.
                case 18501: //Heals $H_W_GOOD2.5%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Heals total of 25% HP.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP every [VAL]. Heals total of 30% HP.
                case 18502: //Heals $H_W_GOOD3%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END. Heals total of 30% HP.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP for the next chain skill: Poison Blade.
                case 21140: //Heals $H_W_GOOD$value$COLOR_END of total HP for the next chain skill: Poison Blade.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP for the next chain skill: Stunning Backhand.
                case 23090: //Heals $H_W_GOOD$value$COLOR_END of total HP for the next chain skill: Stunning Backhand.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP per stack every [VAL]. Effect stacks up to three times.
                case 99003600: //Heals $H_W_GOOD0.5%$COLOR_END of total HP per stack every $H_W_GOOD$tickInterval$COLOR_END. Effect stacks up to three times.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals [VAL] of total HP.
                case 1630: //Heals $H_W_GOOD25%$COLOR_END of total HP.
                case 2020: //Heals $H_W_GOOD35%$COLOR_END of total HP.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals 20% of total HP, [VAL] every [VAL].
                case 18500: //Heals 20% of total HP, $H_W_GOOD2%$COLOR_END every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Heals additional HP by [VAL] when receiving HP recovering effect.
                case 2875: //Heals additional HP by $H_W_GOOD$value$COLOR_END when receiving HP recovering effect.
                case 2876: //Heals additional HP by $H_W_GOOD$value$COLOR_END when receiving HP recovering effect.
                case 2877: //Heals additional HP by $H_W_GOOD$value$COLOR_END when receiving HP recovering effect.
                    break;

                //Heals HP every [VAL].
                case 700400: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700401: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700402: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700403: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700404: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700405: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700406: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 700407: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800100: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800101: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800102: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800103: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800104: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800105: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800106: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800107: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800600: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800601: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800602: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800603: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 800604: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801400: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801401: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801402: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801403: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801404: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801405: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801406: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801407: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801408: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801409: //Heals HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //HP decreases by [VAL] every [VAL].
                case 10100: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10101: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10102: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10103: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10104: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10105: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10110: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10111: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10112: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10113: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10114: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10115: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10120: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10121: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10122: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10123: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10124: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 10125: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501000: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501001: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501002: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501020: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501021: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                case 501022: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END.
                    Add(abnormal, new EfLoseHp(), 0);
                    break;

                //HP decreases by [VAL] every [VAL]. This effect can be stacked up to 3 times.
                case 600900: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600901: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600902: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600903: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600904: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600905: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600906: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600920: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600921: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600922: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600923: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600924: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600925: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                case 600926: //HP decreases by $H_W_BAD$value$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. This effect can be stacked up to 3 times.
                    break;

                //HP drops to [VAL] at end of duration. Fatecutter cannot be removed.
                case 47613000: //HP drops to $H_W_GOOD$value$COLOR_END at end of duration. Fatecutter cannot be removed.
                    break;

                //HP recover amount decreases by [VAL] when receive HP recovering effect.
                case 99003100: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003110: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003120: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003130: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003140: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003150: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003160: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                case 99003170: //HP recover amount decreases by $H_W_BAD$value$COLOR_END when receive HP recovering effect.
                    break;

                //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18700: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18710: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18720: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18730: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18740: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 18750: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                case 47662400: //If this tooltip displays, it means the wrong debuff is selected! BTS required!
                    break;

                //If you die while under this effect, you can resurrect on the spot.
                case 801700: //If you die while under this effect, you can resurrect on the spot.
                    break;

                //Ignored by opponents. Duration: [VAL].
                case 600100: //Ignored by opponents. Duration: $H_W_GOOD$time$COLOR_END.
                    break;

                //Immediately heals [VAL] of total HP.
                case 47650400: //Immediately heals $H_W_GOOD50%$COLOR_END of total HP.
                    break;

                //Immobilizes while in combat and decreases out-of-combat movement speed.
                case 15120: //Immobilizes while in combat and decreases out-of-combat movement speed.
                    break;

                //Immune to Akasha's Withering. Effect persists through death.
                case 4730: //Immune to Akasha's Withering.$BREffect persists through death.
                    break;

                //Immune to all harmful effects.
                case 47650700: //Immune to all harmful effects.
                    break;

                //Immune to all of Kelsaik's effects. Cannot be knocked down or stunned.
                case 47500250: //Immune to all of Kelsaik's effects. Cannot be knocked down or stunned.
                    break;

                //Immune to knockdown and immobilization.
                case 47621600: //Immune to knockdown and immobilization.
                    break;

                //Immune to knockdown, immobilization, and all harmful effects.
                case 47640200: //Immune to knockdown, immobilization, and all harmful effects.
                    break;

                //In 10 sec. you will faint.
                case 47622200: //In 10 sec. you will faint.
                    break;

                //Increase endurance by [VAL].
                case 28030: //Increase endurance by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increase endurance, balance, and resistances by [VAL].
                case 2040: //Increase endurance, balance, and resistances by $H_W_GOOD$value$COLOR_END.
                case 2041: //Increase endurance, balance, and resistances by $H_W_GOOD$value$COLOR_END.
                case 2042: //Increase endurance, balance, and resistances by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increase movement speed by [VAL] while in combat.
                case 26200: //Increase movement speed by $H_W_GOOD$value$COLOR_END while in combat.
                case 27110: //Increase movement speed by $H_W_GOOD$value$COLOR_END while in combat.
                case 28050: //Increase movement speed by $H_W_GOOD$value$COLOR_END while in combat.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Increase power by [VAL], attack speed by [VAL].
                case 801500: //Increase power by $H_W_GOOD$value$COLOR_END, attack speed by $H_W_GOOD$value2$COLOR_END.
                case 801501: //Increase power by $H_W_GOOD$value$COLOR_END, attack speed by $H_W_GOOD$value2$COLOR_END.
                case 801502: //Increase power by $H_W_GOOD$value$COLOR_END, attack speed by $H_W_GOOD$value2$COLOR_END.
                    break;

                //Increase resistances by [VAL].
                case 2030: //Increase resistances by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increased Power and endurance by [VAL] and [VAL], respectively. Attack speed increased by [VAL].
                case 47641000: //Increased Power and endurance by $H_W_BAD$value$COLOR_END and $H_W_BAD$value2$COLOR_END, respectively. Attack speed increased by $H_W_BAD$value3$COLOR_END.
                    break;

                //Increases aggro by [VAL]. Prominence cannot be removed.
                case 47611600: //Increases aggro by $H_W_GOOD$value$COLOR_END. Prominence cannot be removed.
                    break;

                //Increases amount of HP recover by [VAL].
                case 22050: //Increases amount of HP recover by $H_W_GOOD$value$COLOR_END.
                case 22070: //Increases amount of HP recover by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases attack speed by $value.
                case 501200: //Increases attack speed by $value.
                    break;

                //Increases attack speed by [VAL] and defense by [VAL]. Increases reaction resistance by [VAL] and aggression for attack skills by [VAL].
                case 4800: //Increases attack speed by $H_W_GOOD$value$COLOR_END and defense by $H_W_GOOD10%$COLOR_END. Increases reaction resistance by $H_W_GOOD30%$COLOR_END and aggression for attack skills by $H_W_GOOD$value4$COLOR_END.
                case 4810: //Increases attack speed by $H_W_GOOD$value$COLOR_END and defense by $H_W_GOOD5%$COLOR_END. Increases reaction resistance by $H_W_GOOD20%$COLOR_END and aggression for attack skills by $H_W_GOOD$value4$COLOR_END.
                case 4820: //Increases attack speed by $H_W_GOOD$value$COLOR_END and defense by $H_W_GOOD3%$COLOR_END. Increases reaction resistance by $H_W_GOOD10%$COLOR_END and aggression for attack skills by $H_W_GOOD$value4$COLOR_END.
                    break;

                //Increases attack speed by [VAL] and increases Power and endurance by [VAL]. Also, decreases the cooldown by [VAL].
                case 4852: //Increases attack speed by $H_W_GOOD$value$COLOR_END and increases Power and endurance by $H_W_GOOD100 times$COLOR_END. Also, decreases the cooldown by $H_W_GOOD$value4$COLOR_END.
                    break;

                //Increases attack speed by [VAL] every time it is stacked but decreases HP by [VAL] of total HP every [VAL]. Dimensional Wind can be stacked up to 5 times and cannot be removed.
                case 47681200: //Increases attack speed by $H_W_GOOD$value$COLOR_END every time it is stacked but decreases HP by $H_W_BAD0.8%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END. Dimensional Wind can be stacked up to 5 times and cannot be removed.
                    break;

                //Increases attack speed by [VAL], attack by [VAL], and defense by [VAL].
                case 4801: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD10%$COLOR_END, and defense by $H_W_GOOD10%$COLOR_END.
                case 4802: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD10%$COLOR_END, and defense by $H_W_GOOD10%$COLOR_END.
                case 4811: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD5%$COLOR_END, and defense by $H_W_GOOD5%$COLOR_END.
                case 4812: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD5%$COLOR_END, and defense by $H_W_GOOD5%$COLOR_END.
                case 4821: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD3%$COLOR_END, and defense by $H_W_GOOD3%$COLOR_END.
                case 4822: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD3%$COLOR_END, and defense by $H_W_GOOD3%$COLOR_END.
                case 4830: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD10%$COLOR_END, and defense by $H_W_GOOD10%$COLOR_END.
                case 4831: //Increases attack speed by $H_W_GOOD$value$COLOR_END, attack by $H_W_GOOD10%$COLOR_END, and defense by $H_W_GOOD10%$COLOR_END.
                    break;

                //Increases attack speed by [VAL].
                case 701602: //Increases attack speed by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases attack speed by [VAL]. This effect can be stacked up to 3 times.
                case 47662120: //Increases attack speed by $H_W_GOOD$value$COLOR_END. This effect can be stacked up to 3 times.
                    break;

                //Increases balance by [VAL].
                case 23110: //Increases balance by $H_W_GOOD1.5times$COLOR_END.
                case 23111: //Increases balance by $H_W_GOOD2times$COLOR_END.
                case 24110: //Increases balance by $H_W_GOOD1.5times$COLOR_END.
                case 24111: //Increases balance by $H_W_GOOD2times$COLOR_END.
                case 27130: //Increases balance by $H_W_GOOD5times$COLOR_END.
                case 28120: //Increases balance by $H_W_GOOD5times$COLOR_END.
                case 701600: //Increases balance by $H_W_GOOD$value$COLOR_END.
                case 801300: //Increases balance by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases cooldown time of all skills you use while in effect by [VAL].
                case 701200: //Increases cooldown time of all skills you use while in effect by $H_W_BAD$value$COLOR_END.
                case 701201: //Increases cooldown time of all skills you use while in effect by $H_W_BAD$value$COLOR_END.
                case 701300: //Increases cooldown time of all skills you use while in effect by $H_W_BAD$value$COLOR_END.
                    break;

                //Increases crit chance by [VAL] and critical hit damage by [VAL]. Also, decreases resistance to knock down and immobilization by [VAL] and resistance to harmful effect by [VAL].
                case 47620200: //Increases crit chance by $H_W_GOOD$value$COLOR_END and critical hit damage by $H_W_GOOD$value2$COLOR_END. Also, decreases resistance to knock down and immobilization by $H_W_BAD$value3$COLOR_END and resistance to harmful effect by $H_W_BAD$value4$COLOR_END.
                    break;

                //Increases crit chance by [VAL].
                case 22120: //Increases crit chance by $H_W_GOOD2times$COLOR_END.
                    break;

                //Increases crit chance dramatically. Sure Strike cannot be removed.
                case 47612900: //Increases crit chance dramatically.$BRSure Strike cannot be removed.
                    break;

                //Increases crit chance for the next attack by [VAL].
                case 23140: //Increases crit chance for the next attack by $H_W_GOOD2times$COLOR_END.
                case 23160: //Increases crit chance for the next attack by $H_W_GOOD3times$COLOR_END.
                case 23170: //Increases crit chance for the next attack by $H_W_GOOD3times$COLOR_END.
                case 24160: //Increases crit chance for the next attack by $H_W_GOOD2times$COLOR_END.
                case 25170: //Increases crit chance for the next attack by $H_W_GOOD2times$COLOR_END.
                    break;

                //Increases endurance by  [VAL], decreases Power by [VAL].
                case 476217: //Increases endurance by  $H_W_GOOD$value$COLOR_END, decreases Power by $H_W_BAD$value2$COLOR_END.
                case 476218: //Increases endurance by  $H_W_GOOD$value$COLOR_END, decreases Power by $H_W_BAD$value2$COLOR_END.
                case 476219: //Increases endurance by  $H_W_GOOD$value$COLOR_END, decreases Power by $H_W_BAD$value2$COLOR_END.
                case 47621400: //Increases endurance by  $H_W_GOOD$value$COLOR_END, decreases Power by $H_W_BAD$value2$COLOR_END.
                    break;

                //Increases endurance by  [VAL], decreases Power by [VAL]. Double-edged Parry V cannot be removed.
                case 476220: //Increases endurance by  $H_W_GOOD$value$COLOR_END, decreases Power by $H_W_BAD$value2$COLOR_END. Double-edged Parry V cannot be removed.
                    break;

                //Increases endurance by [VAL].
                case 15020: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 21160: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 22110: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 23050: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 24140: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 25020: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                case 27120: //Increases endurance by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases endurance by [VAL]. and grants immunity to knockdown, immobilization, and harmful effects.
                case 47640300: //Increases endurance by $H_W_GOOD$value$COLOR_END. and grants immunity to knockdown, immobilization, and harmful effects.
                    break;

                //Increases endurance by [VAL]. Kelsaik's Protection cannot be removed.
                case 47502200: //Increases endurance by $H_W_GOOD4times$COLOR_END. Kelsaik's Protection cannot be removed.
                    break;

                //Increases endurance by [VAL]. The effect stacks up to 10 times.
                case 47620900: //Increases endurance by $H_W_GOOD$value$COLOR_END. The effect stacks up to 10 times.
                    break;

                //Increases endurance by [VAL]. You gain [VAL] more aggro from all attacking skills. Costs [VAL] MP per [VAL] while active.
                case 100200: //Increases endurance by $H_W_GOOD$value$COLOR_END. You gain $H_W_GOOD$value2$COLOR_END more aggro from all attacking skills. Costs $H_W_BAD$value3$COLOR_END MP per $H_W_BAD$tickInterval3$COLOR_END while active.
                case 100201: //Increases endurance by $H_W_GOOD$value$COLOR_END. You gain $H_W_GOOD$value2$COLOR_END more aggro from all attacking skills. Costs $H_W_BAD$value3$COLOR_END MP per $H_W_BAD$tickInterval3$COLOR_END while active.
                    break;

                //Increases gathering proficiency by [VAL]. Effect stacks up to four times.
                case 99003400: //Increases gathering proficiency by $H_W_GOOD$value$COLOR_END. Effect stacks up to four times.
                    break;

                //Increases knockdown and immobilization rates by [VAL].
                case 47620700: //Increases knockdown and immobilization rates by $H_W_BAD$value$COLOR_END.
                case 47620800: //Increases knockdown and immobilization rates by $H_W_BAD$value$COLOR_END.
                    break;

                //Increases knockdown by [VAL].
                case 24080: //Increases knockdown by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases maximum HP by [VAL], recovers HP by [VAL] every [VAL]. Also increases endurance and Power by [VAL] and [VAL], respectively.
                case 47621700: //Increases maximum HP by $H_W_GOOD$value$COLOR_END, recovers HP by $H_W_GOOD$value2$COLOR_END every $H_W_GOOD$tickInterval2$COLOR_END. Also increases endurance and Power by $H_W_GOOD$value3$COLOR_END and $H_W_GOOD$value4$COLOR_END, respectively.
                    break;

                //Increases maximum HP by [VAL].
                case 49400100: //Increases maximum HP by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases maximum MP by [VAL].
                case 49400200: //Increases maximum MP by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases maximum Stamina by [VAL]. Effect stacks up to three times.
                case 99003500: //Increases maximum Stamina by $H_W_GOOD$value$COLOR_END. Effect stacks up to three times.
                    break;

                //Increases movement speed in  [VAL]. Entering combat cancels the effect.
                case 49400400: //Increases movement speed in  $H_W_GOOD240$COLOR_END. Entering combat cancels the effect.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Increases movement speed while in combat by [VAL].
                case 22080: //Increases movement speed while in combat by $H_W_GOOD$value$COLOR_END.
                case 24090: //Increases movement speed while in combat by $H_W_GOOD$value$COLOR_END.
                case 26080: //Increases movement speed while in combat by $H_W_GOOD$value$COLOR_END.
                case 26120: //Increases movement speed while in combat by $H_W_GOOD$value$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Increases moving speed in combat by [VAL]. Effect stacks up to three times.
                case 99003200: //Increases moving speed in combat by $H_W_GOOD$value$COLOR_END. Effect stacks up to three times.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Increases Power and attack speed by [VAL] and [VAL], respectively, but HP decreases [VAL] every [VAL]. The effect stacks up to 99 times. Tuwangi Blood-Haze cannot be removed.
                case 47630100: //Increases Power and attack speed by $H_W_GOOD$value$COLOR_END and $H_W_GOOD$value2$COLOR_END, respectively, but HP decreases $H_W_BAD0.2%$COLOR_END every $H_W_BAD$tickInterval3$COLOR_END. The effect stacks up to 99 times. Tuwangi Blood-Haze cannot be removed.
                    break;

                //Increases Power and endurance by [VAL] and [VAL], respectively. Also, any HP recovery effects on you increase by [VAL].
                case 47621900: //Increases Power and endurance by $H_W_BAD$value1$COLOR_END and $H_W_BAD$value2$COLOR_END, respectively. Also, any HP recovery effects on you increase by $H_W_BAD$value3$COLOR_END.
                    break;

                //Increases Power and endurance by [VAL] every time it is stacked. Will of the Black Warp cannot be removed and can be stacked up to 10 times.
                case 47680500: //Increases Power and endurance by $H_W_GOOD$value$COLOR_END every time it is stacked. Will of the Black Warp cannot be removed and can be stacked up to 10 times.
                    break;

                //Increases Power by [VAL] (up to [VAL]) every time you jump because of Dimensional Vigor.
                case 476815: //Increases Power by $H_W_GOOD$value$COLOR_END (up to $H_W_GOOD75%$COLOR_END) every time you jump because of Dimensional Vigor.
                    break;

                //Increases Power by [VAL] (up to [VAL]) every time you jump but decreases HP by [VAL] every [VAL]. Dimensional Vigor can be stacked up to 5 times and cannot be removed.
                case 47681500: //Increases Power by $H_W_GOOD15%$COLOR_END (up to $H_W_GOOD75%$COLOR_END) every time you jump but decreases HP by $H_W_BAD0.8%$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END. Dimensional Vigor can be stacked up to 5 times and cannot be removed.
                    break;

                //Increases power by [VAL] and attack speed by [VAL].
                case 47630300: //Increases power by $H_W_GOOD1000%$COLOR_END and attack speed by $H_W_GOOD200%$COLOR_END.
                    break;

                //Increases Power by [VAL] every time it is stacked but decreases HP by [VAL] every [VAL]. It can be stacked up to 5 times, but when it is stacked 5 times, you are forced to dance and the stacking resets. Dimensional Group Dance cannot be removed.
                case 47681400: //Increases Power by $H_W_GOOD$value2$COLOR_END every time it is stacked but decreases HP by $H_W_BAD0.8%$COLOR_END every $H_W_BAD$tickInterval3$COLOR_END. It can be stacked up to 5 times, but when it is stacked 5 times, you are forced to dance and the stacking resets. Dimensional Group Dance cannot be removed.
                    break;

                //Increases Power by [VAL] every time it is stacked but decreases HP by [VAL] of total HP every [VAL]. Dimensional Insanity can be stacked up to 5 times and cannot be removed.
                case 47681000: //Increases Power by $H_W_GOOD$value$COLOR_END every time it is stacked but decreases HP by $H_W_BAD0.8%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END. Dimensional Insanity can be stacked up to 5 times and cannot be removed.
                    break;

                //Increases power by [VAL] for Final Reprisal.
                case 28010: //Increases power by $H_W_GOOD$value$COLOR_END for Final Reprisal.
                    break;

                //Increases Power by [VAL], and crit chance by [VAL]. However, decreases endurance by [VAL]. Costs [VAL] MP per [VAL] while active.
                case 100100: //Increases Power by $H_W_GOOD$value$COLOR_END, and crit chance by $H_W_GOOD$value3$COLOR_END. However, decreases endurance by $H_W_BAD$value2$COLOR_END. Costs $H_W_BAD$value4$COLOR_END MP per $H_W_BAD$tickInterval4$COLOR_END while active.
                case 100101: //Increases Power by $H_W_GOOD$value$COLOR_END, and crit chance by $H_W_GOOD$value3$COLOR_END. However, decreases endurance by $H_W_BAD$value2$COLOR_END. Costs $H_W_BAD$value4$COLOR_END MP per $H_W_BAD$tickInterval4$COLOR_END while active.
                    break;

                //Increases power by [VAL], and critical damage by [VAL]. Effect stacks up to 10 times.
                case 47611300: //Increases power by $H_W_GOOD$value$COLOR_END, and critical damage by $H_W_GOOD+0.1$COLOR_END. Effect stacks up to 10 times.
                    break;

                //Increases Power by [VAL], but decreases resistance to knockdown and immobilization by [VAL] and decreases resistance to harmful effects by [VAL].
                case 47620100: //Increases Power by $H_W_GOOD$value$COLOR_END, but decreases resistance to knockdown and immobilization by $H_W_BAD$value2$COLOR_END and decreases resistance to harmful effects by $H_W_BAD$value3$COLOR_END.
                    break;

                //Increases power by [VAL], but loses [VAL] of total HP every [VAL]. The effect stacks up to 99 times.
                case 47501700: //Increases power by $H_W_GOOD$value$COLOR_END, but loses $H_W_BAD2%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END. The effect stacks up to 99 times.
                    break;

                //Increases Power by [VAL], but loses [VAL] of total HP every [VAL]. The effect stacks up to 99 times.
                case 47501300: //Increases Power by $H_W_GOOD$value$COLOR_END, but loses $H_W_BAD0.5%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END. The effect stacks up to 99 times.
                    break;

                //Increases power by [VAL], increases endurance by [VAL], restores [VAL] HP every [VAL].
                case 47610100: //Increases power by $H_W_GOOD$value$COLOR_END, increases endurance by $H_W_GOOD$value2$COLOR_END, restores $H_W_GOOD1%$COLOR_END HP every $H_W_GOOD$tickInterval3$COLOR_END.
                    break;

                //Increases power by [VAL].
                case 47640100: //Increases power by $H_W_GOOD$value$COLOR_END.
                case 47640500: //Increases power by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases Power by [VAL].
                case 99004700: //Increases Power by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases power by [VAL]. Can be amassed 99 times.
                case 47502100: //Increases power by $H_W_GOOD$value$COLOR_END. Can be amassed 99 times.
                    break;

                //Increases Power by [VAL]. Can be amassed 99 times.
                case 49400800: //Increases Power by $H_W_GOOD$value$COLOR_END. Can be amassed 99 times.
                    break;

                //Increases Power by [VAL]. The effect stacks up to 10 times.
                case 47621000: //Increases Power by $H_W_GOOD$value$COLOR_END. The effect stacks up to 10 times.
                    break;

                //Increases Power by [VAL]. This effect end if stunned.
                case 47622100: //Increases Power by $H_W_GOOD$value$COLOR_END. This effect end if stunned.
                    break;

                //Increases power for the next chain skill by [VAL]: Arrow Volley.
                case 26030: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Arrow Volley.
                case 26150: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Arrow Volley.
                    break;

                //Increases power for the next chain skill by [VAL]: Combative Strike.
                case 21110: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Combative Strike.
                case 21111: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Combative Strike.
                    break;

                //Increases power for the next chain skill by [VAL]: Fireblast.
                case 25050: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Fireblast.
                    break;

                //Increases power for the next chain skill by [VAL]: Flaming Barrage.
                case 25080: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Flaming Barrage.
                    break;

                //Increases power for the next chain skill by [VAL]: Fury Strike.
                case 23120: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Fury Strike.
                    break;

                //Increases power for the next chain skill by [VAL]: Ice Needle.
                case 25030: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Ice Needle.
                    break;

                //Increases power for the next chain skill by [VAL]: Incendiary Trap.
                case 26130: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Incendiary Trap.
                    break;

                //Increases power for the next chain skill by [VAL]: Leaping Strike.
                case 21130: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Leaping Strike.
                case 21131: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Leaping Strike.
                case 23040: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Leaping Strike.
                    break;

                //Increases power for the next chain skill by [VAL]: Overhand Strike.
                case 23130: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Overhand Strike.
                    break;

                //Increases power for the next chain skill by [VAL]: Rain of Blows.
                case 21080: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Rain of Blows.
                    break;

                //Increases power for the next chain skill by [VAL]: Rising Fury.
                case 21040: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Rising Fury.
                    break;

                //Increases power for the next chain skill by [VAL]: Shield Counter.
                case 22040: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Shield Counter.
                    break;

                //Increases power for the next chain skill by [VAL]: Shocking Implosion.
                case 28060: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Shocking Implosion.
                    break;

                //Increases power for the next chain skill by [VAL]: Staggering Strike.
                case 24070: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Staggering Strike.
                    break;

                //Increases power for the next chain skill by [VAL]: Whirlwind.
                case 23030: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END: Whirlwind.
                    break;

                //Increases power for the next chain skill by [VAL]?but once only.
                case 25130: //Increases power for the next chain skill by $H_W_GOOD$value$COLOR_END?but once only.
                    break;

                //Increases resist to critical hit by [VAL], while within 20m of the caster.
                case 700230: //Increases resist to critical hit by $H_W_GOOD$value$COLOR_END, while within 20m of the caster.
                case 700231: //Increases resist to critical hit by $H_W_GOOD$value$COLOR_END, while within 20m of the caster.
                case 700232: //Increases resist to critical hit by $H_W_GOOD$value$COLOR_END, while within 20m of the caster.
                    break;

                //Increases resistance to immobilization and knockdown by [VAL], and decreases resistance to critical hits by [VAL], decreases charging speed by [VAL], and attack speed by [VAL]. Wintermight cannot be removed.
                case 47612200: //Increases resistance to immobilization and knockdown by $H_W_GOOD$value$COLOR_END, and decreases resistance to critical hits by $H_W_BAD$value2$COLOR_END, decreases charging speed by $H_W_BAD$value3$COLOR_END, and attack speed by $H_W_BAD$value4$COLOR_END. Wintermight cannot be removed.
                    break;

                //Increases resistance to immobilization and knockdown by [VAL], and decreases resistance to critical hits by [VAL], decreases charging speed by [VAL], and lowers attack speed by [VAL]. Glaciermight cannot be removed.
                case 47612100: //Increases resistance to immobilization and knockdown by $H_W_GOOD$value$COLOR_END, and decreases resistance to critical hits by $H_W_BAD$value2$COLOR_END, decreases charging speed by $H_W_BAD$value3$COLOR_END, and lowers attack speed by $H_W_BAD$value4$COLOR_END. Glaciermight cannot be removed.
                    break;

                //Increases resistance to immobilization and knockdown by [VAL]. Decreases resistance to critical hits by [VAL], and decreases charging speed by [VAL]. Icemight cannot be removed.
                case 47612000: //Increases resistance to immobilization and knockdown by $H_W_GOOD$value$COLOR_END. Decreases resistance to critical hits by $H_W_BAD$value2$COLOR_END, and decreases charging speed by $H_W_BAD$value3$COLOR_END. Icemight cannot be removed.
                    break;

                //Increases resistance to immobilization and knockdown by [VAL]. Decreases resistance to critical hits by [VAL]. Frostmight cannot be removed.
                case 47611900: //Increases resistance to immobilization and knockdown by $H_W_GOOD$value$COLOR_END. Decreases resistance to critical hits by $H_W_BAD$value2$COLOR_END. Frostmight cannot be removed.
                    break;

                //Increases skill damage by [VAL] every time its number of target increased and maximum of five targets with [VAL] increased skill damage but decreases HP by [VAL] of total HP every [VAL] per each stack. Dimensional Blade can be stacked up to 5 times and cannot be removed.
                case 47681300: //Increases skill damage by $H_W_GOOD$50%$COLOR_END every time its number of target increased and maximum of five targets with $H_W_GOOD3 times$COLOR_END increased skill damage but decreases HP by $H_W_BAD0.8%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END per each stack. Dimensional Blade can be stacked up to 5 times and cannot be removed.
                    break;

                //Increases skill damage by [VAL].
                case 47611400: //Increases skill damage by $H_W_GOOD$value$COLOR_END.
                case 49400300: //Increases skill damage by $H_W_GOOD$value$COLOR_END.
                case 99003300: //Increases skill damage by $H_W_GOOD$value$COLOR_END.
                    break;

                //Increases skill power by [VAL] for Metamorphic Blast.
                case 28020: //Increases skill power by $H_W_GOOD$value$COLOR_END for Metamorphic Blast.
                    break;

                //Increases skill power for the next chain skill by [VAL]: Penetrating Arrow.
                case 26070: //Increases skill power for the next chain skill by $H_W_GOOD$value$COLOR_END: Penetrating Arrow.
                    break;

                //Increases stamina by [VAL]. It can be stacked up to 3 times.
                case 99004800: //Increases stamina by $H_W_GOOD$value$COLOR_END. It can be stacked up to 3 times.
                    break;

                //Increases swimming speed by [VAL]. Can resurrect at the spot of death.
                case 47640600: //Increases swimming speed by $H_W_BAD$value$COLOR_END. Can resurrect at the spot of death.
                    break;

                //Increases the attack speed of PCs within 20m, excluding you, by [VAL], and increases Power and endurance by [VAL]. Also, decreases the cooldown by [VAL].
                case 4851: //Increases the attack speed of PCs within 20m, excluding you, by $H_W_GOOD100%$COLOR_END, and increases Power and endurance by $H_W_GOOD100 times$COLOR_END. Also, decreases the cooldown by $H_W_GOOD$90%$COLOR_END.
                    break;

                //Increases the chance of reaction resistance 10 times.
                case 21: //Increases the chance of reaction resistance 10 times.
                    break;

                //Increases XP by [VAL] while hunting.
                case 4500: //Increases XP by $H_W_GOOD10%$COLOR_END while hunting.
                case 4501: //Increases XP by $H_W_GOOD20%$COLOR_END while hunting.
                case 4502: //Increases XP by $H_W_GOOD30%$COLOR_END while hunting.
                case 4503: //Increases XP by $H_W_GOOD40%$COLOR_END while hunting.
                case 4504: //Increases XP by $H_W_GOOD50%$COLOR_END while hunting.
                case 4505: //Increases XP by $H_W_GOOD60%$COLOR_END while hunting.
                case 4506: //Increases XP by $H_W_GOOD70%$COLOR_END while hunting.
                case 4507: //Increases XP by $H_W_GOOD80%$COLOR_END while hunting.
                case 4508: //Increases XP by $H_W_GOOD90%$COLOR_END while hunting.
                case 4509: //Increases XP by $H_W_GOOD2 times$COLOR_END while hunting.
                case 4510: //Increases XP by $H_W_GOOD2.5 times$COLOR_END while hunting.
                case 4511: //Increases XP by $H_W_GOOD3 times$COLOR_END while hunting.
                case 4512: //Increases XP by $H_W_GOOD4 times$COLOR_END while hunting.
                case 4513: //Increases XP by $H_W_GOOD5 times$COLOR_END while hunting.
                case 4514: //Increases XP by $H_W_GOOD10 times$COLOR_END while hunting.
                    break;

                //Increases your awesomeness when beating down Big-Ass Monsters.
                case 2: //Increases your awesomeness when beating down Big-Ass Monsters.
                    break;

                //Infection consumes [VAL] of total HP every [VAL].
                case 47640700: //Infection consumes $H_W_BAD10%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 47640800: //Infection consumes $H_W_BAD5%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 47640900: //Infection consumes $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Infiltration mode, your surrounding becomes blur.
                case 4850: //Infiltration mode, your surrounding becomes blur.
                    break;

                //Insanity increases Power by [VAL] and the chance of getting knocked down or immobilized by [VAL]. Increases movement speed by [VAL] and attack speed by [VAL].
                case 47680000: //Insanity increases Power by $H_W_GOOD$value$COLOR_END and the chance of getting knocked down or immobilized by $H_W_GOOD$value2$COLOR_END. Increases movement speed by $H_W_GOOD$value3$COLOR_END and attack speed by $H_W_GOOD$value4$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 2);
                    Add(abnormal, new EfAttackSpeedInc(), 3);
                    break;

                //Instantly restores [VAL] of total HP.
                case 1510: //Instantly restores $H_W_GOOD50%$COLOR_END of total HP.
                    break;

                //Intoxicated by the power of delicious and strong wine restores MP by [VAL]. You are stunned and cannot move.
                case 4762: //Intoxicated by the power of delicious and strong wine restores MP by $H_W_GOOD1013$COLOR_END. You are stunned and cannot move.
                    break;

                //It is pitch-dark and you cannot see anything around you. Darkness cannot be removed.
                case 47660800: //It is pitch-dark and you cannot see anything around you. Darkness cannot be removed.
                    break;

                //It is pitch-dark and you cannot see anything around you. Darkness II cannot be removed.
                case 47661000: //It is pitch-dark and you cannot see anything around you. Darkness II cannot be removed.
                    break;

                //Just in time! Restores [VAL] HP every [VAL].
                case 4765: //Just in time! Restores $H_W_GOOD890$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Kelsaik is rampant! No one can stop him now.
                case 475150: //Kelsaik is rampant! No one can stop him now.
                    break;

                //Kills you as soon as debuff ends.
                case 301000: //Kills you as soon as debuff ends.
                case 301001: //Kills you as soon as debuff ends.
                    break;

                //Knockdown increases by [VAL].
                case 2710: //Knockdown increases by $H_W_GOOD$value$COLOR_END.
                case 2711: //Knockdown increases by $H_W_GOOD$value$COLOR_END.
                case 2712: //Knockdown increases by $H_W_GOOD$value$COLOR_END.
                case 2713: //Knockdown increases by $H_W_GOOD$value$COLOR_END.
                case 2714: //Knockdown increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Lose [VAL] HP every [VAL].
                case 100500: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100501: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100502: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100503: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100504: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100505: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100506: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100520: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100521: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100522: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100523: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100524: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100525: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 100526: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500700: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500701: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500702: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500703: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500704: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500705: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500706: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500707: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500720: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500721: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500722: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500723: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500724: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500725: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500726: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 500727: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600600: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600601: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600602: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600603: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600620: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600621: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600622: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 600623: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700800: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700801: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700802: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700803: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700804: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700805: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700806: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700807: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700820: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700821: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700822: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700823: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700824: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700825: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700826: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                case 700827: //Lose $H_W_BAD$value$COLOR_END HP every $H_W_BAD$tickInterval$COLOR_END.
                    Add(abnormal, new EfLoseHp(), 0);
                    break;

                //Lose [VAL] MP every [VAL].
                case 501100: //Lose $H_W_BAD$value$COLOR_END MP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Lose [VAL] of total HP every [VAL].
                case 4710: //Lose $H_W_BAD12%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000400: //Lose $H_W_BAD1.2%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000410: //Lose $H_W_BAD1.4%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000420: //Lose $H_W_BAD1.6%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000430: //Lose $H_W_BAD1.8%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000440: //Lose $H_W_BAD2%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000450: //Lose $H_W_BAD2.2%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000460: //Lose $H_W_BAD2.4%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000470: //Lose $H_W_BAD2.6%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99000480: //Lose $H_W_BAD50%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Lose [VAL] of total MP every [VAL].
                case 99002300: //Lose $H_W_BAD2%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002310: //Lose $H_W_BAD2.2%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002320: //Lose $H_W_BAD2.4%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002330: //Lose $H_W_BAD2.6%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002340: //Lose $H_W_BAD2.8%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002350: //Lose $H_W_BAD3%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002360: //Lose $H_W_BAD3.2%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                case 99002370: //Lose $H_W_BAD3.4%$COLOR_END of total MP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Lose [VAL] total HP every [VAL].
                case 99001030: //Lose $H_W_BAD2%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Lose 3% of total HP every [VAL].
                case 47613200: //Lose 3% of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Lose 3.6% total HP every [VAL]. Speed decreases by [VAL].
                case 99000970: //Lose 3.6% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses [VAL] of current HP when Prophecy of Death expires.
                case 47300700: //Loses $H_W_BAD$value$COLOR_END of current HP when Prophecy of Death expires.
                    break;

                //Loses [VAL] of total HP after [VAL] Thulsa's Poison cannot be purified or cleansed.
                case 47300300: //Loses $H_W_BAD30%$COLOR_END of total HP after $H_W_BAD$tickInterval$COLOR_END$BRThulsa's Poison cannot be purified or cleansed.
                    break;

                //Loses [VAL] of total HP every [VAL] and kills target when effect ends. Can be removed only with Doomfire or Doomchill.
                case 47501400: //Loses $H_W_BAD5%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and kills target when effect ends.$BRCan be removed only with Doomfire or Doomchill.
                case 47502000: //Loses $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and kills target when effect ends.$BRCan be removed only with Doomfire or Doomchill.
                    break;

                //Loses [VAL] of total HP every [VAL] and kills target when effect ends. Doomchill can be removed only with Doomfire.
                case 47501100: //Loses $H_W_BAD5%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and kills target when effect ends.$BRDoomchill can be removed only with Doomfire.
                case 47501900: //Loses $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and kills target when effect ends.$BRDoomchill can be removed only with Doomfire.
                    break;

                //Loses [VAL] of total HP every [VAL] and kills target when effect ends. Doomfire can be removed only with Doomchill.
                case 47501800: //Loses $H_W_BAD3%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and kills target when effect ends.$BRDoomfire can be removed only with Doomchill.
                    break;

                //Loses [VAL] of total HP every [VAL] and target dies when effect ends. Doomfire can be removed only with Doomchill.
                case 47500100: //Loses $H_W_BAD5%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END and target dies when effect ends.$BRDoomfire can be removed only with Doomchill.
                    break;

                //Loses [VAL] of total HP every [VAL].
                case 494550: //Loses $H_W_BAD2%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Loses [VAL] of total HP every [VAL]. Pollutes all party members in 10 meter radius and causes periodic damage. Cause of pollution cannot be neutralized.
                case 49400500: //Loses $H_W_BAD0.1%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.$BRPollutes all party members in 10 meter radius and causes periodic damage.$BRCause of pollution cannot be neutralized.
                    break;

                //Loses [VAL] of total HP every [VAL]. Ranged damage decreases by [VAL]. Seal of Punishment cannot be removed.
                case 47500900: //Loses $H_W_BAD10%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END.$BRRanged damage decreases by $H_W_BAD$value$COLOR_END.$BRSeal of Punishment cannot be removed.
                    break;

                //Loses [VAL] of total HP every [VAL]. Thulsa's Venom cannot be purified or cleansed.
                case 47300200: //Loses $H_W_BAD5%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.$BRThulsa's Venom cannot be purified or cleansed.
                    break;

                //Loses [VAL] of total HP every [VAL]. Venom Resin cannot be purified or cleansed.
                case 47300100: //Loses $H_W_BAD50%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END.$BRVenom Resin cannot be purified or cleansed.
                    break;

                //Loses [VAL] of your total HP every [VAL] Ranged damage decreases by [VAL]. Seal of Punishment cannot be removed.
                case 47500500: //Loses $H_W_BAD20%$COLOR_END of your total HP every $H_W_BAD$tickInterval2$COLOR_END$BRRanged damage decreases by $H_W_BAD$value$COLOR_END.$BRSeal of Punishment cannot be removed.
                    break;

                //Loses [VAL] total HP every [VAL].
                case 99001000: //Loses $H_W_BAD1.4%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001010: //Loses $H_W_BAD1.6%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001020: //Loses $H_W_BAD1.8%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001040: //Loses $H_W_BAD2.2%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001050: //Loses $H_W_BAD2.4%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001060: //Loses $H_W_BAD2.6%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                case 99001070: //Loses $H_W_BAD2.8%$COLOR_END total HP every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Loses 1.5% total HP every [VAL]. Speed decreases by [VAL].
                case 99000900: //Loses 1.5% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 1.8% total HP every [VAL]. Speed decreases by [VAL].
                case 99000910: //Loses 1.8% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 2.1% total HP every [VAL]. Speed decreases by [VAL].
                case 99000920: //Loses 2.1% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 2.4% total HP every [VAL]. Speed decreases by [VAL].
                case 99000930: //Loses 2.4% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 2.7% total HP every [VAL]. Speed decreases by [VAL].
                case 99000940: //Loses 2.7% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 3% total HP every [VAL]. Speed decreases by [VAL].
                case 99000950: //Loses 3% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Loses 3.3% total HP every [VAL]. Speed decreases by [VAL].
                case 99000960: //Loses 3.3% total HP every $H_W_BAD$tickInterval$COLOR_END.$BRSpeed decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Lost fighting will and flee!
                case 49400700: //Lost fighting will and flee!
                    break;

                //Makes your body wet. Decreases movement speed by [VAL]. Decreases HP by [VAL] when attacked. Use Fluffy Towel to dry.
                case 47660400: //Makes your body wet. Decreases movement speed by $H_W_BAD$value$COLOR_END. Decreases HP by $H_W_BAD$value2$COLOR_END when attacked. Use Fluffy Towel to dry.
                    break;

                //Maximum MP is reduced by [VAL] after 10 seconds.
                case 47620500: //Maximum MP is reduced by $H_W_BAD$value$COLOR_END after 10 seconds.
                case 47620600: //Maximum MP is reduced by $H_W_BAD$value$COLOR_END after 10 seconds.
                    break;

                //Movement slows to [VAL] and strips [VAL] of total HP every [VAL]. Snare of Agony cannot be removed.
                case 47650800: //Movement slows to $H_W_BAD$value$COLOR_END and strips $H_W_BAD9%$COLOR_END of total HP every $H_W_BAD$tickInterval$COLOR_END. Snare of Agony cannot be removed.
                    break;

                //Movement speed and attack speed decrease by $value.
                case 201200: //Movement speed and attack speed decrease by $value.
                    break;

                //Movement speed decreases by [VAL] and decrease HP by [VAL] every [VAL] by lightning struck.
                case 15080: //Movement speed decreases by $H_W_BAD$value$COLOR_END and decrease HP by $H_W_BAD$value2$COLOR_END every $H_W_BAD$tickInterval2$COLOR_END by lightning struck.
                    break;

                //Movement speed decreases by [VAL], and you lose [VAL] of total HP every [VAL]. Bleeding cannot be removed.
                case 99002510: //Movement speed decreases by $H_W_BAD$value$COLOR_END, and you lose $H_W_BAD10%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END.$BRBleeding cannot be removed.
                    break;

                //Movement speed decreases by [VAL].
                case 15070: //Movement speed decreases by $H_W_BAD$value$COLOR_END.
                case 15130: //Movement speed decreases by $H_W_BAD$value$COLOR_END.
                    break;

                //Movement speed decreases by [VAL]. This curse by Suryati cannot be removed.
                case 476107: //Movement speed decreases by $H_W_BAD$value$COLOR_END. This curse by Suryati cannot be removed.
                    break;

                //Movement speed increases by [VAL] while in combat, but chances of being immobilized or knocked down increase by [VAL]. Ends if knocked down.
                case 90300: //Movement speed increases by $H_W_GOOD$value$COLOR_END while in combat, but chances of being immobilized or knocked down increase by $H_W_BAD$value2$COLOR_END. Ends if knocked down.
                    Add(abnormal, new EfAttackSpeedInc(), 0);
                    break;

                //Movement speed increases by [VAL].
                case 2740: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2741: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2742: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2743: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2744: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2860: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2861: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                case 2862: //Movement speed increases by $H_W_GOOD$value$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Moving speed decreases by [VAL].
                case 101400: //Moving speed decreases by $H_W_BAD$value$COLOR_END.
                    break;

                //Next attack does [VAL] more damage.
                case 500100: //Next attack does $H_W_GOOD$value$COLOR_END more damage.
                    break;

                //No debuff tooltip display (isShow=False). If this tooltip displays, BTS is required!
                case 801650: //No debuff tooltip display (isShow=False). If this tooltip displays, BTS is required!
                    break;

                //No natural MP regeneration! For 30 minutes! BTS needed if this message is displayed.
                case 3: //No natural MP regeneration! For 30 minutes! BTS needed if this message is displayed.
                    break;

                //Non-combat movement speed increases by [VAL].
                case 15: //Non-combat movement speed increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Non-effect debuff
                case 5: //Non-effect debuff
                    break;

                //Oh, I think the pattern experiment has failed... You look very angry...
                case 47680700: //Oh, I think the pattern experiment has failed... You look very angry...
                    break;

                //Once the third Word of Unmaking is spoken, the Unmaking is unleashed. This effect cannot be canceled.
                case 99002700: //Once the third Word of Unmaking is spoken, the Unmaking is unleashed. This effect cannot be canceled.
                    break;

                //Other PCs have less chance of knocking you down.
                case 90100: //Other PCs have less chance of knocking you down.
                    break;

                //Others in your party receive [VAL] while within 20m of you. Ends if you are knocked down.
                case 200200: //Others in your party receive $H_W_GOODGuardian Power$COLOR_END while within 20m of you. Ends if you are knocked down.
                case 200201: //Others in your party receive $H_W_GOODGuardian Power$COLOR_END while within 20m of you. Ends if you are knocked down.
                    break;

                //Panicked! Flee for approximately 10m.
                case 400601: //Panicked! Flee for approximately 10m.
                    break;

                //Panicked! Flee for approximately 12m.
                case 400602: //Panicked! Flee for approximately 12m.
                    break;

                //Panicked! Flee for approximately 8m.
                case 400600: //Panicked! Flee for approximately 8m.
                    break;

                //Passion for eating coconuts makes you temporarily invincible against any damage. You do not get knocked down or immobilized.
                case 47662200: //Passion for eating coconuts makes you temporarily invincible against any damage. You do not get knocked down or immobilized.
                    break;

                //Past the Black Warp, you become more powerful! Increases maximum HP by [VAL] per each stack. Increases Power and endurance by [VAL]. Also, increases resistance to knock-down and immobility.
                case 47680800: //Past the Black Warp, you become more powerful! Increases maximum HP by $H_W_GOOD$value$COLOR_END per each stack. Increases Power and endurance by $H_W_GOOD$value2$COLOR_END. Also, increases resistance to knock-down and immobility.
                    break;

                //Power decreases by [VAL].
                case 99000600: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000610: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000620: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000630: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000640: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000650: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000660: //Power decreases by $H_W_BAD$value$COLOR_END.
                case 99000670: //Power decreases by $H_W_BAD$value$COLOR_END.
                    break;

                //Power increases by $value. Balance and resistance to stun increase 50%. Inflict [VAL] more damage when attacking other players.
                case 400700: //Power increases by $value.$BRBalance and resistance to stun increase 50%.$BRInflict $H_W_GOOD$value2$COLOR_END more damage when attacking other players.
                    break;

                //Power increases by $value. Your damage against other players increases by $value2.
                case 300800: //Power increases by $value. Your damage against other players increases by $value2.
                    break;

                //Power increases by [VAL].
                case 201: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 202: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 601: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 602: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 1250: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10300: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10301: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10302: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10303: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10304: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10305: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10310: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10311: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10312: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10313: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10314: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10315: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10320: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10321: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10322: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10323: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10324: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 10325: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 22010: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 24010: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 24060: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 25010: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 47612300: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000100: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000110: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000120: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000130: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000140: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000150: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000160: //Power increases by $H_W_GOOD$value$COLOR_END.
                case 99000170: //Power increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Power increases by [VAL]. Can be heaped up 99 times.
                case 47300800: //Power increases by $H_W_GOOD$value$COLOR_END. Can be heaped up 99 times.
                    break;

                //Power increases by [VAL]. Endurance decreases by [VAL].
                case 99001500: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001510: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001520: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001530: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001540: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001550: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001560: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 99001570: //Power increases by $H_W_GOOD$value$COLOR_END. Endurance decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Power is reduced by [VAL]. This effect cannot be removed.
                case 476108: //Power is reduced by $H_W_BAD$value$COLOR_END. This effect cannot be removed.
                    break;

                //Prevents MP decay.
                case 300100: //Prevents MP decay.
                case 400400: //Prevents MP decay.
                    break;

                //Prevents the crystal from breaking at the time of death.
                case 4610: //Prevents the crystal from breaking at the time of death.
                    break;

                //Projecting the aura on yourself and party members within 20m costs you [VAL] MP per [VAL] while it is active.
                case 700200: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                case 700201: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                case 700202: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                case 700300: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                case 700600: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                case 700700: //Projecting the aura on yourself and party members within 20m costs you $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while it is active.
                    break;

                //Purification decreases MP by [VAL] every [VAL]. The Purification of Dagon cannot be removed.
                case 476705: //Purification decreases MP by $H_W_BAD50%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. The Purification of Dagon cannot be removed.
                    break;

                //Rage added with sorrow increases Power by [VAL] and movement speed by [VAL]. Reduces endurance by [VAL]. Rage of the Warrior cannot be removed.
                case 47670200: //Rage added with sorrow increases Power by $H_W_GOOD$value$COLOR_END and movement speed by $H_W_GOOD$value2$COLOR_END. Reduces endurance by $H_W_BAD$value3$COLOR_END. Rage of the Warrior cannot be removed.
                    break;

                //Reaction absolute invincibility
                case 10: //Reaction absolute invincibility
                    break;

                //Receive [VAL] less HP from healing or restoration effects.
                case 25160: //Receive $H_W_GOOD$value$COLOR_END less HP from healing or restoration effects.
                    break;

                //Received Commander's blessing! Heals [VAL] of total HP every [VAL].
                case 900400: //Received Commander's blessing! Heals $H_W_GOOD2%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 900401: //Received Commander's blessing! Heals $H_W_GOOD2%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //Reduces maximum HP by [VAL], but increases resistance to immobilization and knock down by [VAL] and increases resistance to harmful effect by [VAL].
                case 47620300: //Reduces maximum HP by $H_W_BAD$value$COLOR_END, but increases resistance to immobilization and knock down by $H_W_GOOD$value2$COLOR_END and increases resistance to harmful effect by $H_W_GOOD$value3$COLOR_END.
                    break;

                //Reduces MP cost by [VAL] for Metamorphic Blast.
                case 27020: //Reduces MP cost by $H_W_GOOD$value$COLOR_END for Metamorphic Blast.
                    break;

                //Reduces MP cost for the next chain skill by [VAL]: Incendiary Trap.
                case 26060: //Reduces MP cost for the next chain skill by $H_W_GOOD$value$COLOR_END: Incendiary Trap.
                    break;

                //Reflecting damage!!! Decreases HP by [VAL] of total HP every successful hit.
                case 47613100: //Reflecting damage!!! Decreases HP by $H_W_GOOD0.1%$COLOR_END of total HP every successful hit.
                    break;

                //Regenerates [VAL] of total MP per stack every [VAL]. Effect stacks up to three times.
                case 99003700: //Regenerates $H_W_GOOD0.5%$COLOR_END of total MP per stack every $H_W_GOOD$tickInterval$COLOR_END. Effect stacks up to three times.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Resist to periodic damage increases by [VAL].
                case 2785: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2786: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2787: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2788: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2789: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2830: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2831: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2832: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2833: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                case 2834: //Resist to periodic damage increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Resist to stun increases by [VAL].
                case 2800: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2801: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2802: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2803: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2804: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2845: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2846: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2847: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2848: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                case 2849: //Resist to stun increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Resistance to all harmful effect, and increases endurance by [VAL].
                case 47612700: //Resistance to all harmful effect, and increases endurance by $H_W_GOOD$value2$COLOR_END.
                    break;

                //Resistance to Malaise of Despair increases by [VAL]. Effect stacks up to three times.
                case 2400: //Resistance to Malaise of Despair increases by $H_W_GOOD$value$COLOR_END. Effect stacks up to three times.
                    break;

                //Resistance to weakening effects increases by [VAL].
                case 2770: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2771: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2772: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2773: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2774: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2815: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2816: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2817: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2818: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                case 2819: //Resistance to weakening effects increases by $H_W_GOOD$value$COLOR_END.
                    break;

                //Resistances increase by [VAL].
                case 800400: //Resistances increase by $H_W_GOOD$value$COLOR_END.
                    break;

                //Resists any damage 100% and increases skill damage by [VAL].
                case 47662130: //Resists any damage 100% and increases skill damage by $H_W_GOOD$value2$COLOR_END.
                    break;

                //Restores  [VAL] of total MP every [VAL].
                case 1133: //Restores  $H_W_GOOD4%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] HP every [VAL].
                case 4764: //Restores $H_W_GOOD1424$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 4766: //Restores $H_W_GOOD2137$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701000: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701001: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701002: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701003: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701004: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701005: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701006: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701007: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701008: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701009: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701010: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 701011: //Restores $H_W_GOOD$value$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                case 801600: //Restores $H_W_GOOD625$COLOR_END HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Restores [VAL] MP every [VAL], or until you take damage or use another combat skill or item.
                case 700900: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END, or until you take damage or use another combat skill or item.
                case 800200: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END, or until you take damage or use another combat skill or item.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] MP every [VAL].
                case 1200: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10900: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10901: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10902: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10903: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10904: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10905: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10910: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10911: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10912: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10913: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10914: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10915: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10920: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10921: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10922: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10923: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10924: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 10925: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11000: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11001: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11002: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11003: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11004: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11005: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11010: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11011: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11012: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11013: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11014: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11015: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11020: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11021: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11022: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11023: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11024: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 11025: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 500300: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 500301: //Restores $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] MP per [VAL] while within 20m of the caster.
                case 700330: //Restores $H_W_GOOD$value$COLOR_END MP per $H_W_GOOD$tickInterval$COLOR_END while within 20m of the caster.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] of all HP.
                case 990046: //Restores $H_W_GOOD50%$COLOR_END of all HP.
                case 99004500: //Restores $H_W_GOOD50%$COLOR_END of all HP.
                case 99004600: //Restores $H_W_GOOD50%$COLOR_END of all HP.
                    break;

                //Restores [VAL] of MP every [VAL].
                case 2530: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2531: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2532: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2533: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2534: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2535: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2536: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2537: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2545: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2546: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2547: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2548: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2549: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2550: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2551: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2552: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2620: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2621: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2622: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2623: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2624: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2625: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2626: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2627: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2635: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2636: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2637: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2638: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2639: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2640: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2641: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2642: //Restores $H_W_GOOD$value$COLOR_END of MP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] of total HP every [VAL].
                case 476621: //Restores $H_W_GOOD20%$COLOR_END of total HP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenHp(), 0);
                    break;

                //Restores [VAL] of total MP every [VAL].
                case 1530: //Restores $H_W_GOOD5%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2000: //Restores $H_W_GOOD5%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 2010: //Restores $H_W_GOOD2%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 24150: //Restores $H_W_GOOD6%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                case 90200: //Restores $H_W_GOOD5%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] of total MP every [VAL]. Restores total of 17% MP.
                case 18601: //Restores $H_W_GOOD1.7%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END. Restores total of 17% MP.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores [VAL] of total MP every [VAL]. Restores total of 20% MP.
                case 18602: //Restores $H_W_GOOD2%$COLOR_END of total MP every $H_W_GOOD$tickInterval$COLOR_END. Restores total of 20% MP.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores 15% of total MP, [VAL] every [VAL].
                case 18600: //Restores 15% of total MP, $H_W_GOOD1.5%$COLOR_END every $H_W_GOOD$tickInterval$COLOR_END.
                    Add(abnormal, new EfRegenMp(), 0);
                    break;

                //Restores HP by [VAL] for every successful hit.  Self-Reliance of the Black Warp cannot be removed.
                case 47680400: //Restores HP by $H_W_GOOD0.2%$COLOR_END for every successful hit. $BRSelf-Reliance of the Black Warp cannot be removed.
                    break;

                //Restores HP by [VAL] when a skill hits the target.
                case 47680100: //Restores HP by $H_W_GOOD$value$COLOR_END when a skill hits the target.
                    break;

                //Resurrect instantly if you are killed while in effect.
                case 700100: //Resurrect instantly if you are killed while in effect.
                    break;

                //Scatter in confusion. Ends when knocked down.
                case 49400710: //Scatter in confusion. Ends when knocked down.
                    break;

                //Self-destruct. Orcan Loyalty cannot be removed.
                case 47621200: //Self-destruct. Orcan Loyalty cannot be removed.
                    break;

                //Seren protects you against attacks from  infamous, notorious, nefarious, and reviled outlaws. Effect ends when you become level 11.
                case 999800: //Seren protects you against attacks from  infamous, notorious, nefarious, and reviled outlaws. Effect ends when you become level 11.
                    break;

                //Seren's blessing on travelers. Combat XP increases by [VAL]. Effect persists through death.
                case 1110: //Seren's blessing on travelers. Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                    break;

                //Shock! Shock! Decreases HP by [VAL] every [VAL]. Stuns you and immobilizes you. Shock cannot be removed.
                case 47660300: //Shock! Shock! Decreases HP by $H_W_BAD30%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. Stuns you and immobilizes you. Shock cannot be removed.
                    break;

                //Skills cannot be used while in effect. Incapacitation can not be removed.
                case 47610400: //Skills cannot be used while in effect. Incapacitation can not be removed.
                    break;

                //Sleep comfortably. Ends if you take damage.
                case 99001900: //Sleep comfortably. Ends if you take damage.
                case 99001910: //Sleep comfortably. Ends if you take damage.
                case 99001920: //Sleep comfortably. Ends if you take damage.
                case 99001930: //Sleep comfortably. Ends if you take damage.
                case 99001940: //Sleep comfortably. Ends if you take damage.
                case 99001950: //Sleep comfortably. Ends if you take damage.
                case 99001960: //Sleep comfortably. Ends if you take damage.
                case 99001970: //Sleep comfortably. Ends if you take damage.
                    break;

                //Sleep comfortably. Ends immediately if you are hit.
                case 801200: //Sleep comfortably. Ends immediately if you are hit.
                case 801201: //Sleep comfortably. Ends immediately if you are hit.
                    break;

                //Sleep comfortably. Ends immediately when hit.
                case 701100: //Sleep comfortably. Ends immediately when hit.
                case 701101: //Sleep comfortably. Ends immediately when hit.
                    break;

                //Speed decreases by $value and attack speed by $value2.
                case 601000: //Speed decreases by $value and attack speed by $value2.
                    break;

                //Speed decreases by $value or more.
                case 201101: //Speed decreases by $value or more.
                case 201102: //Speed decreases by $value or more.
                    break;

                //Speed decreases by $value.
                case 201100: //Speed decreases by $value.
                    break;

                //Speed decreases by $value. Players cannot use evasive or movement skills.
                case 400800: //Speed decreases by $value.$BRPlayers cannot use evasive or movement skills.
                    break;

                //Speed decreases by [VAL] and balance increases by [VAL] for $time. Effect ends when damaged significantly or knocked down.
                case 701400: //Speed decreases by $H_W_BAD$value$COLOR_END and balance increases by $H_W_GOOD$value2$COLOR_END for $time. Effect ends when damaged significantly or knocked down.
                case 701401: //Speed decreases by $H_W_BAD$value$COLOR_END and balance increases by $H_W_GOOD$value2$COLOR_END for $time. Effect ends when damaged significantly or knocked down.
                case 701420: //Speed decreases by $H_W_BAD$value$COLOR_END and balance increases by $H_W_GOOD$value2$COLOR_END for $time. Effect ends when damaged significantly or knocked down.
                case 701421: //Speed decreases by $H_W_BAD$value$COLOR_END and balance increases by $H_W_GOOD$value2$COLOR_END for $time. Effect ends when damaged significantly or knocked down.
                    break;

                //Speed decreases by [VAL] or more.
                case 500500: //Speed decreases by $H_W_BAD$value$COLOR_END or more.
                    break;

                //Speed decreases by [VAL] until you use the skill or get knocked down.
                case 900300: //Speed decreases by $H_W_BAD$value$COLOR_END until you use the skill or get knocked down.
                    break;

                //Speed decreases by [VAL], and HP recover amount decreases by [VAL] when receive HP recovering effect. You lose [VAL] of total HP every [VAL] while in effect.
                case 99002520: //Speed decreases by $H_W_BAD$value$COLOR_END, and HP recover amount decreases by $H_W_BAD$value3$COLOR_END when receive HP recovering effect. You lose $H_W_BAD6%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END while in effect.
                    break;

                //Speed decreases by [VAL], and you lose [VAL] of total HP every [VAL].
                case 99000580: //Speed decreases by $H_W_BAD$value$COLOR_END, and you lose $H_W_BAD6%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END.
                case 99002500: //Speed decreases by $H_W_BAD$value$COLOR_END, and you lose $H_W_BAD6%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END.
                    break;

                //Speed decreases by [VAL].
                case 100630: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 500200: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 600400: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 600700: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 600701: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 700500: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 700501: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 800800: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 900100: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 900110: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 900200: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 900210: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000500: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000510: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000520: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000530: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000540: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000550: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000560: //Speed decreases by $H_W_BAD$value$COLOR_END.
                case 99000570: //Speed decreases by $H_W_BAD$value$COLOR_END.
                    break;

                //Speed increases by [VAL] while out of combat.
                case 1470: //Speed increases by $H_W_GOOD$value$COLOR_END while out of combat.
                case 1480: //Speed increases by $H_W_GOOD$value$COLOR_END while out of combat.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Speed increases by [VAL] while within 20m of caster.
                case 700730: //Speed increases by $H_W_GOOD$value$COLOR_END while within 20m of caster.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Speed increases by [VAL].
                case 23010: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 300600: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 300601: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 400300: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 400301: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000800: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000810: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000820: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000830: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000840: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000850: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000860: //Speed increases by $H_W_GOOD$value$COLOR_END.
                case 99000870: //Speed increases by $H_W_GOOD$value$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Speeds casting by [VAL] for Arun's Tears.
                case 27100: //Speeds casting by $H_W_GOOD$value$COLOR_END for Arun's Tears.
                    break;

                //Speeds casting by [VAL] for Arun's Vitae.
                case 27070: //Speeds casting by $H_W_GOOD$value$COLOR_END for Arun's Vitae.
                case 27080: //Speeds casting by $H_W_GOOD$value$COLOR_END for Arun's Vitae.
                    break;

                //Speeds casting by [VAL] for Fireblast.
                case 25100: //Speeds casting by $H_W_GOOD$value$COLOR_END for Fireblast.
                    break;

                //Speeds casting by [VAL] for Flame Pillar.
                case 25070: //Speeds casting by $H_W_GOOD$value$COLOR_END for Flame Pillar.
                    break;

                //Speeds casting by [VAL] for Flaming Barrage.
                case 25060: //Speeds casting by $H_W_GOOD$value$COLOR_END for Flaming Barrage.
                    break;

                //Speeds casting by [VAL] for Flatten.
                case 24100: //Speeds casting by $H_W_GOOD$value$COLOR_END for Flatten.
                case 24101: //Speeds casting by $H_W_GOOD$value$COLOR_END for Flatten.
                    break;

                //Speeds casting by [VAL] for Heart Thrust.
                case 23060: //Speeds casting by $H_W_GOOD$value$COLOR_END for Heart Thrust.
                case 23061: //Speeds casting by $H_W_GOOD$value$COLOR_END for Heart Thrust.
                    break;

                //Speeds casting by [VAL] for Knockdown Strike.
                case 23070: //Speeds casting by $H_W_GOOD$value$COLOR_END for Knockdown Strike.
                    break;

                //Speeds casting by [VAL] for Lightning Trap.
                case 25090: //Speeds casting by $H_W_GOOD$value$COLOR_END for Lightning Trap.
                    break;

                //Speeds casting by [VAL] for Mindblast.
                case 25110: //Speeds casting by $H_W_GOOD$value$COLOR_END for Mindblast.
                    break;

                //Speeds casting by [VAL] for Onslaught.
                case 22060: //Speeds casting by $H_W_GOOD$value$COLOR_END for Onslaught.
                    break;

                //Speeds casting by [VAL] for Poison Arrow.
                case 26100: //Speeds casting by $H_W_GOOD$value$COLOR_END for Poison Arrow.
                    break;

                //Speeds casting by [VAL] for Stunning Trap.
                case 26110: //Speeds casting by $H_W_GOOD$value$COLOR_END for Stunning Trap.
                case 26111: //Speeds casting by $H_W_GOOD$value$COLOR_END for Stunning Trap.
                    break;

                //Speeds casting by [VAL] for Thrall of Life.
                case 27090: //Speeds casting by $H_W_GOOD$value$COLOR_END for Thrall of Life.
                    break;

                //Speeds casting by [VAL] for Whirlwind.
                case 23080: //Speeds casting by $H_W_GOOD$value$COLOR_END for Whirlwind.
                    break;

                //Speeds charging by [VAL] for Arcane Pulse.
                case 25140: //Speeds charging by $H_W_GOOD$value$COLOR_END for Arcane Pulse.
                    break;

                //Speeds charging by [VAL] for Lethal Strike.
                case 24120: //Speeds charging by $H_W_GOOD$value$COLOR_END for Lethal Strike.
                    break;

                //Speeds charging by [VAL] for Penetrating Arrow.
                case 26160: //Speeds charging by $H_W_GOOD$value$COLOR_END for Penetrating Arrow.
                case 26170: //Speeds charging by $H_W_GOOD$value$COLOR_END for Penetrating Arrow.
                case 26171: //Speeds charging by $H_W_GOOD$value$COLOR_END for Penetrating Arrow.
                case 26190: //Speeds charging by $H_W_GOOD$value$COLOR_END for Penetrating Arrow.
                    break;

                //Speeds charging by [VAL] for Radiant Arrow.
                case 26180: //Speeds charging by $H_W_GOOD$value$COLOR_END for Radiant Arrow.
                    break;

                //Speeds charging by [VAL] for Thunder Strike.
                case 24130: //Speeds charging by $H_W_GOOD$value$COLOR_END for Thunder Strike.
                    break;

                //Stamina does not decrease while in effect.
                case 2930: //Stamina does not decrease while in effect.
                case 2931: //Stamina does not decrease while in effect.
                case 2932: //Stamina does not decrease while in effect.
                    break;

                //Stone
                case 11: //Stone
                    break;

                //Strike vulnerable joints to reduce foe's speed by [VAL]. Uses [VAL] MP per [VAL] while active. Triggered on your fourth consecutive hit with the Combo Attack skill.
                case 100600: //Strike vulnerable joints to reduce foe's speed by $H_W_GOOD25%$COLOR_END. Uses $H_W_BAD$value2$COLOR_END MP per $H_W_BAD$tickInterval2$COLOR_END while active. Triggered on your fourth consecutive hit with the Combo Attack skill.
                    break;

                //Stun any other party members within 15m radius of you.
                case 47610600: //Stun any other party members within 15m radius of you.
                    break;

                //Stun effect
                case 4: //Stun effect
                    break;

                //Stunned and cannot move. Brutal blow cannot be removed.
                case 47610500: //Stunned and cannot move. Brutal blow cannot be removed.
                    break;

                //Stunned and disoriented.
                case 400200: //Stunned and disoriented.
                    break;

                //Stunned and frozen in place.
                case 15030: //Stunned and frozen in place.
                    break;

                //Stunned by mighty impact.
                case 15060: //Stunned by mighty impact.
                    break;

                //Stunned every [VAL] and decrease HP by [VAL] whenever stunned by unextinguished flame.
                case 15040: //Stunned every $H_W_BAD$tickInterval2$COLOR_END and decrease HP by $H_W_BAD$value2$COLOR_END whenever stunned by unextinguished flame.
                    break;

                //Stunned. Cannot move!
                case 476106: //Stunned. Cannot move!
                    break;

                //Stunned. Cannot move.
                case 301: //Stunned. Cannot move.
                case 302: //Stunned. Cannot move.
                case 901: //Stunned. Cannot move.
                case 100300: //Stunned. Cannot move.
                case 100301: //Stunned. Cannot move.
                case 100900: //Stunned. Cannot move.
                case 101000: //Stunned. Cannot move.
                case 101100: //Stunned. Cannot move.
                case 200100: //Stunned. Cannot move.
                case 200400: //Stunned. Cannot move.
                case 200800: //Stunned. Cannot move.
                case 300400: //Stunned. Cannot move.
                case 300700: //Stunned. Cannot move.
                case 473650: //Stunned. Cannot move.
                case 476225: //Stunned. Cannot move.
                case 500800: //Stunned. Cannot move.
                case 500801: //Stunned. Cannot move.
                case 600300: //Stunned. Cannot move.
                case 600800: //Stunned. Cannot move.
                case 47300500: //Stunned. Cannot move.
                case 99000000: //Stunned. Cannot move.
                case 99000010: //Stunned. Cannot move.
                case 99000020: //Stunned. Cannot move.
                case 99000030: //Stunned. Cannot move.
                case 99000040: //Stunned. Cannot move.
                case 99000050: //Stunned. Cannot move.
                case 99000060: //Stunned. Cannot move.
                case 99000070: //Stunned. Cannot move.
                case 99001280: //Stunned. Cannot move.
                    break;

                //Stunned. Cannot move. Blindlight cannot be removed.
                case 476302: //Stunned. Cannot move. Blindlight cannot be removed.
                    break;

                //Stunned. Cannot move. Wrath of Kelsaik cannot be removed.
                case 47501200: //Stunned. Cannot move. Wrath of Kelsaik cannot be removed.
                    break;

                //Stuns all other party members within 15m of you. Eyestriker cannot be removed.
                case 47630200: //Stuns all other party members within 15m of you. Eyestriker cannot be removed.
                    break;

                //Stuns all party members in 10m.
                case 47300600: //Stuns all party members in 10m.
                    break;

                //Stuns party members within 5m of you.
                case 99001200: //Stuns party members within 5m of you.
                case 99001210: //Stuns party members within 5m of you.
                case 99001220: //Stuns party members within 5m of you.
                case 99001230: //Stuns party members within 5m of you.
                case 99001240: //Stuns party members within 5m of you.
                case 99001250: //Stuns party members within 5m of you.
                case 99001260: //Stuns party members within 5m of you.
                case 99001270: //Stuns party members within 5m of you.
                    break;

                //Stuns you and immobilizes you.
                case 201400: //Stuns you and immobilizes you.
                case 201401: //Stuns you and immobilizes you.
                case 601200: //Stuns you and immobilizes you.
                    break;

                //Stuns you and immobilizes you. Shocking Electricity cannot be removed.
                case 476602: //Stuns you and immobilizes you. Shocking Electricity cannot be removed.
                    break;

                //Sweeter than any other chocolate, the Heart Chocolate instantly restores [VAL] of the max MP.
                case 1131: //Sweeter than any other chocolate, the Heart Chocolate instantly restores $H_W_GOOD50%$COLOR_END of the max MP.
                    break;

                //Take [VAL] less damage from other players. Effect persists through death.
                case 1400: //Take $H_W_GOOD$value$COLOR_END less damage from other players.$BREffect persists through death.
                    break;

                //Target dies after effect's duration. Extinction cannot be removed.
                case 47670900: //Target dies after effect's duration.$BRExtinction cannot be removed.
                    break;

                //Test
                case 1650: //Test
                case 1660: //Test
                case 90600: //Test
                case 476619: //Test
                case 476629: //Test
                case 47500200: //Test
                case 47612600: //Test
                    break;

                //The bloodstain instantly restores HP by [VAL] when a skill hits the target. Yuanti’s Bloodstain cannot be removed.
                case 47670600: //The bloodstain instantly restores HP by $H_W_GOOD$value$COLOR_END when a skill hits the target. Yuanti’s Bloodstain cannot be removed.
                    break;

                //The energy from the Blessing Jar decreases the cooldown by [VAL]. Blessing Jars cannot be removed.
                case 47662000: //The energy from the Blessing Jar decreases the cooldown by $H_W_GOOD$value$COLOR_END. Blessing Jars cannot be removed.
                    break;

                //The Fist of Torment kills you. You cannot escape your fate.
                case 47650900: //The Fist of Torment kills you. You cannot escape your fate.
                    break;

                //The Goddess of the Night blesses your journey. You project an aura on yourself and party members within 40m. Effect persists through death.
                case 1120: //The Goddess of the Night blesses your journey. You project an aura on yourself and party members within 40m.$BREffect persists through death.
                    break;

                //The lancer protecting you takes [VAL] of the damage you take and decreases damage received by [VAL], as long as you stay within 20m.
                case 200930: //The lancer protecting you takes $H_W_GOOD35%$COLOR_END of the damage you take and decreases damage received by $H_W_GOOD$value2$COLOR_END, as long as you stay within 20m.
                    break;

                //The mysterious energy of the Fane of Kaprima absorbs damage up to [VAL]. As soon as the maximum absorption amount is exceeded, the effect disappears regardless of its duration time. Magic Absorption cannot be removed.
                case 47670100: //The mysterious energy of the Fane of Kaprima absorbs damage up to $H_W_GOOD$value$COLOR_END. As soon as the maximum absorption amount is exceeded, the effect disappears regardless of its duration time. Magic Absorption cannot be removed.
                case 47670101: //The mysterious energy of the Fane of Kaprima absorbs damage up to $H_W_GOOD$value$COLOR_END. As soon as the maximum absorption amount is exceeded, the effect disappears regardless of its duration time. Magic Absorption cannot be removed.
                    break;

                //The power of the Heart Lollipop restores [VAL] of the max MP every [VAL].
                case 1130: //The power of the Heart Lollipop restores $H_W_GOOD4%$COLOR_END of the max MP every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //The Powerful Infused Renewal Draught has a special effect of restoring [VAL] MP every [VAL].
                case 1201: //The Powerful Infused Renewal Draught has a special effect of restoring $H_W_GOOD$value$COLOR_END MP every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //The Powerful Potion of Cultural Opportunity has a special effect of increasing your hunting XP by [VAL]. The effect does not disappear, even if you die.
                case 1105: //The Powerful Potion of Cultural Opportunity has a special effect of increasing your hunting XP by $H_W_GOOD$value$COLOR_END. The effect does not disappear, even if you die.
                    break;

                //The Powerful Vigorous Renewal Draught has a special effect of restoring [VAL] of all HP every [VAL].
                case 1151: //The Powerful Vigorous Renewal Draught has a special effect of restoring $H_W_GOOD1%$COLOR_END of all HP every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //The repair has been beautifully done. Increases endurance by [VAL] and maximum HP by [VAL]. Repair Completed cannot be removed.
                case 47660600: //The repair has been beautifully done. Increases endurance by $H_W_GOOD$value$COLOR_END and maximum HP by $H_W_GOOD$value2$COLOR_END. Repair Completed cannot be removed.
                    break;

                //The sad cry of the mother rings in the sky. Decreases endurance by [VAL]. The Sadness of the Mother can be stacked up to 6 times.
                case 47662500: //The sad cry of the mother rings in the sky. Decreases endurance by $H_W_BAD$value$COLOR_END. The Sadness of the Mother can be stacked up to 6 times.
                    break;

                //The summoned energy absorbs damage of up to [VAL]. As soon as the maximum absorption amount is exceeded, the effect disappears regardless of its duration time.
                case 701604: //The summoned energy absorbs damage of up to $H_W_GOOD$value$COLOR_END. As soon as the maximum absorption amount is exceeded, the effect disappears regardless of its duration time.
                    break;

                //The summoned energy restores HP by [VAL] every [VAL].
                case 701605: //The summoned energy restores HP by $H_W_GOOD$value$COLOR_END every $H_W_GOOD$tickInterval$COLOR_END.
                    break;

                //The tremendous heat of the temple decreases HP by [VAL] every [VAL]. Overheat of the Temple cannot be removed.
                case 47660100: //The tremendous heat of the temple decreases HP by $H_W_BAD3%$COLOR_END every $H_W_BAD$tickInterval$COLOR_END. Overheat of the Temple cannot be removed.
                    break;

                //This altar is indestructible until kill all monsters.
                case 99003000: //This altar is indestructible until kill all monsters.
                    break;

                //This machine is well maintained with daily oiling! Increases movement speed by [VAL] and attack speed by [VAL]. Well-Maintained Machines cannot be removed.
                case 47660500: //This machine is well maintained with daily oiling! Increases movement speed by $H_W_GOOD$value$COLOR_END and attack speed by $H_W_GOOD$value2$COLOR_END. Well-Maintained Machines cannot be removed.
                    break;

                //Torchbearer nearby. Combat XP increases by [VAL]. Effect persists through death.
                case 1125: //Torchbearer nearby. Combat XP increases by $H_W_GOOD$value$COLOR_END.$BREffect persists through death.
                    break;

                //Unable to use skills.
                case 15000: //Unable to use skills.
                    break;

                //Unchained goddess
                case 13: //Unchained goddess
                    break;

                //Vortex
                case 12: //Vortex
                    break;

                //When it is stacked 10 times, the deep sleep effect occurs.
                case 47662140: //When it is stacked 10 times, the deep sleep effect occurs.
                    break;

                //When Nexus Strike stacks [VAL] times, the nexus will implode upon you, killing you on the spot. Nexus Strike cannot be removed.
                case 47680600: //When Nexus Strike stacks $H_W_BAD5$COLOR_END times, the nexus will implode upon you, killing you on the spot.$BRNexus Strike cannot be removed.
                    break;

                //While in effect, 100% resist to all harmful effect with magical armor.
                case 15140: //While in effect, 100% resist to all harmful effect with magical armor.
                    break;

                //While in effect, cooldown of close-range attack skills decreases by [VAL].
                case 2960: //While in effect, cooldown of close-range attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2961: //While in effect, cooldown of close-range attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2962: //While in effect, cooldown of close-range attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2963: //While in effect, cooldown of close-range attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2964: //While in effect, cooldown of close-range attack skills decreases by $H_W_GOOD$value$COLOR_END.
                    break;

                //While in effect, cooldown of ranged attack skills decreases by [VAL].
                case 2945: //While in effect, cooldown of ranged attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2946: //While in effect, cooldown of ranged attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2947: //While in effect, cooldown of ranged attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2948: //While in effect, cooldown of ranged attack skills decreases by $H_W_GOOD$value$COLOR_END.
                case 2949: //While in effect, cooldown of ranged attack skills decreases by $H_W_GOOD$value$COLOR_END.
                    break;

                //While in effect, immune to Killian's terror, does not end upon death.
                case 47600650: //While in effect, immune to Killian's terror, does not end upon death.
                    break;

                //While the effect lasts, you cannot use any kind of skill! Cursed Jar cannot be removed.
                case 47661900: //While the effect lasts, you cannot use any kind of skill! Cursed Jar cannot be removed.
                    break;

                //Will not take any damage from any harmful effects.
                case 501500: //Will not take any damage from any harmful effects.
                    break;

                //With golem's power, receive reduced electricity based damage by [VAL].
                case 47662600: //With golem's power, receive reduced electricity based damage by $H_W_GOOD$value$COLOR_END.
                    break;

                //You are a quitter who abandoned your comrades in a dungeon. While your status is "Dropout", you cannot use instance matching.
                case 999994: //You are a quitter who abandoned your comrades in a dungeon. While your status is "Dropout", you cannot use instance matching.
                    break;

                //You are gradually becoming blind.
                case 47662300: //You are gradually becoming blind.
                    break;

                //You are out of energy! You cannot use any skills.
                case 47671000: //You are out of energy! You cannot use any skills.
                case 99002400: //You are out of energy! You cannot use any skills.
                case 99002410: //You are out of energy! You cannot use any skills.
                case 99002420: //You are out of energy! You cannot use any skills.
                case 99002430: //You are out of energy! You cannot use any skills.
                case 99002440: //You are out of energy! You cannot use any skills.
                case 99002450: //You are out of energy! You cannot use any skills.
                case 99002460: //You are out of energy! You cannot use any skills.
                case 99002470: //You are out of energy! You cannot use any skills.
                    break;

                //You are stunned and cannot move.
                case 23150: //You are stunned and cannot move.
                case 23155: //You are stunned and cannot move.
                    break;

                //You become 100% resistant to any harmful effect. This effect cannot be removed.
                case 99002071: //You become 100% resistant to any harmful effect. This effect cannot be removed.
                case 99002072: //You become 100% resistant to any harmful effect. This effect cannot be removed.
                    break;

                //You become stunned and cannot move.
                case 21120: //You become stunned and cannot move.
                case 26020: //You become stunned and cannot move.
                case 26090: //You become stunned and cannot move.
                    break;

                //You can attack other players and other players can attack you. Effect ends if you remain out of combat for 60 seconds.
                case 999801: //You can attack other players and other players can attack you. Effect ends if you remain out of combat for 60 seconds.
                    break;

                //You can use the rib-crushing skill.
                case 400900: //You can use the rib-crushing skill.
                    break;

                //You cannot use some skills or items while swimming.
                case 905000: //You cannot use some skills or items while swimming.
                case 999999: //You cannot use some skills or items while swimming.
                    break;

                //You cannot use your basic attack skills.
                case 500900: //You cannot use your basic attack skills.
                    break;

                //You cause all other party members within 10m to flee in terror.
                case 47611100: //You cause all other party members within 10m to flee in terror.
                    break;

                //You cause Insanity in all other party members within 10m.
                case 47611000: //You cause Insanity in all other party members within 10m.
                    break;

                //You decrease the movement speed of all party members within 10m of you.
                case 47610700: //You decrease the movement speed of all party members within 10m of you.
                    break;

                //You die when effect ends. This effect cannot be removed.
                case 476109: //You die when effect ends. This effect cannot be removed.
                    break;

                //You have 10 sec. to avoid Doom.
                case 47610910: //You have 10 sec. to avoid Doom.
                    break;

                //You have 3 sec. to avoid Failure, which blocks all skill use.
                case 47611210: //You have 3 sec. to avoid Failure, which blocks all skill use.
                    break;

                //You have 3 sec. to avoid Insanity, which reverses your movement keys.
                case 47611010: //You have 3 sec. to avoid Insanity, which reverses your movement keys.
                    break;

                //You have 3 sec. to avoid Panic, which causes you to flee.
                case 47611110: //You have 3 sec. to avoid Panic, which causes you to flee.
                    break;

                //You have 3 sec. to avoid Torpor, which slows movement speed.
                case 47610710: //You have 3 sec. to avoid Torpor, which slows movement speed.
                    break;

                //You have 3 sec. to avoid Weakness, which reduces power.
                case 47610810: //You have 3 sec. to avoid Weakness, which reduces power.
                    break;

                //You have escalated to a nefarious outlaw. Cannot use Clerics of Restoration, Noble Clerics of Restoration, teleport masters, or flight masters. Increased chance of crystal breakage. Increased rate of stamina loss. Power decreased by [VAL] and endurance decreased by [VAL]. Movement speed decreased by [VAL]. Effect ends if you remain out of combat for 60 seconds once your infamy is 0.
                case 999803: //You have escalated to a nefarious outlaw.$BRCannot use Clerics of Restoration, Noble Clerics of Restoration, teleport masters, or flight masters.$BRIncreased chance of crystal breakage.$BRIncreased rate of stamina loss.$BRPower decreased by $H_W_BAD$value$COLOR_END and endurance decreased by $H_W_BAD$value2$COLOR_END. Movement speed decreased by $H_W_BAD$value3$COLOR_END.$BREffect ends if you remain out of combat for 60 seconds once your infamy is 0.
                    break;

                //You have escalated to a notorious criminal. Cannot use Clerics of Restoration or Noble Clerics of Restoration. Increased chance of crystal breakage. Increased rate of stamina loss. Effect ends if you remain out of combat for 60 seconds once your infamy is 0.
                case 999802: //You have escalated to a notorious criminal.$BRCannot use Clerics of Restoration or Noble Clerics of Restoration.$BRIncreased chance of crystal breakage.$BRIncreased rate of stamina loss. Effect ends if you remain out of combat for 60 seconds once your infamy is 0.
                    break;

                //You have escalated to a reviled villain. Cannot use Clerics of Restoration, Noble Clerics of Restoration, teleport masters, or flight masters. Increased chance of crystal breakage. Increased rate of stamina loss. Power decreased by [VAL] and endurance decreased by [VAL]. Movement speed decreased by [VAL]. Effect ends if you remain out of combat for 60 seconds once your infamy is 0.
                case 1: //You have escalated to a reviled villain.$BRCannot use Clerics of Restoration, Noble Clerics of Restoration, teleport masters, or flight masters.$BRIncreased chance of crystal breakage.$BRIncreased rate of stamina loss.$BRPower decreased by $H_W_BAD$value$COLOR_END and endurance decreased by $H_W_BAD$value2$COLOR_END. Movement speed decreased by $H_W_BAD$value3$COLOR_END.$BREffect ends if you remain out of combat for 60 seconds once your infamy is 0.
                case 999804: //You have escalated to a reviled villain.$BRCannot use Clerics of Restoration, Noble Clerics of Restoration, teleport masters, or flight masters.$BRIncreased chance of crystal breakage.$BRIncreased rate of stamina loss.$BRPower decreased by $H_W_BAD$value$COLOR_END and endurance decreased by $H_W_BAD$value2$COLOR_END. Movement speed decreased by $H_W_BAD$value3$COLOR_END.$BREffect ends if you remain out of combat for 60 seconds once your infamy is 0.
                    break;

                //You lose [VAL] of your total HP every [VAL], and your melee damage decreases by [VAL]. Seal of revenge cannot be removed.
                case 47500400: //You lose $H_W_BAD20%$COLOR_END of your total HP every $H_W_BAD$tickInterval2$COLOR_END, and your melee damage decreases by $H_W_BAD$value$COLOR_END.$BRSeal of revenge cannot be removed.
                    break;

                //You may now use Final Salvo.
                case 600200: //You may now use Final Salvo.
                    break;

                //You may now use Master's Leash.
                case 201000: //You may now use Master's Leash.
                    break;

                //You may now use Staggering Counter.
                case 100700: //You may now use Staggering Counter.
                    break;

                //You may now use Vortex Slash.
                case 100400: //You may now use Vortex Slash.
                    break;

                //You project an aura on yourself and party members which decreases skill damage by [VAL]. Effect persists even after death.
                case 47502300: //You project an aura on yourself and party members which decreases skill damage by $H_W_BAD50%$COLOR_END. Effect persists even after death.
                    break;

                //You reduce the power of all other party members within 10m of you.
                case 47610800: //You reduce the power of all other party members within 10m of you.
                    break;

                //You take [VAL] more damage when hit by the enemy who marked you.
                case 600500: //You take $H_W_BAD10%$COLOR_END more damage when hit by the enemy who marked you.
                case 600501: //You take $H_W_BAD10%$COLOR_END more damage when hit by the enemy who marked you.
                case 600502: //You take $H_W_BAD10%$COLOR_END more damage when hit by the enemy who marked you.
                    break;

                //You will lose [VAL] of total HP every [VAL], and you cannot be healed. Seal of death cannot be removed.
                case 47500600: //You will lose $H_W_BAD1%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END, and you cannot be healed.$BRSeal of death cannot be removed.
                case 47501000: //You will lose $H_W_BAD10%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END, and you cannot be healed.$BRSeal of death cannot be removed.
                    break;

                //You will lose [VAL] of total HP every [VAL], and your damage of close-range attack skill will decrease by [VAL]. Seal of revenge cannot be removed.
                case 47500800: //You will lose $H_W_BAD10%$COLOR_END of total HP every $H_W_BAD$tickInterval2$COLOR_END, and your damage of close-range attack skill will decrease by $H_W_BAD$value$COLOR_END.$BRSeal of revenge cannot be removed.
                    break;

                //You will lose [VAL] of your current HP if the curse is not removed before the timer runs out.
                case 99001800: //You will lose $H_W_BAD$value$COLOR_END of your current HP if the curse is not removed before the timer runs out.
                    break;

                //You’ve been electrically shocked. Electric Shock can be stacked up to 4 times.
                case 47661500: //You’ve been electrically shocked. Electric Shock can be stacked up to 4 times.
                case 47662700: //You’ve been electrically shocked. Electric Shock can be stacked up to 4 times.
                    break;

                //You’ve been electrically shocked. When affected, your HP decreases by [VAL]. Electric Shock can be stacked up to 4 times.
                case 476615: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                case 476616: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                case 476617: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                case 476625: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                case 476626: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                case 476627: //You’ve been electrically shocked. When affected, your HP decreases by $H_W_BAD$value$COLOR_END. Electric Shock can be stacked up to 4 times.
                    break;

                //You’ve been exposed to enemies! Increases the aggression of enemies within 12 m. Exposure cannot be removed.
                case 47660700: //You’ve been exposed to enemies! Increases the aggression of enemies within 12 m. Exposure cannot be removed.
                    break;

                //You’ve fallen into a sticky swamp. Decreases movement speed by [VAL]. Stickiness cannot be removed.
                case 47662100: //You’ve fallen into a sticky swamp. Decreases movement speed by $H_W_BAD$value$COLOR_END. Stickiness cannot be removed.
                    break;

                //You’ve fallen into Insanity of the Black Warp! [VAL] Power and [VAL] the chance of getting knocked-down or immobilized. Insanity of the Black Warp cannot be removed.
                case 47680200: //You’ve fallen into Insanity of the Black Warp! $H_W_GOODTriples$COLOR_END Power and $H_W_GOODdoubles$COLOR_END the chance of getting knocked-down or immobilized.$BRInsanity of the Black Warp cannot be removed.
                    break;

                //You’ve fallen into Madness of the Black Warp! [VAL] movement speed and [VAL] Power. Madness of the Black Warp cannot be removed.
                case 47680300: //You’ve fallen into Madness of the Black Warp! $H_W_GOODDoubles$COLOR_END movement speed and $H_W_GOODtriples$COLOR_END Power.$BRMadness of the Black Warp cannot be removed.
                    break;

                //Your [VAL] protects others in your party while within 20m of you.
                case 200900: //Your $H_W_GOODPledge of Protection$COLOR_END protects others in your party while within 20m of you.
                    break;

                //Your movement keys are reversed.
                case 99001100: //Your movement keys are reversed.
                case 99001110: //Your movement keys are reversed.
                case 99001120: //Your movement keys are reversed.
                case 99001130: //Your movement keys are reversed.
                case 99001140: //Your movement keys are reversed.
                case 99001150: //Your movement keys are reversed.
                case 99001160: //Your movement keys are reversed.
                case 99001170: //Your movement keys are reversed.
                    break;

                //Your muscles spasm every [VAL].
                case 99001700: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001710: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001720: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001730: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001740: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001750: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001760: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                case 99001770: //Your muscles spasm every $H_W_BAD$tickInterval$COLOR_END.
                    break;

                //Your next attack does [VAL] more damage.
                case 300300: //Your next attack does $H_W_GOOD$value$COLOR_END more damage.
                    break;

                //Your next attack is [VAL] faster.
                case 300500: //Your next attack is $H_W_GOOD$value$COLOR_END faster.
                case 300501: //Your next attack is $H_W_GOOD$value$COLOR_END faster.
                case 300502: //Your next attack is $H_W_GOOD$value$COLOR_END faster.
                    break;

                //Your non-combat movement speed increases by [VAL].
                case 90510: //Your non-combat movement speed increases by $H_W_GOOD$value$COLOR_END.
                    Add(abnormal, new EfSpeedInc(), 0);
                    break;

                //Your Power increases by [VAL] and endurance decreases by [VAL].
                case 400100: //Your Power increases by $H_W_GOOD$value$COLOR_END and endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 400101: //Your Power increases by $H_W_GOOD$value$COLOR_END and endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 400102: //Your Power increases by $H_W_GOOD$value$COLOR_END and endurance decreases by $H_W_BAD$value2$COLOR_END.
                case 400103: //Your Power increases by $H_W_GOOD$value$COLOR_END and endurance decreases by $H_W_BAD$value2$COLOR_END.
                    break;

                //Your speed decreases by [VAL]. You must remove this effect before the timer runs out or die.
                case 99002600: //Your speed decreases by $H_W_BAD$value2$COLOR_END. You must remove this effect before the timer runs out or die.
                case 99002610: //Your speed decreases by $H_W_BAD$value2$COLOR_END. You must remove this effect before the timer runs out or die.
                    break;
            }
        }
    }
}
