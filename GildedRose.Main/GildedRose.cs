using System.Collections.Generic;
using GildedRose.Main;

namespace GildedRose.Main
{
    public class GildedRose
    {
        private readonly IUpdateStrategyFactory _updateStrategyFactory = new UpdateStrategyFactory();
        readonly IList<Item> _items;

        public GildedRose(IList<Item> items)
        {
            this._items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                var updateStrategy = _updateStrategyFactory.Create(item);
                updateStrategy.Update(item);
            }
        }
    }

}