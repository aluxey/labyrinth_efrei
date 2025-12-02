using Labyrinth.Crawl;
using Labyrinth.Items;
using Labyrinth.Sys;
using Labyrinth.Tiles;
using System.Linq;

namespace Labyrinth
{
    public class RandExplorer(ICrawler crawler, IEnumRandomizer<RandExplorer.Actions> rnd)
    {
        private readonly ICrawler _crawler = crawler;
        private readonly IEnumRandomizer<Actions> _rnd = rnd;

        public enum Actions
        {
            TurnLeft,
            Walk
        }

        public int GetOut(int n)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 0, "n must be strictly positive");
            MyInventory bag = new ();

            for( ; n > 0 && _crawler.FacingTile is not Outside; n--)
            {
                EventHandler<CrawlingEventArgs>? changeEvent;
                TryOpenFacingDoor(bag);

                if (_crawler.FacingTile.IsTraversable
                    && _rnd.Next() == Actions.Walk)
                {
                    CollectInventory(bag, _crawler.Walk());
                    changeEvent = PositionChanged;
                }
                else
                {
                    _crawler.Direction.TurnLeft();
                    changeEvent = DirectionChanged;
                }
                changeEvent?.Invoke(this, new CrawlingEventArgs(_crawler));
            }
            return n;
        }

        public event EventHandler<CrawlingEventArgs>? PositionChanged;

        public event EventHandler<CrawlingEventArgs>? DirectionChanged;

        private static void CollectInventory(MyInventory bag, Inventory source)
        {
            while (source.HasItems)
            {
                bag.MoveItemFrom(source);
            }
        }

        private void TryOpenFacingDoor(MyInventory bag)
        {
            if (_crawler.FacingTile is not Door door || !door.IsLocked)
            {
                return;
            }

            var keys = bag.Items.OfType<Key>().ToList();
            foreach (var key in keys)
            {
                if (!door.IsLocked)
                {
                    break;
                }

                var keyIndex = IndexOf(bag, key);
                if (keyIndex < 0)
                {
                    continue;
                }

                var opened = door.Open(bag, keyIndex);
                if (opened)
                {
                    return;
                }

                MoveKeyToBack(bag, key);
            }
        }

        private static void MoveKeyToBack(MyInventory bag, ICollectable key)
        {
            var keyIndex = IndexOf(bag, key);
            if (keyIndex < 0)
            {
                return;
            }
            var temp = new MyInventory();
            temp.MoveItemFrom(bag, keyIndex);
            bag.MoveItemFrom(temp);
        }

        private static int IndexOf(MyInventory bag, ICollectable target)
        {
            var index = 0;
            foreach (var item in bag.Items)
            {
                if (ReferenceEquals(item, target))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }

}
