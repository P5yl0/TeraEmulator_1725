using System;
using System.Collections.Generic;
using Communication;
using Data.Enums;
using Data.Enums.SkillEngine;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Npc;
using Data.Structures.Template;
using Network.Server;
using Tera.Services;
using Utils;

namespace Tera.AdminEngine.AdminCommands
{
    class Spawn : ACommand
    {
        public override void ProcessAsync(IConnection connection, string msg)
        {
            try
            {
                if (msg.Equals("on"))
                {
                    connection.Player.Instance.IsEditingMode = true;
                    Alert(connection, "Edit mode ON!");
                    return;
                }

                if (msg.Equals("off"))
                {
                    connection.Player.Instance.IsEditingMode = false;
                    Alert(connection, "Edit mode OFF!");
                    return;
                }

                if (msg.Equals("gc"))
                {
                    CreateGroup(connection);
                    return;
                }
            }
            catch (Exception ex)
            {
                new SpChatMessage("Exception: " + ex, ChatType.System).Send(connection);
            }
        }

        private void CreateGroup(IConnection connection)
        {
            var npcs = Global.VisibleService.FindTargets(connection.Player,
                                                         connection.Player.Position,
                                                         50,
                                                         TargetingAreaType.Enemy);

            npcs.Sort((c1, c2) =>
                {
                    Npc npc1 = c1 as Npc;
                    Npc npc2 = c2 as Npc;
                    if (npc1 == null || npc2 == null) 
                        return 0;

                    return npc2.NpcTemplate.Size.GetHashCode()
                        .CompareTo(npc1.NpcTemplate.Size.GetHashCode());
                });

            int counter = 0;
            int startAngle = Funcs.Random().Next(0, 359);

            foreach (var creature in npcs)
            {
                Npc npc = creature as Npc;
                if (npc == null) continue;

                int count = int.Parse(GetValue(connection, "Enter count of: [" + npc.NpcTemplate.Name + "]"));
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        SpawnTemplate spawnTemplate = npc.SpawnTemplate.Clone();

                        spawnTemplate.X += (float)(150f * Math.Cos(startAngle + counter * 30) * counter / 2 / Math.PI + Funcs.Random().Next(-25, 25));
                        spawnTemplate.Y += (float)(150f * Math.Sin(startAngle + counter * 30) * counter / 2 / Math.PI + Funcs.Random().Next(-25, 25));

                        Global.MapService.SpawnTeraObject(MapService.CreateNpc(spawnTemplate), npc.Instance);
                        counter++;
                    }

                    Global.MapService.DespawnTeraObject(npc);
                }
            }
        }
    }
}