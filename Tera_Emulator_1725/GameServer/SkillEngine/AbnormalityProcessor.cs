using System;
using System.Collections.Generic;
using System.Threading;
using Communication;
using Data.Enums;
using Data.Structures.Creature;
using Data.Structures.Player;
using Data.Structures.SkillEngine;
using Data.Structures.World;
using Network.Server;

namespace Tera.SkillEngine
{
    class AbnormalityProcessor
    {
        public List<Abnormal> Abnormals;

        public object AbnormalsLock = new object();

        public AbnormalityProcessor()
        {
            Abnormals = new List<Abnormal>();
        }

        public void AddAbnormality(Creature target, int id, Creature caster = null)
        {
            if (id == 0)
                return;

            lock (target.EffectsLock)
            {
                for (int i = 0; i < target.Effects.Count; i++)
                {
                    if (target.Effects[i].Abnormality.Id == id)
                    {
                        ReleaseAbnormal(target.Effects[i]);
                        break;
                    }
                }
            }

            Abnormal abnormal = new Abnormal(target, Data.Data.Abnormalities[id], caster);

            lock (target.EffectsLock)
            {
                target.Effects.Add(abnormal);
            }

            EffectsProvider.ProvideEffects(abnormal);

            Player player = target as Player;
            if (abnormal.Effects.Count == 0
                && player != null
                && player.AccountName.Equals("alehandr", StringComparison.OrdinalIgnoreCase))
            {
                new SpChatMessage("Unknown effects for abnormality: " + id, ChatType.System).Send(player);
            }

            Global.VisibleService.Send(target, new SpAbnormal(abnormal));

            if (player != null)
                Global.PartyService.SendEffectsToPartyMembers(player.Party);

            lock (AbnormalsLock)
            {
                Abnormals.Add(abnormal);
            }
        }

        //

        private void ReleaseAbnormal(Abnormal abnormal)
        {
            lock (AbnormalsLock)
            {
                Abnormals.Remove(abnormal);
            }

            lock (abnormal.Creature.EffectsLock)
            {
                abnormal.Creature.Effects.Remove(abnormal);
            }

            Global.VisibleService.Send(abnormal.Creature, new SpRemoveAbnormal(abnormal));
            abnormal.Release();
        }

        public void Action()
        {
            lock (AbnormalsLock)
            {
                for (int i = 0; i < Abnormals.Count; i++)
                {
                    CheckAbnormal(Abnormals[i]);

                    if ((i & 511) == 0) // 2^N - 1
                        Thread.Sleep(1);
                }
            }
        }

        private void CheckAbnormal(Abnormal abnormal)
        {
            if (abnormal.TimeRemain == 0)
            {
                ReleaseAbnormal(abnormal);
                return;
            }

            if(abnormal.Effects == null)
                return;

            for (int i = 0; i < abnormal.Effects.Count; i++)
                abnormal.Effects[i].Action();
        }
    }
}