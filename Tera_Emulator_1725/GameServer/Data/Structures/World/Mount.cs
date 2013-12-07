namespace Data.Structures.World
{
    public class Mount
    {
        public int SkillId { get; set; }
        public int MountId { get; set; }
        public string Name { get; set; }
        public int SpeedModificator { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
