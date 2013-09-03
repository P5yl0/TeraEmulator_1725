using System.Collections.Generic;
using Data.Structures.Geometry;
using Data.Structures.Npc;
using Data.Structures.Player;
using Network.Server;

namespace Tera.DungeonEngine.Dungeons
{
    class IoDDungeon : ADungeon
    {
        protected List<int> ShowedMovies = new List<int>();
 
        public override void Init()
        {
            ParentMapId = 13;

            foreach (Npc npc in Npcs)
                if (npc.NpcId == 1501 && npc.NpcTemplate.HuntingZoneId == 436)//exit teleport
                    npc.LifeStats.Kill();
        }

        public override void OnNpcKill(Player player, Npc killed)
        {
            if (killed.NpcTemplate.Name == "Karascha")
            {
                Players.ForEach(pl => new SpQuestMovie(6).Send(pl));

                foreach (Npc npc in Npcs)
                    if (npc.NpcId == 1501 && npc.NpcTemplate.HuntingZoneId == 436)//exit teleport
                        npc.LifeStats.Rebirth();
            }
        }

        public override void OnMove(Player player)
        {
            if (new Polygon
                    {
                        PointList =
                            new List<Point3D>
                                {
                                    new Point3D(69304.79f, -7931.33057f),
                                    new Point3D(69018.96f, -7932.204f),
                                    new Point3D(68999.23f, -7516.155f),
                                    new Point3D(69306.9844f, -7264.699f)
                                }
                    }.Contains(player.Position.ToPoint3D()) && !ShowedMovies.Contains(5))
            {
                Players.ForEach(pl => new SpQuestMovie(5).Send(pl));
                ShowedMovies.Add(5);
            }
        }
    }
}
