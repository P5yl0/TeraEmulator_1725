using System.Collections.Generic;
using Data.Enums;
using Data.Enums.Craft;

namespace Data.Structures.Craft
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public KeyValuePair<int, int> ResultItem { get; set; }
        public KeyValuePair<int, int> CriticalResultItem { get; set; }
        public byte CriticalChancePercent { get; set; }
        public Dictionary<int, int> NeededItems { get; set; }
        public CraftStat CraftStat { get; set; }

        public int Level { get; set; }
        /*
        public int Level
        {
            get
            {
                if (ReqMin/50 == 0)
                    return 1;
            
                return ReqMin/50;
            }
        }*/
        public short ReqMin { get; set; }
        public short ReqMax { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Recipe() { }
    }
}
