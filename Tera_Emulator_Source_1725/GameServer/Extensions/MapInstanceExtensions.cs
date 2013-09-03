using Communication;
using Data.Structures.World;
using Network.Server;
using Utils;

namespace Tera.Extensions
{
    public static class MapInstanceExtensions
    {
        public static void AddDrop(this MapInstance instance, Item item)
        {
            instance.Items.Add(item);

            new DelayedAction(() => instance.RemoveItem(item), 60000);
        }

        public static void RemoveItem(this MapInstance instance, Item item)
        {
            try
            {
                instance.Items.Remove(item);
                Global.VisibleService.Send(item, new SpRemoveItem(item));
                item.Release();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
                //Already removed
            }
        }
    }
}
