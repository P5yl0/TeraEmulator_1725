using System.Collections.Generic;
using System.Linq;
using Communication.Interfaces;
using Data.Structures.Player;
using Data.Structures.World;
using Data.Structures.World.Continent;

namespace Tera.Services
{
    public class AreaService : IAreaService
    {
        public void Init()
        {
            foreach (var areaList in Data.Data.Areas.Values)
                foreach (var area in areaList)
                    SortSections(area);
        }

        private void SortSections(Area area)
        {
            if (area.Sections == null)
                area.Sections = new List<Section>();

            area.Sections.Sort((s1, s2) =>
                {
                    if (s1.Priority == s2.Priority)
                        return 0;

                    return s1.Priority > s2.Priority
                               ? -1
                               : 1;
                });

            foreach (var section in area.Sections)
                SortSections(section);
        }

        private void SortSections(Section section)
        {
            if (section.Sections == null)
                section.Sections = new List<Section>();

            section.Sections.Sort((s1, s2) =>
                {
                    if (s1.Priority == s2.Priority)
                        return 0;

                    return s1.Priority > s2.Priority
                               ? -1
                               : 1;
                });

            foreach (var section2 in section.Sections)
                SortSections(section2);
        }

        public Area GetPlayerArea(Player player)
        {
            return GetAreaByCoords(player.Position);
        }

        public Area GetAreaByCoords(WorldPosition coords)
        {
            if (!Data.Data.Areas.ContainsKey(coords.MapId))
                return null;

            return (from area in Data.Data.Areas[coords.MapId] from section in area.Sections where section.Polygon.Contains(coords.ToPoint3D()) select area).FirstOrDefault();
        }

        public Section GetCurrentSection(Player player)
        {
            return GetSectionByCoords(player.Position);
        }

        public Section GetSectionByCoords(WorldPosition coords)
        {
            Area area = GetAreaByCoords(coords);

            if (area == null)
                return null;

            Section s = null;

            foreach (Section section in area.Sections)
            {
                if (!section.Polygon.Contains(coords.ToPoint3D()))
                    continue;

                foreach (var section1 in section.Sections)
                    if (section1.Polygon.Contains(coords.ToPoint3D()))
                        if (s == null || section1.Priority > s.Priority)
                            s = section1;

                return s ?? section;
            }

            return null;
        }

        public void Action()
        {
            
        }
    }
}
