using System;
using System.Collections.Generic;
using Communication.Interfaces;
using Data.Structures.Template;
using Data.Structures.World;

namespace Tera.Services
{
    class GeoService : IGeoService
    {
        public static Dictionary<int, List<GeoLocation>> GeoData;

        public void Init()
        {
            GeoData = new Dictionary<int, List<GeoLocation>>();

            foreach (var mapSpawn in Data.Data.DcSpawns)
            {
                foreach (SpawnTemplate spawnTemplate in mapSpawn.Value)
                {
                    if (!GeoData.ContainsKey(spawnTemplate.MapId))
                        GeoData.Add(spawnTemplate.MapId, new List<GeoLocation>());

                    GeoLocation location = null;

                    for (int i = 0; i < Data.Data.GeoData.Count; i++)
                    {
                        if (Data.Data.GeoData[i].CheckIntersect(spawnTemplate.X, spawnTemplate.Y))
                        {
                            location = Data.Data.GeoData[i];
                            break;
                        }
                    }

                    if (location == null)
                        continue;

                    if (!GeoData[spawnTemplate.MapId].Contains(location))
                        GeoData[spawnTemplate.MapId].Add(location);
                }
            }
        }

        public void FixZ(WorldPosition position)
        {
            if ((int)position.X % 256 > 20 || (int)position.Y % 256 > 20)
                return;

            for (int i = 0; i < GeoData[position.MapId].Count; i++)
            {
                if (GeoData[position.MapId][i].CheckIntersect(position.X, position.Y))
                {
                    if (GeoData[position.MapId][i].OffsetZ.Equals(float.MinValue))
                        return;

                    position.Z = GeoData[position.MapId][i].GetZ(position.X, position.Y) + 25;
                    return;
                }
            }
        }

        public void FixOffset(int mapId, float x, float y, float z)
        {
            if ((int)x % 256 > 10 || (int)y % 256 > 10)
                return;

            for (int i = 0; i < GeoData[mapId].Count; i++)
            {
                if (GeoData[mapId][i].CheckIntersect(x, y))
                {
                    if (GeoData[mapId][i].FixesCount >= 10000)
                        return;

                    float offset = z - GeoData[mapId][i].GetZ(x, y);

                    if (!GeoData[mapId][i].OffsetZ.Equals(float.MinValue))
                        offset += GeoData[mapId][i].OffsetZ;

                    float newOffset = GeoData[mapId][i].OffsetZ;
                    int maxCount = 0;

                    for (int j = 0; j < GeoData[mapId][i].OffsetFixes.Count + 1; j++)
                    {
                        if (j == GeoData[mapId][i].OffsetFixes.Count)
                        {
                            if (GeoData[mapId][i].OffsetFixes.Count < 10)
                                GeoData[mapId][i].OffsetFixes.Add(new KeyValuePair<float, int>(offset, 1));

                            break;
                        }
                        
                        if (Math.Abs(offset - GeoData[mapId][i].OffsetFixes[j].Key) < 50)
                        {
                            GeoData[mapId][i].OffsetFixes[j] = new KeyValuePair<float, int>(
                                GeoData[mapId][i].OffsetFixes[j].Key,
                                GeoData[mapId][i].OffsetFixes[j].Value + 1);

                            break;
                        }
                    }

                    for (int j = 0; j < GeoData[mapId][i].OffsetFixes.Count; j++)
                    {
                        if (GeoData[mapId][i].OffsetFixes[j].Value > maxCount)
                        {
                            newOffset = GeoData[mapId][i].OffsetFixes[j].Key;
                            maxCount = GeoData[mapId][i].OffsetFixes[j].Value;
                        }
                    }

                    GeoData[mapId][i].FixesCount++;
                    GeoData[mapId][i].OffsetZ = newOffset;

                    return;
                }
            }
        }

        public void Action()
        {
            
        }
    }
}
