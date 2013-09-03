using Data.Structures.World;

namespace Tera.DungeonEngine.Dungeons
{
    abstract class ADungeon : MapInstance
    {
        public int ParentMapId;

        public long EnterTime;

        public abstract void Init();
    }
}
